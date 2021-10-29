using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Visible Fields
    [SerializeField] Transform playerTransform;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float baseSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float sprintCost;
    [SerializeField] float maxStamina;
    [SerializeField] float recoverySpeed;
    [SerializeField] float minSprintThreshold;
    [SerializeField] float minSpeedThreshold;
    [SerializeField] Animator anim;
    [SerializeField] float shotgunLinger = 1;
    [SerializeField] float shotgunRateOfFire = 0.1f;
    [SerializeField] AudioManager breathingSFXManager;
    [SerializeField] GameObject bloodParticleSystem;

    // Hidden Fields
    Vector2 moveDirection;
    private bool isSprinting;
    private bool canFire = true;
    private float speed;
    private float stamina;
    GameController gameController;
    AudioManager manager;

    void Start()
    {
        FindReferences();
        GameObject.FindGameObjectWithTag("Shotgun").GetComponent<PolygonCollider2D>().enabled = false;
        stamina = maxStamina;
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.Ispaused)
        {
            // Check Sprinting
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isSprinting = (stamina > minSprintThreshold) || (isSprinting && stamina > 0);
            }
            else
                isSprinting = false;
            anim.SetBool("Sprinting", isSprinting);
            speed = isSprinting ? sprintSpeed : baseSpeed;

            // Check Shotgun
            if (Input.GetMouseButtonDown(0) && canFire)
            {
                StartCoroutine(ShotgunFire());
                StartCoroutine(RestrictRateOfFire());
            }

            // 2D Movement
            moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            bool isMoving = moveDirection.magnitude > minSpeedThreshold;
            IsMoving(isMoving);

            // Camera Movement
            Vector3 mousePosition = GetMouseWorldPosition();

            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            playerTransform.eulerAngles = new Vector3(0, 0, angle);

            // Update Stamina
            if (isSprinting && isMoving)
                stamina = Mathf.Clamp(stamina - Time.deltaTime * sprintCost, 0, 100);
            else
                stamina = Mathf.Clamp(stamina + Time.deltaTime * recoverySpeed, 0, 100);

            // Handle Animation Speed Scaling
            anim.speed = (isSprinting && isMoving ? sprintSpeed / baseSpeed : 1f);

            // Play heavy breathing if below the minimum stamina threshold
            if (stamina < minSprintThreshold && !breathingSFXManager.isPlaying("HeavyBreathing"))
            {
                breathingSFXManager.Play("HeavyBreathing");
            }
        }
        else 
        {
            IsMoving(false);
        }
    }

    private static Vector3 GetMouseWorldPosition() 
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    private static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    private IEnumerator ShotgunFire() {
        // Try to fire the gun
        if (gameController.getAmmo() > 0)
        {
            anim.SetTrigger("Shoot");
            gameController.setAmmo(gameController.getAmmo() - 1);
            GameObject.FindGameObjectWithTag("Shotgun").GetComponent<PolygonCollider2D>().enabled = true;
            yield return new WaitForSeconds(shotgunLinger);
            GameObject.FindGameObjectWithTag("Shotgun").GetComponent<PolygonCollider2D>().enabled = false;
            Debug.Log(gameController.getAmmo());
        }
        else 
        {
            manager.Play("NoAmmo");
        }
    }

    private IEnumerator RestrictRateOfFire() 
    {
        canFire = false;
        yield return new WaitForSeconds(shotgunRateOfFire);
        canFire = true;
    }

    public void Die() 
    {
        rb.freezeRotation = true;
        anim.SetTrigger("Killed");
        breathingSFXManager.Play("Crunch");
        stamina = 0;
    }

    void IsMoving(bool isMoving) 
    {
        Vector2 newVelocity = Vector2.zero;
        if (isMoving)
            newVelocity = moveDirection.normalized * speed;
        rb.velocity = newVelocity;
        anim.SetBool("Moving", isMoving);
    }

    public float getStaminaPercentage() 
    {
        return (stamina / maxStamina);
    }

    void FindReferences()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        manager = gameObject.GetComponentInChildren<AudioManager>();
    }
}

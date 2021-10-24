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
    [SerializeField] GameController gameController;
    [SerializeField] float minSpeedThreshold;

    [SerializeField] float shotgunLinger = 1;

    // Hidden Fields
    Vector2 moveDirection;
    private bool IsSprinting;
    private float speed;
    private float stamina;

    void Start()
    {
        GameObject.FindGameObjectWithTag("Shotgun").GetComponent<PolygonCollider2D>().enabled = false;
        stamina = maxStamina;
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            IsSprinting = (stamina > minSprintThreshold) || (IsSprinting && stamina > 0);
        }
        else
            IsSprinting = false;

        speed = IsSprinting ? sprintSpeed : baseSpeed;

        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("HI");
            StartCoroutine(ShotgunFire());
        }

        if (!gameController.Ispaused)
        {
            // Update Stamina
            if (IsSprinting)
                stamina = Mathf.Clamp(stamina - Time.deltaTime * sprintCost, 0, 100);
            else
                stamina = Mathf.Clamp(stamina + Time.deltaTime * recoverySpeed, 0, 100);                

            // 2D Movement
            moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (moveDirection.magnitude < minSpeedThreshold)
                rb.velocity = Vector2.zero;
            else
                rb.velocity = moveDirection.normalized * speed;

            // Camera Movement
            Vector3 mousePosition = GetMouseWorldPosition();

            Vector3 aimDirection = (mousePosition - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            playerTransform.eulerAngles = new Vector3(0, 0, angle);
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
        Debug.Log("YO");
        GameObject.FindGameObjectWithTag("Shotgun").GetComponent<PolygonCollider2D>().enabled = true;
        yield return new WaitForSeconds(shotgunLinger);
        GameObject.FindGameObjectWithTag("Shotgun").GetComponent<PolygonCollider2D>().enabled = false;
    }
}

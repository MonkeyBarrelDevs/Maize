using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterController : MonoBehaviour
{   
    [SerializeField] protected Animator anim;
    [SerializeField] protected float stunTime = 3;
    [SerializeField] protected float detectionRadius = 5;
    protected AIPath aiPath;
    protected GameController gameController;
    protected GameObject player;
    protected bool isWanderer = false;
    protected bool isAimlessWanderer = false;
    protected bool isStunned = false;
    protected CircleCollider2D circleCollider;
    protected WanderingDestinationSetter wanderSetter;
    protected AIDestinationSetter destinationSetter;


    //public bool stunned;
    // Start is called before the first frame update
    void Start()
    {
        FindReferences();
        MonsterSetup();
    }

    // Update is called once per frame
    void Update()
    {
        aiPath.canMove = !isStunned && !gameController.Ispaused;
        if (isWanderer) {
            WanderingChaseCheck();
        }
    }

    protected virtual void WanderingChaseCheck() 
    {
        WanderingChaseCheck(true);
    }

    protected void WanderingChaseCheck(bool doChase) 
    {
        bool withinProximity = Vector3.Distance(player.transform.position, this.transform.position) < detectionRadius;
        if (withinProximity && doChase)
        {
            Debug.Log("Chase started");
            wanderSetter.enabled = false;
            destinationSetter.enabled = true;
        }
        else
        {
            wanderSetter.enabled = true;
            destinationSetter.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collider) 
    {
        //Debug.Log("yo");
        if (collider.gameObject.tag.Equals("Player") && aiPath.canMove) {
            gameController.DeathEvent();
        }   
    }

    public virtual IEnumerator StunMonster() {
        // Debug.Log("Hi");
        circleCollider.enabled = false;
        isStunned = true;
        anim.SetTrigger("Stunned");
        yield return new WaitForSeconds(stunTime);
        circleCollider.enabled = true;
        isStunned = false;
    }

    void MonsterSetup()
    {
        if (wanderSetter == null)
        {
            Debug.Log("Not a wanderer!");
            destinationSetter.enabled = true;
        }
        else
        {
            if (destinationSetter == null)
            {
                // Debug.Log("An aimless wanderer!");
                isAimlessWanderer = true;
                wanderSetter.enabled = true;
            }
            else
            {
                // Debug.Log("A proximity chaser!");
                isWanderer = true;
                wanderSetter.enabled = true;
                destinationSetter.enabled = false;
            }
        }
    }

    void FindReferences() 
    {
        wanderSetter = gameObject.GetComponent<WanderingDestinationSetter>();
        destinationSetter = gameObject.GetComponent<AIDestinationSetter>();
        aiPath = gameObject.GetComponent<AIPath>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        player = GameObject.FindWithTag("Player");
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
}

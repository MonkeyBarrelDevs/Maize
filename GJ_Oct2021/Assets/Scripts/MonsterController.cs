using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterController : MonoBehaviour
{   
    [SerializeField] protected Animator anim;
    [SerializeField] protected float stunTime = 3;
    protected AIPath aiPath;
    protected GameController gameController;
    protected GameObject player;
    protected bool withinChaseProximity = false;
    protected bool isWanderer = false;
    protected bool isAimlessWanderer = false;
    protected bool isStunned = false;


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

    private void WanderingChaseCheck() {
        if (Vector3.Distance (player.transform.position, this.transform.position) < 5) {
            Debug.Log("COME HERE");
            withinChaseProximity = true;
        } else {
            withinChaseProximity = false;
        }
        if (withinChaseProximity) {
            Debug.Log("Chase started");
             GetComponent<WanderingDestinationSetter>().enabled = false;
             GetComponent<AIDestinationSetter>().enabled = true;
        } else {
            GetComponent<WanderingDestinationSetter>().enabled = true;
            GetComponent<AIDestinationSetter>().enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collider) {
        //Debug.Log("yo");
        if (collider.gameObject.tag.Equals("Player") && aiPath.canMove) {
            gameController.DeathEvent();
        }   
    }
    public virtual IEnumerator StunMonster() {
        Debug.Log("Hi");
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        isStunned = true;
        anim.SetTrigger("Stunned");
        yield return new WaitForSeconds(stunTime);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        isStunned = false;
    }

    void FindReferences() 
    {
        player = GameObject.FindWithTag("Player");
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void MonsterSetup() 
    {
        aiPath = gameObject.GetComponent<AIPath>();
        if (GetComponent<WanderingDestinationSetter>() == null)
        {
            Debug.Log("Not a wanderer!");
            GetComponent<AIDestinationSetter>().enabled = true;
        }
        else
        {
            if (GetComponent<AIDestinationSetter>() == null)
            {
                Debug.Log("An aimless wanderer!");
                isAimlessWanderer = true;
                GetComponent<WanderingDestinationSetter>().enabled = true;
            }
            else
            {
                Debug.Log("A wanderer!");
                isWanderer = true;
                GetComponent<WanderingDestinationSetter>().enabled = true;
                GetComponent<AIDestinationSetter>().enabled = false;
            }
        }
    }
}

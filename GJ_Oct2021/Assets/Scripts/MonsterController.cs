using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterController : MonoBehaviour
{   
    private AIPath aiPath;

    public GameObject Player;
    private GameController gameController;

    private bool withinChaseProximity = false;

    private bool isWanderer = false;

    private bool isAimlessWanderer = false;

    [SerializeField] Animator anim;

    //public bool stunned;
    // Start is called before the first frame update
    void Start()
    {
        aiPath = gameObject.GetComponent<AIPath>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        if (GetComponent<WanderingDestinationSetter>() == null) {
            Debug.Log("Not a wanderer!");
            GetComponent<AIDestinationSetter>().enabled = true;
        } else {
            if (GetComponent<AIDestinationSetter>() ==  null) {
                Debug.Log("An aimless wanderer!");
                isAimlessWanderer = true;
                GetComponent<WanderingDestinationSetter>().enabled = true;
            } else {
                Debug.Log("A wanderer!");
                isWanderer = true;
                GetComponent<WanderingDestinationSetter>().enabled = true;
                GetComponent<AIDestinationSetter>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Vector3.Distance (Player.transform.position, this.transform.position) < 5) {
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
        }*/
        if (isWanderer) {
            WanderingChaseCheck();
        }
    }

    private void WanderingChaseCheck() {
        if (Vector3.Distance (Player.transform.position, this.transform.position) < 5) {
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
    public IEnumerator StunMonster() {
        Debug.Log("Hi");
        GameObject.FindGameObjectWithTag("Monster").GetComponent<CircleCollider2D>().enabled = false;
        aiPath.canMove = false;
        anim.SetTrigger("Stunned");
        yield return new WaitForSeconds(4);
        GameObject.FindGameObjectWithTag("Monster").GetComponent<CircleCollider2D>().enabled = true;
        aiPath.canMove = true;
    }
}

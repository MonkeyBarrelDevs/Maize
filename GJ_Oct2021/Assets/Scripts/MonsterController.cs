using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterController : MonoBehaviour
{   
    private AIPath aiPath;

    public GameObject Player;
    private GameController gameController;

    //public bool stunned;
    // Start is called before the first frame update
    void Start()
    {
        aiPath = gameObject.GetComponent<AIPath>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        yield return new WaitForSeconds(4);
        GameObject.FindGameObjectWithTag("Monster").GetComponent<CircleCollider2D>().enabled = true;
        aiPath.canMove = true;
        
    }
}

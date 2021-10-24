using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterController : MonoBehaviour
{   
    private AIPath aiPath;
    //public bool stunned;
    // Start is called before the first frame update
    void Start()
    {
        aiPath = gameObject.GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnCollisionEnter2D(Collider2D collider) {
        if (collider.gameObject.tag.Equals("Flashlight")) {
            StartCoroutine(StunMonster());
        }

        if (collider.gameObject.tag.Equals("Shotgun")) {
            StartCoroutine(StunMonster());
        }    
    }*/
    public IEnumerator StunMonster() {
        Debug.Log("Hi");
        aiPath.canMove = false;
        yield return new WaitForSeconds(4);
        aiPath.canMove = true;
        
    }
}

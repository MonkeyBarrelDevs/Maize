using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private MonsterController monsterController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider) {

        monsterController = collider.gameObject.GetComponent<MonsterController>();
        StartCoroutine(monsterController.StunMonster());
        
        // if (collider.gameObject.tag.Equals("Monster")) {
        //     StartCoroutine(monsterController.StunMonster());
        // }  

        // if (collider.gameObject.tag.Equals("Unique Monster")) {

        //     Debug.Log("yo");
        //     StartCoroutine(monsterController.StunMonster());
        // }      
    }
}

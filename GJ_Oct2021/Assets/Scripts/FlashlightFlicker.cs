using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashlightFlicker : MonoBehaviour
{
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("LightSource").GetComponent<Light2D>().enabled = false;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
            GameObject.FindGameObjectWithTag("LightSource").GetComponent<Light2D>().enabled = true;
            
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("LightSource").GetComponent<Light2D>().enabled = false;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
            GameObject.FindGameObjectWithTag("LightSource").GetComponent<Light2D>().enabled = true;
        }
    }
}

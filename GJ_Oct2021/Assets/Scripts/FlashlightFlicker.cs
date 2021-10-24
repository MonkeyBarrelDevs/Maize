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
            Debug.Log("getin");
            this.gameObject.GetComponent<AudioSource>().Play(0);
            Debug.Log("test");
            GameObject.FindGameObjectWithTag("LightSource").GetComponent<Light2D>().enabled = false;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
            GameObject.FindGameObjectWithTag("LightSource").GetComponent<Light2D>().enabled = true;

            
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.gameObject.GetComponent<AudioSource>().Play(0);
            GameObject.FindGameObjectWithTag("LightSource").GetComponent<Light2D>().enabled = false;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
            GameObject.FindGameObjectWithTag("LightSource").GetComponent<Light2D>().enabled = true;
        }
    }
}

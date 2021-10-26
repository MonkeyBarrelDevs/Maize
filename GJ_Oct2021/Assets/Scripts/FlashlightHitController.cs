using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightHitController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Unique Monster"))
        {
            MC_Gazer gazer = collision.GetComponent<MC_Gazer>();
            gazer.InFlashlight(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Unique Monster"))
        {
            MC_Gazer gazer = collision.GetComponent<MC_Gazer>();
            gazer.InFlashlight(false);
        }
    }
}

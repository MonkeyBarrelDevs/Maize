using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerAudioTrigger : MonoBehaviour
{
    public void PlayClip()
    {
        this.gameObject.GetComponent<AudioManager>().Play("Flicker");
    }
}

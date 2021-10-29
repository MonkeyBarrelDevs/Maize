using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashlightFlicker : MonoBehaviour
{
    [SerializeField] float timeMin = 0.1f;
    [SerializeField] float timeMax = 0.7f;
    [SerializeField] float flickerThreshold = 10;
    [SerializeField] float delayMin = 2.5f;
    [SerializeField] float delayMax = 4f;
    GameObject player;
    Light2D light;
    bool isFlickering = false;

    private void Start()
    {
        FindReferences();
    }

    private void Update()
    {
        while (Vector3.Distance(player.transform.position, transform.position) < flickerThreshold && !isFlickering) 
        {
            StartCoroutine(flicker());
        }
    }

    IEnumerator flicker() 
    {
        // Stops from flickering within timeframe
        isFlickering = true;

        // Flicker the flashlight
        this.gameObject.GetComponent<AudioSource>().Play(0);
        light.enabled = false;
        yield return new WaitForSeconds(Random.Range(timeMin, timeMax));
        light.enabled = true;

        // Restrict rate of flicker
        yield return new WaitForSeconds(Random.Range(delayMin, delayMax));
        isFlickering = false;
    }

    void FindReferences() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        light = GameObject.FindGameObjectWithTag("LightSource").GetComponent<Light2D>();
    }
}

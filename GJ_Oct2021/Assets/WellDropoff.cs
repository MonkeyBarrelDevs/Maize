using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellDropoff : MonoBehaviour
{
    [SerializeField] int targetPebbles;
    [SerializeField] GameController gameController;
    [SerializeField] int droppedPebbles;
    [SerializeField] Sprite[] countdownSprites;
    [SerializeField] SpriteRenderer countdownRenderer;
    [SerializeField] SpriteRenderer arrowRenderer;

    void Update()
    {
        // Change the well sprite if you are holding a pebble
        arrowRenderer.enabled = gameController.getPebbles() > 0;

        // Change the countdown sprite above the well depending on the pebbles you have given it
        countdownRenderer.sprite = countdownSprites[droppedPebbles];
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            droppedPebbles += gameController.getPebbles();
            gameController.setPebbles(0);
        }
        if(droppedPebbles >= targetPebbles)
            gameController.VictoryEvent();
    }
}

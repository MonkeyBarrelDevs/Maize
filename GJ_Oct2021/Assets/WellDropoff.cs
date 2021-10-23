using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellDropoff : MonoBehaviour
{
    [SerializeField] int targetPebbles;
    [SerializeField] GameController gameController;
    [SerializeField] int droppedPebbles;
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

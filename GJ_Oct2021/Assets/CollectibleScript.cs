using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    enum CollectibleType
    {
        Pebble,
        Key,
        Ammo
    }

    private GameController gameController;
    [SerializeField] CollectibleType collectibleType;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
                switch(collectibleType)
                {
                case CollectibleType.Key:
                    Destroy(this.gameObject);
                    gameController.setKeys(gameController.getKeys() + 1);
                    break;
                case CollectibleType.Pebble:
                    Destroy(this.gameObject);
                    gameController.setPebbles(gameController.getPebbles() + 1);
                    break;
                case CollectibleType.Ammo:
                    if (gameController.getAmmo() < 2) 
                    {
                        Destroy(this.gameObject);
                        gameController.setAmmo(gameController.getAmmo() + 1);
                    }
                    break;
                }

        }
    }
}

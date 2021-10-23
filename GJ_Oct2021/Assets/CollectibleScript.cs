using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    enum CollectibleType
    {
        Pebble,
        Key
    }
    [SerializeField] GameController gameController;
    [SerializeField] CollectibleType collectibleType;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
                Destroy(this.gameObject);
                switch(collectibleType)
                {
                    case CollectibleType.Key:
                        gameController.setKeys(gameController.Keys + 1);
                        break;
                    case CollectibleType.Pebble:
                        gameController.setPebbles(gameController.Pebbles + 1);
                        break;
                }

        }
    }
}

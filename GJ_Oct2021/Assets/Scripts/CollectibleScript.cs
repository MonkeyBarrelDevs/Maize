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

    [SerializeField] CollectibleType collectibleType;
    AudioManager manager;
    GameController gameController;

    private void Start()
    {
        FindReferences();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (collectibleType)
            {
                case CollectibleType.Key:
                    ResolvePickup();
                    gameController.setKeys(gameController.getKeys() + 1);
                    break;
                case CollectibleType.Pebble:
                    ResolvePickup();
                    gameController.setPebbles(gameController.getPebbles() + 1);
                    break;
                case CollectibleType.Ammo:
                    if (gameController.getAmmo() < 2)
                    {
                        ResolvePickup();
                        gameController.setAmmo(gameController.getAmmo() + 1);
                    }
                    break;
            }
        }
    }

    void ResolvePickup()
    {
        switch (collectibleType) 
        {
            case CollectibleType.Ammo:
                manager.Play("PickUpAmmo");
                break;
            case CollectibleType.Pebble:
                manager.Play("PickUpMagicObject");
                break;
        }
        Destroy(this.gameObject);
    }

    void FindReferences()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        manager = gameController.GetComponent<AudioManager>();
    }
}

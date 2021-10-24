using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellDropoff : MonoBehaviour
{
    [SerializeField] int targetPebbles;
    [SerializeField] GameController gameController;
    [SerializeField] int droppedPebbles;
    public SpriteRenderer spriteRenderer;
    public Sprite wellArrowSprite, wellSprite;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(gameController.getPebbles() > 0)
        {
            spriteRenderer.sprite = wellArrowSprite;
        }
        else
        {
            spriteRenderer.sprite = wellSprite;
        }
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

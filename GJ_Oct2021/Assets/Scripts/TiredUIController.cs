using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredUIController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] CanvasGroup canvasGroup;
    [Range(0f, 1f)] [SerializeField] float maxOpacity; 

    // Update is called once per frame
    void Update()
    {
        canvasGroup.alpha = (1 - playerController.getStaminaPercentage()) * maxOpacity;
    }
}

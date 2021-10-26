using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Gazer : MonsterController
{
    [SerializeField] float enrageTime = 3;
    [SerializeField] float speedMultiplier = 1.25f;
    bool enraged = false;

    public void InFlashlight(bool inFlashlight) 
    {
        isStunned = !enraged && inFlashlight;
        anim.SetBool("Stunned", isStunned);
    }

    override
    public IEnumerator StunMonster() 
    {
        if (!enraged)
        {
            Debug.Log("Gaze");

            // Starts Enrage
            enraged = true;
            float initialSpeed = aiPath.maxSpeed;
            aiPath.maxSpeed = initialSpeed * speedMultiplier;

            // Enrage Animation Stun
            isStunned = true;
            anim.SetTrigger("Enraged");
            yield return new WaitForSeconds(stunTime);
            isStunned = false;

            // Resolves Enrage
            yield return new WaitForSeconds(enrageTime);
            enraged = false;
            aiPath.maxSpeed = initialSpeed;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Gazer : MonsterController
{
    [SerializeField] float enrageTime = 3;
    [SerializeField] float speedMultiplier = 1.25f;
    bool enraged = false;
    bool enraging = false;

    public void InFlashlight(bool inFlashlight)
    {
        if (!enraged) { 
            isStunned = inFlashlight;
            anim.SetBool("Stunned", isStunned);
        }
    }

    override
    public IEnumerator StunMonster() 
    {
        sprayBlood();
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

    protected override void WanderingChaseCheck()
    {
        WanderingChaseCheck(!enraged);
    }
}

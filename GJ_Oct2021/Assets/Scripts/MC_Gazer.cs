using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Gazer : MonsterController
{
    [SerializeField] float enrageTime = 3;
    [SerializeField] float speedMultiplier = 1.25f;
    bool enraged = false;
    bool enraging = false;

    /// <summary>
    /// The Gazer only becomes stunned while they are in the player's line-of-sight
    /// </summary>
    public void InFlashlight(bool inFlashlight)
    {
        if (!enraged) { 
            isStunned = inFlashlight;
            anim.SetBool("Stunned", isStunned);
        }
    }

    /// <summary>
    /// Instead of becoming stunn when shot, the Gazer becomes enraged and chases the player.
    /// </summary>
    override
    public IEnumerator StunMonster() 
    {
        sprayBlood(); // Activates the blood spray particle VFX
        if (!enraged)
        {
            // Set up the Gazer's enrage state
            enraged = true;
            float initialSpeed = aiPath.maxSpeed;
            aiPath.maxSpeed = initialSpeed * speedMultiplier;

            // Hold the Gazer in place while its enrage animation plays
            isStunned = true;
            anim.SetTrigger("Enraged");
            yield return new WaitForSeconds(stunTime);
            isStunned = false;

            // Switch off its enrage after a set amount of time
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

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyTrailControl))]


public class Zombie : BaseEnemy
{
    private EnemyTrailControl trailControl;
    private bool powerUp = false;

    public void loopPowerup()
    {
        powerUp = true;
        if (entityMovement.maxSpeed < entityMovement.maxMaxSpeed)
        {
            entityMovement.maxSpeed += 5;
            entityMovement.moveForce += 15;
        }

        if (powerUp)
        {
            StartCoroutine(hideTrail());
        }
    }

    IEnumerator hideTrail()
    {
        this.trailControl = GetComponent<EnemyTrailControl>();
        trailControl.trail.enabled = false;
        yield return new WaitForSeconds(0.25f);
        trailControl.trail.enabled = true;
    }
}

using UnityEngine;
using System.Collections;

public class FollowingEnemy : BaseEnemy
{
    Transform player;               // Reference to the player's position.

    public void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
    // Changes the enemy behaviour
    public override void AIControl() {
        float velocity = 1.0f;

        // configures movement based on player's position;
        Vector3 playerPosition = player.position;
        Vector3 enemyPosition = this.transform.position;

        // player is above enemy - must traverse down to portal
        // OR player is directly below the enemy - due to platform creation decides a random way
        if (((playerPosition.y > enemyPosition.y) && (playerPosition.y - enemyPosition.y) > 5) ||
            ((playerPosition.y < enemyPosition.y) && (enemyPosition.y - playerPosition.y) > 5) &&
            (Mathf.Abs(Mathf.Abs(playerPosition.x) - Mathf.Abs(enemyPosition.x)) < 15))
        {
            // does normal behaviour
            base.AIControl();
            return;
        }

        // enemy is on the right side - moving left 
        if ((playerPosition.x < enemyPosition.x))
        {
            //Moving left so invert velocity
            velocity *= -1;   
        }

        base.entityMovement.Movement(velocity);
	}
}

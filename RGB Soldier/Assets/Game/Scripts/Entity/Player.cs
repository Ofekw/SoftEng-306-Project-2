using UnityEngine;
using System.Collections;
using System;

// Enforces these modules to be loaded up with this module when placed on a prefab/game object
[RequireComponent(typeof(EntityMovement))]


public class Player : KillableEntityInterface {

    public EntityMovement entityMovement;
    public Rigidbody2D projectile;
    public float projectileSpeed = 10;
    public float xProjectileOffset = 0f;
    public float yProjectileOffset = 0f;

    // Use this for initialization
    void Start () 
    {
	 this.entityMovement = GetComponent<EntityMovement>();
	}
	
	void FixedUpdate () 
    {
        //if pressing jump button, call jump method to toggle boolean
        if (Input.GetButtonDown("Jump"))
        {
            entityMovement.Jump();
        }
        float hVelocity = Input.GetAxis("Horizontal");
        //call the base movement module method to handle movement
        entityMovement.Movement(hVelocity);

        //If the shift button is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Shoot();
        }
    }

    public void Shoot () {
        Rigidbody2D clone;
        //Shoot to the right
        if (entityMovement.facingRight) {
            clone = (Rigidbody2D)Instantiate(projectile, new Vector3(transform.position.x + xProjectileOffset, transform.position.y + yProjectileOffset, transform.position.z), transform.rotation);
            clone.velocity = new Vector2(projectileSpeed, 0);
        } else {
            //Shoot to the left
            clone = (Rigidbody2D)Instantiate(projectile, new Vector3(transform.position.x - xProjectileOffset, transform.position.y + yProjectileOffset, transform.position.z), transform.rotation);
            //Invert prefab
            Vector3 theScale = clone.transform.localScale;
            theScale.x *= -1;
            clone.transform.localScale = theScale;
            clone.velocity = new Vector2(-projectileSpeed, 0);
        }
    }

    public override void takeDamage(int damageReceived)
    {
        throw new NotImplementedException();
    }

    public override void die()
    {
        throw new NotImplementedException();
    }

}

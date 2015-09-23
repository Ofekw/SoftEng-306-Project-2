using UnityEngine;
using System.Collections;
using System;

// Enforces these modules to be loaded up with this module when placed on a prefab/game object
[RequireComponent(typeof(EntityMovement))]


public class Player : KillableEntityInterface {

    public EntityMovement entityMovement;
    public Rigidbody2D projectileRight;
    public Rigidbody2D projectileLeft;
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Shoot();
        }
    }

    public void Shoot () {
        Rigidbody2D clone;
        if (entityMovement.facingRight) {
            clone = (Rigidbody2D)Instantiate(projectileRight, new Vector3(transform.position.x + xProjectileOffset, transform.position.y + yProjectileOffset, transform.position.z), transform.rotation);
            clone.velocity = new Vector3(projectileSpeed, 0, 0);
        } else {
            clone = (Rigidbody2D)Instantiate(projectileLeft, new Vector3(transform.position.x - xProjectileOffset, transform.position.y + yProjectileOffset, transform.position.z), transform.rotation);
            clone.velocity = new Vector3(-projectileSpeed, 0, 0);
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

using UnityEngine;
using System.Collections;
using System;

// Enforces these modules to be loaded up with this module when placed on a prefab/game object
[RequireComponent(typeof(EntityMovement))]


public class Player : KillableEntityInterface {

    public EntityMovement entityMovement;


    bool moveRight = false;
    bool moveLeft = false;
    bool isJumping = false;

    Vector3 movement;

	// Use this for initialization
	void Start () 
    {
	    this.entityMovement = GetComponent<EntityMovement>();
	}
	
	void FixedUpdate () 
    {
        /*
        if (moveRight)
        {
            movement.Set(1, 0, 0);
            movement = movement.normalized * movementSpeed * Time.deltaTime;
            playerRigidBody.MovePosition(transform.position + movement);
        }
         * */
        //if pressing jump button, call jump method to toggle boolean
        if (Input.GetButtonDown("Jump"))
        {
            entityMovement.Jump();
        }
        if (isJumping)
        {
            entityMovement.Jump();
        }
        //float hVelocity = Input.GetAxis("Horizontal");
        float hVelocity = 0f;
        if (moveRight)
        {
            hVelocity += 1.0f;
        }
        else if (!moveRight)
        {
            hVelocity -= 1.0f;
        }

        if (moveLeft)
        {
            hVelocity -= 1.0f;
        }
        else if (!moveLeft)
        {
            hVelocity += 1.0f;
        }
        //call the base movement module method to handle movement
        entityMovement.Movement(hVelocity);
	}

    public void rightButtonPressed()
    {
        moveRight = true;
    }

    public void rightButtonReleased()
    {
        moveRight = false;
    }

    public void leftButtonPressed()
    {
        moveLeft = true;
    }

    public void leftButtonReleased()
    {
        moveLeft = false;
    }

    public void jumpPressed()
    {
        isJumping = true;
    }

    public void jumpReleased()
    {
        isJumping = false;
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

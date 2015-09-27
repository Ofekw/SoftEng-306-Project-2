using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// Enforces these modules to be loaded up with this module when placed on a prefab/game object
[RequireComponent(typeof(EntityMovement))]


public class Player : KillableEntityInterface {

    public EntityMovement entityMovement;
    public Slider healthSlider; //TODO: healthSlider logic should be moved to Player manager in future
	// Use this for initialization
	void Start () 
    {
	 this.entityMovement = GetComponent<EntityMovement>();
        healthSlider.value = maxHealth;
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
	}

    public override void takeDamage(int damageReceived)
    {
        healthSlider.value -= damageReceived;
        currentHealth -= damageReceived;
        
    }

    public override void die()
    {
        throw new NotImplementedException();
    }

}

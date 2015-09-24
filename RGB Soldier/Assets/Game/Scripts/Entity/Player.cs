using UnityEngine;
using System.Collections;
using System;
using Assets.Game.Scripts.Enviroment;

// Enforces these modules to be loaded up with this module when placed on a prefab/game object
[RequireComponent(typeof(EntityMovement))]


public class Player : KillableEntityInterface {

    public EntityMovement entityMovement;
    public Rigidbody2D projectile;
    public float projectileSpeed = 10;
    public float xProjectileOffset = 0f;
    public float yProjectileOffset = 0f;
    public Boolean attacking = false;
    public float attackCooldown = 0.3f;
    public float lastAttack;
    public BoxCollider2D meleeCollider;
    public Boolean specialAttack = false;
    //TODO move incrementing of special charge to game manager
    public int specialCharge = 0;
    //TODO associate with skill set 
    public int specialChargeMeterLength = 100;

    public int strength = 1;    //Strength - Melee
    public int agility = 1;     //Agility- Speed
    public int dexterity = 1;   //Dexterity- Range
    public int intelligence = 1;//Intelligence - Special
    public int vitality = 1;    //Vitality - Health

    // Use this for initialization
    void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
	    this.entityMovement = GetComponent<EntityMovement>();
        meleeCollider.enabled = false;
        attacking = false;
        specialAttack = false;
        lastAttack = Time.time;
    } 

    void Update () 
    {
        //TODO move incrementing of special charge to game manager
        specialCharge++;
        var shakingAmount = Input.acceleration.magnitude;
        if (shakingAmount > 1.5)
        {
            Special();
        }
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

        //If the control button is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Melee();
        }
        if(attacking == true){
            meleeCollider.enabled = true;
            if((Time.time - lastAttack) > 0.1)
            {
                attacking = false;
                meleeCollider.enabled = false;
            }
        }
        else
        {
            meleeCollider.enabled = false;
        }

        UpdateStats();
    }

    public void UpdateStats()
    {
        this.maxHealth = vitality;
        entityMovement.maxSpeed = agility * 5.0f;
        //Strength and dexterity are called during damage calculations
    }

    public void Melee() {
        if (Time.time > (lastAttack + attackCooldown))
        {
            attacking = true;
            lastAttack = Time.time;
        }
    }

    public void Special()
    {
        //If the meter is fully charged
        if (specialCharge >= specialChargeMeterLength)
        {
            specialAttack = true;
            specialCharge = 0;
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                var e = enemy.GetComponent<BaseEnemy>();
                e.die();
            }


        }

    }

    public void Shoot () {
        Rigidbody2D clone;
        //Shoot to the right
        if (entityMovement.facingRight) {
            clone = (Rigidbody2D)Instantiate(projectile, new Vector3(transform.position.x + xProjectileOffset, transform.position.y + yProjectileOffset, transform.position.z), transform.rotation);
            //Set damage equal to dexterity stat
            clone.GetComponent<ProjectileScript>().damage = dexterity;
            //Set x speed 
            clone.velocity = new Vector2(projectileSpeed, 0);
        } else {
            //Shoot to the left
            clone = (Rigidbody2D)Instantiate(projectile, new Vector3(transform.position.x - xProjectileOffset, transform.position.y + yProjectileOffset, transform.position.z), transform.rotation);
            clone.GetComponent<ProjectileScript>().damage = dexterity;
            //Invert prefab
            Vector3 theScale = clone.transform.localScale;
            theScale.x *= -1;
            clone.transform.localScale = theScale;
            //Set x speed
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

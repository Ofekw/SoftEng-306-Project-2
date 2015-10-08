using UnityEngine;
using System.Collections;
using System;
using Assets.Game.Scripts.Enviroment;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(EntityMovement))]

public class playerMultiplayer : KillableEntityInterface
{

    public EntityMovement entityMovement;
    public ProjectileSpawner projectileSpawner;
    public float projectileSpeed = 10;
    public float xProjectileOffset = 0f;
    public float yProjectileOffset = 0f;
    public bool attacking = false;
    public float attackCooldown = 0.3f;
    public float lastAttack;
    public float attackDuration = 0.2f;
    public BoxCollider2D meleeCollider;
    public float knockBackStrength = 500;

    public int strength = 3;    //Strength - Melee
    public int agility = 3;    //Agility- Speed
    public int dexterity = 3;   //Dexterity- Range
    public int vitality = 3;    //Vitality - Health
    public int currentHealth = 3;

    public int abilityPoints; // Points to spend on skill

    public bool temporaryInvulnerable = false;
    public float temporaryInvulnerableTime;
    public float invulnTime = 2.0f;


    bool moveRight = false;
    bool moveLeft = false;
    public bool isJumping = false;

    public string playerHInput; //horizontal input axis
    public string playerVInput; //vertical input axis
    public string playerJumpInput; 
    public string playerMeleeInput; 
    public string playerRangedInput;

    Vector3 movement;
    public Animator animator;                  //Used to store a reference to the Player's animator component.

    void Start()
    {
        this.entityMovement = GetComponent<EntityMovement>();
        projectileSpawner = GetComponent<PlayerProjectileSpawner>();
        meleeCollider.enabled = false;
        attacking = false;
        lastAttack = Time.time;
        temporaryInvulnerableTime = Time.time;

        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();
    }
	
    void Update()
    {
		if (GameManager.instance.isPaused ())
			return;
        //if pressing jump button, call jump method to toggle boolean
        if (Input.GetButtonDown("Jump"))
        {
            entityMovement.Jump();
        }

        if (isJumping)
        {
            entityMovement.Jump();
        }
        float hVelocity = CrossPlatformInputManager.GetAxis(playerHInput);

        if (hVelocity == 0)
        {
            hVelocity = Input.GetAxis(playerHInput);
        }

        //call the base movement module method to handle movement
        entityMovement.Movement(hVelocity);

        if (attacking == true)
        {
            meleeCollider.enabled = true;
            if ((Time.time - lastAttack) > attackDuration)
            {
                attacking = false;
                meleeCollider.enabled = false;
            }
        }
        else
        {
            meleeCollider.enabled = false;
        }


        if (temporaryInvulnerable)
        {
            if (Time.time > temporaryInvulnerableTime + invulnTime)
            {
                temporaryInvulnerable = false;
            }
        }   
    }
    
    public void Melee()
    {
        animator.SetTrigger("playerMelee");
        if (Time.time > (lastAttack + attackCooldown))
        {
            attacking = true;
            lastAttack = Time.time;
        }
    }

    public void Shoot()
    {
        animator.SetTrigger("playerShoot");
        //Shoot to the right
        if (entityMovement.facingRight)
        {
            projectileSpawner.spawnProjectile("arrowAttack", transform.position.x, transform.position.y, xProjectileOffset, yProjectileOffset, true);
        }
        else
        {
            projectileSpawner.spawnProjectile("arrowAttack", transform.position.x, transform.position.y, xProjectileOffset, yProjectileOffset, false);
        }
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
        if (!temporaryInvulnerable)
        {
            animator.SetTrigger("playerHit");
            calculateDamage(damageReceived);
        }
        if (currentHealth <= 0)
        {
            die();
        }
    }

    public void calculateDamage(int damageReceived)
    {
        currentHealth--;
        temporaryInvulnerable = true;
        temporaryInvulnerableTime = Time.time;
        print("You lost a life");
    }

    public void takeDamageKnockBack(int damageReceived, float dir)
    {
        if (!temporaryInvulnerable)
        {
            knockBack(dir);
            takeDamage(damageReceived);
        }
    }

    private void knockBack(float dir)
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockBackStrength * dir, knockBackStrength));
    }

    public override void die()
    {

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Orb"))
        {
            GameManager.instance.orbsCollected++;
        }
    }
}

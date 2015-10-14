using UnityEngine;
using System.Collections;
using System;
using Assets.Game.Scripts.Enviroment;
using UnityStandardAssets.CrossPlatformInput;

// Enforces these modules to be loaded up with this module when placed on a prefab/game object
[RequireComponent(typeof(EntityMovement))]


public class Player : KillableEntityInterface
{
    public EntityMovement entityMovement;
    public ProjectileSpawner projectileSpawner;
    public float projectileSpeed = 10;
    public float xProjectileOffset = 0f;
    public float yProjectileOffset = 0f;
    public Boolean attacking = false;
    public Boolean rangedAttack = false;
    public float attackCooldown = 0.3f;
    public float lastAttack;
    public float attackDuration = 0.2f;
    public BoxCollider2D meleeCollider;
    public float knockBackStrength = 500;

    public int strength;    //Strength - Melee
    public int agility;    //Agility- Speed
    public int dexterity;   //Dexterity- Range
    public int intelligence; //Intelligence - Special
    public int vitality;    //Vitality - Health

    public int abilityPoints; // Points to spend on skill

    public Boolean temporaryInvulnerable = false;
    public float temporaryInvulnerableTime;
    public float invulnTime = 2.0f;

    public SpriteRenderer renderer;
    public float opacitySwitchTime;

    public AudioClip meleeAttackSound;
    public AudioClip specialAttackSound;
    public AudioClip rangedAttackSound;
    public AudioClip damageTakenSound;

    bool moveRight = false;
    bool moveLeft = false;
    public bool isJumping = false;

    Vector3 movement;

    public Animator animator;                  //Used to store a reference to the Player's animator component.

    // Use this for initialization
    // Starts after everything has woken - must wait for gamecontrol
    void Start()
    {   
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        this.entityMovement = GetComponent<EntityMovement>();
        Camera.main.GetComponent<CameraShake>().enabled = false;
        projectileSpawner = GetComponent<PlayerProjectileSpawner>();
        meleeCollider.enabled = false;
        attacking = false;
        lastAttack = Time.time;
        temporaryInvulnerableTime = Time.time;
        renderer = this.gameObject.GetComponent<SpriteRenderer>();

        //Get a component reference to the Player's animator component
        animator = GetComponent<Animator>();

        strength = GameControl.control.playerStr;
        agility = GameControl.control.playerAgl;
        dexterity = GameControl.control.playerDex;
        intelligence = GameControl.control.playerInt;
        vitality = GameControl.control.playerVit;
        abilityPoints = GameControl.control.abilityPoints;
		maxHealth = vitality;
		currentHealth = maxHealth;
    }

    void Update()
    {
		if (GameManager.instance.isPaused ())
			return;
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

        if (isJumping)
        {
            entityMovement.Jump();
        }
        float hVelocity = CrossPlatformInputManager.GetAxis("Horizontal");

        if (hVelocity == 0)
        {
            hVelocity = Input.GetAxis("Horizontal");
            animator.ResetTrigger("Walk");
            Debug.Log("Set");
        }


        if (hVelocity != 0)
        {
            animator.SetTrigger("Walk");
            Debug.Log("Not");
        }



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

        if (attacking == true)
        {
            meleeCollider.enabled = true;
            if ((Time.time - lastAttack) > attackDuration)
            {
                attacking = false;
                animator.ResetTrigger("Melee");
                meleeCollider.enabled = false;
            }
        }
        else
        {
            meleeCollider.enabled = false;
        }



        if (temporaryInvulnerable)
        {
            Color color = new Color(1f, 1f, 1f, 1f);
            if (renderer.material.color.a == 1f && Time.time > opacitySwitchTime)
            {
                opacitySwitchTime = Time.time + 0.25f;
                color = renderer.material.color;
                color.a = .5f;
                renderer.material.color = color;
            }
            if (renderer.material.color.a == .5f && Time.time > opacitySwitchTime)
            {
                opacitySwitchTime = Time.time + 0.25f;
                color = renderer.material.color;
                color.a = 1f;
                renderer.material.color = color;
            }
            if (Time.time > temporaryInvulnerableTime + invulnTime)
            {
                temporaryInvulnerable = false;
                color = renderer.material.color;
                color.a = 1f;
                renderer.material.color = color;
            }
        }

        UpdateStats();
    }


    public void UpdateStats()
    {
        this.maxHealth = vitality;
        entityMovement.maxSpeed = agility * 5.0f;
        //Strength and dexterity are called during damage calculations
        strength = GameControl.control.playerStr;
        agility = GameControl.control.playerAgl;
        dexterity = GameControl.control.playerDex;
        intelligence = GameControl.control.playerInt;
        vitality = GameControl.control.playerVit;
        abilityPoints = GameControl.control.abilityPoints;
    }

    public void Melee()
    {
        AudioSource.PlayClipAtPoint(meleeAttackSound, transform.position);
        animator.SetTrigger("Attack");
        if (Time.time > (lastAttack + attackCooldown))
        {
            attacking = true;
            lastAttack = Time.time;
        }
    }

    public void Special()
    {
        //If the meter is fully charged
        if (GameManager.instance.canSpecialAtk)
        {
            AudioSource.PlayClipAtPoint(specialAttackSound, transform.position);
            Camera.main.GetComponent<CameraShake>().enabled = true;

            Camera.main.GetComponent<CameraShake>().shake = 2;
            GameManager.instance.resetSpecialAtkCounter(); //reset counter
            var enemies = GameObject.FindGameObjectsWithTag("Zombie");
            foreach (GameObject enemy in enemies)
            {
                var e = enemy.GetComponent<BaseEnemy>();
                e.die();
            }

        }

    }

    public void Shoot()
    {
        AudioSource.PlayClipAtPoint(rangedAttackSound, transform.position);

        animator.SetTrigger("Attack");
        //Shoot to the right
        if (entityMovement.facingRight)
        {
            projectileSpawner.spawnProjectile("arrowAttack", transform.position.x, transform.position.y + 1, xProjectileOffset, yProjectileOffset, true);
        }
        else
        {
		projectileSpawner.spawnProjectile("arrowAttack", transform.position.x, transform.position.y + 1, xProjectileOffset, yProjectileOffset, false);
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
        AudioSource.PlayClipAtPoint(damageTakenSound, transform.position);

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

        if(coll.transform.tag == "MovingPlatform")
        {
            transform.parent = coll.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }


}

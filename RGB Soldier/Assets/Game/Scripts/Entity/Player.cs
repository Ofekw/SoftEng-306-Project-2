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

    public SkinnedMeshRenderer renderer;
    public float opacitySwitchTime;

    private AudioClip meleeAttackSound;
    private AudioClip specialAttackSound;
    private AudioClip rangedAttackSound;
    private AudioClip damageTakenSound;
    private AudioClip jumpSound;

    bool moveRight = false;
    bool moveLeft = false;
    public bool isJumping = false;
    public AudioSource source;
    public bool hasRanged = false;

    Vector3 movement;

    public Animator animator;                  //Used to store a reference to the Player's animator component.
    public PowerupController powerController;

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
        renderer = this.GetComponentInChildren<SkinnedMeshRenderer>();
        meleeAttackSound = Resources.Load("Audio/melee_attack") as AudioClip;
        specialAttackSound = Resources.Load("Audio/special_attack") as AudioClip;
        rangedAttackSound = Resources.Load("Audio/range_attack") as AudioClip;
        damageTakenSound= Resources.Load("Audio/player_ugh") as AudioClip;
        jumpSound = Resources.Load("Audio/player_jump") as AudioClip;


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
            isJumping = false;
        }
        float hVelocity = CrossPlatformInputManager.GetAxis("Horizontal");

        if (hVelocity == 0)
        {
            hVelocity = Input.GetAxis("Horizontal");
            animator.ResetTrigger("Walk");
        }


        if (hVelocity != 0)
        {
            animator.SetTrigger("Walk");
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
                animator.ResetTrigger("Attack");
                meleeCollider.enabled = false;
            }
        }
        else
        {
            meleeCollider.enabled = false;
        }



        if (temporaryInvulnerable)
        {
            if (renderer.material.color.a == 1f && Time.time > opacitySwitchTime)
            {
                opacitySwitchTime = Time.time + 0.25f;
                setAlpha(0.5f);
            }
            if (renderer.material.color.a == .5f && Time.time > opacitySwitchTime)
            {
                opacitySwitchTime = Time.time + 0.25f;
                setAlpha(1.0f);
            }
            if (Time.time > temporaryInvulnerableTime + invulnTime)
            {
                temporaryInvulnerable = false;
                setAlpha(1.0f);
            }
        }

        UpdateStats();
    }

    public void setAlpha(float alpha)
    {
        Material[] materials = renderer.materials;
        for(int i = 0; i < materials.Length; i++)
        {
            Color colorAlpha = materials[i].color;
            colorAlpha.a = alpha;
            materials[i].color = colorAlpha;
        }
    }


    public void UpdateStats()
    {
        this.maxHealth = vitality;
        entityMovement.maxSpeed = agility * 5.0f;
        //Strength and dexterity are called during damage calculations
		if (powerController.isAttackBoost()) {
			strength = GameControl.control.playerStr + 1;
			dexterity = GameControl.control.playerDex + 1;
		} else {
			strength = GameControl.control.playerStr;
			dexterity = GameControl.control.playerDex;
		}
		if (powerController.isAgilityBoost()) {
			agility = GameControl.control.playerAgl + 1;
		} else {
			agility = GameControl.control.playerAgl;
		}
        intelligence = GameControl.control.playerInt;
        vitality = GameControl.control.playerVit;
        abilityPoints = GameControl.control.abilityPoints;
    }

    public void Melee()
    {
        animator.SetTrigger("Attack");

        if (Time.time > (lastAttack + attackCooldown))
        {
            source.PlayOneShot(meleeAttackSound, ((float)GameControl.control.soundBitsVolume )/100);
            AudioSource.PlayClipAtPoint(meleeAttackSound, transform.position);
            attacking = true;
            lastAttack = Time.time;
        }
    }

    public void Special()
    {
        //If the meter is fully charged
        if (GameManager.instance.canSpecialAtk)
        {
            Vibration.Vibrate(2000);
            source.PlayOneShot(specialAttackSound, ((float)GameControl.control.soundBitsVolume) / 100);
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
        hasRanged = true;
        source.PlayOneShot(rangedAttackSound, ((float)GameControl.control.soundBitsVolume) / 100);

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
        source.PlayOneShot(jumpSound, ((float)GameControl.control.soundBitsVolume) / 100);
        setJumping();
    }
    public void setJumping()
    {
        isJumping = true;
    }

    public override void takeDamage(int damageReceived)
    {
        AudioSource.PlayClipAtPoint(damageTakenSound, transform.position);

        if (!temporaryInvulnerable)
        {
            Vibration.Vibrate(100);
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
        currentHealth -= damageReceived;
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

		if (coll.gameObject.CompareTag ("BulletTime"))
		{
			GameManager.instance.activateBulletTime();
		}

		if (coll.gameObject.CompareTag ("Powerup")) 
		{
			Powerup powerup = coll.gameObject.GetComponent<Powerup>();
			PowerupController control = GameObject.FindGameObjectWithTag("PowerupController").GetComponent<PowerupController>();
			control.activatePowerup(powerup);

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

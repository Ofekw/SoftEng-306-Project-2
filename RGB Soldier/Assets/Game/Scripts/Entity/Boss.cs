using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(EntityMovement))]

public class Boss : KillableEntityInterface
{

    private Animator animator;
    bool isJumping = false;
    bool moveRight = false;
    bool moveLeft = false;
    bool facingRight = false;
    public float xProjectileOffset;
    public float yProjectileOffset;
    public EntityMovement entityMovement;
    public ProjectileSpawner projectileSpawner;
    public int health = 30;


    public override void die()
    {
        Destroy(this.gameObject);
    }

    public override void takeDamage(int damageReceived)
    {
        health -= damageReceived;
        if (health <= 0)
        {
            die();
        }
    }

    // Use this for initialization
    void Start()
    {
        this.entityMovement = GetComponent<EntityMovement>();
        this.animator = GetComponent<Animator>();
        projectileSpawner = GetComponent<BossProjectileSpawner>();
        yProjectileOffset = -0.2f;
        xProjectileOffset = 3f;
        health = 30;
    }

    // Update is called once per frame
    void Update()
    {

        //float hVelocity = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            moveLeft = true;
            animator.SetBool("isMovingLeft", true);
            facingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            moveRight = true;
            animator.SetBool("isMovingRight", true);
            facingRight = true;
        }

        if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            moveLeft = false;
            animator.SetBool("isMovingLeft", false);
        }

        if (Input.GetKeyUp(KeyCode.Keypad6))
        {
            moveRight = false;
            animator.SetBool("isMovingRight", false);
        }

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            entityMovement.Jump();
        }

        if (isJumping)
        {
            entityMovement.Jump();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            attack();
        }

        float hVelocity = 0f;
        if (moveRight && !moveLeft)
        {
            hVelocity = 1.0f;
        }
        else if (moveLeft && !moveRight)
        {
            hVelocity = -1.0f;
        }
        if (!moveRight && !moveLeft)
        {
            hVelocity = 0.0f;
        }

        //hVelocity = Input.GetAxis("Horizontal");
        //call the base movement module method to handle movement
        entityMovement.Movement(hVelocity);
    }

    void attack()
    {
        if ( facingRight)
        {
            projectileSpawner.spawnProjectile("blackOrbAttack", transform.position.x, transform.position.y, xProjectileOffset, yProjectileOffset, true);
        }
        else if (!(facingRight))
        {
            projectileSpawner.spawnProjectile("blackOrbAttack", transform.position.x, transform.position.y, xProjectileOffset, yProjectileOffset, false);
        }
    }
}
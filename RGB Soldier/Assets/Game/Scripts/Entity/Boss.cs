using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(EntityMovement))]

public class Boss : KillableEntityInterface
{

    private Animator animator;
    //bool isJumping = false;
    //bool moveRight = false;
    // moveLeft = false;
    public float xProjectileOffset;
    public float yProjectileOffset;
    public EntityMovement entityMovement;
    public ProjectileSpawner projectileSpawner;
    private float xSpawnPoints = 17.5f;
    private float attackTimer = 3f;
    GameObject player;


    public override void die()
    {
        Destroy(this.gameObject);
    }

    public override void takeDamage(int damageReceived)
    {
        currentHealth -= damageReceived;
        if (currentHealth <= 0)
        {
            die();
        }
    }

    public void AI()
    {
        teleport();
        blackOrbAttack();
    }

    private void teleport()
    {
        float yPos = 0;
        if (player.transform.position.y < -5)
        {
            yPos = -8.5f;
        }
        if (player.transform.position.x < 0)
        {
            if (entityMovement.facingRight)
            {
                entityMovement.facingRight = false;
                entityMovement.Flip();
            }
            entityMovement.facingRight = false;
            animator.SetBool("isMovingLeft", true);
            animator.SetBool("isMovingRight", false);
            this.gameObject.transform.position = Vector2.Lerp(this.gameObject.transform.position, new Vector2(xSpawnPoints, yPos), 3);
        } else if (player.transform.position.x >= 0)
        {
            if (!entityMovement.facingRight)
            {
                entityMovement.facingRight = true;
                entityMovement.Flip();
            }
            entityMovement.facingRight = true;
            animator.SetBool("isMovingRight", true);
            animator.SetBool("isMovingLeft", false);
            this.gameObject.transform.position = Vector2.Lerp(this.gameObject.transform.position, new Vector2(-xSpawnPoints, yPos), 3);
        }
    }


    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        this.entityMovement = GetComponent<EntityMovement>();
        this.animator = GetComponent<Animator>();
        projectileSpawner = GetComponent<BossProjectileSpawner>();
        yProjectileOffset = -0.2f;
        xProjectileOffset = 3f;
        currentHealth = 10;
    }

    // Update is called once per frame
    void Update()
    { 
        if(Math.Abs(player.transform.position.x - this.transform.position.x)< 5)
        {
            teleport();
        }
        attackTimer -= Time.deltaTime;
        if (attackTimer < 0)
        {
            attackTimer = 5f;
            spiritBomb();
        }
    }

    void blackOrbAttack()
    {
        if (entityMovement.facingRight)
        {
            projectileSpawner.spawnProjectile("blackOrbAttack", transform.position.x, transform.position.y, xProjectileOffset, yProjectileOffset, true);
        }
        else if (!(entityMovement.facingRight))
        {
            projectileSpawner.spawnProjectile("blackOrbAttack", transform.position.x, transform.position.y, xProjectileOffset, yProjectileOffset, false);
        }
    }

    void spiritBomb()
    {
        if (entityMovement.facingRight)
        {
            projectileSpawner.spawnProjectile("definetlyNotASpiritBomb", transform.position.x, transform.position.y, xProjectileOffset, yProjectileOffset, true);
        }
        else if (!(entityMovement.facingRight))
        {
            projectileSpawner.spawnProjectile("definetlyNotASpiritBomb", transform.position.x, transform.position.y, xProjectileOffset, yProjectileOffset, false);
        }
    }
}
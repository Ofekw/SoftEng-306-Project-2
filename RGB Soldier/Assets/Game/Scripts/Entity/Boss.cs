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
    public BossProjectileSpawner projectileSpawner;
    private float xSpawnPoints = 17.5f;
    private float attackTimer = 1f;
    private System.Random rand = new System.Random();
    GameObject player;


    public override void die()
    {
        Destroy(this.gameObject);
        Application.LoadLevel("menu_start_screen");
    }

    public override void takeDamage(int damageReceived)
    {
        currentHealth -= damageReceived;
        if (currentHealth <= 0)
        {
            die();
        }
    }

    private void teleport()
    {
        float yPos = 0;
        if (player.transform.position.y < -5)
        {
            yPos = -8.5f;
        }

        float teleX = xSpawnPoints;
        if (-12 < player.transform.position.x && player.transform.position.x < 12)
        {
            int random = rand.Next(1, 3);
            if (random == 1)
            {
                teleX *= -1;
            }
        }
        else if (player.transform.position.x >= 0)
        {
            teleX *= -1;
        }

        if (teleX > 0)
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
        } else if (teleX <= 0)
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
        if (attackTimer <= 0)
        {
            attackTimer = 5f;
            int attackNo = rand.Next(1, 5);
            if (attackNo == 1)
            {
                teleport();
                spiritBomb();
            } else
            {
                teleport();
                blackOrbAttack();
            }
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
            projectileSpawner.spawnProjectile("unblockableAttack", transform.position.x, transform.position.y+1, xProjectileOffset+2, yProjectileOffset, true);
        }
        else if (!(entityMovement.facingRight))
        {
            projectileSpawner.spawnProjectile("unblockableAttack", transform.position.x, transform.position.y+1, xProjectileOffset+2, yProjectileOffset, false);
        }
    }
}
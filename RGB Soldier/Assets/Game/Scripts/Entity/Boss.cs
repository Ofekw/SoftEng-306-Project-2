using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(EntityMovement))]

public class Boss : KillableEntityInterface
{

    private Animator animator;
    public float xProjectileOffset;
    public float yProjectileOffset;
    public EntityMovement entityMovement;
    public BossProjectileSpawner projectileSpawner;
    private float xSpawnPoints = 17.5f;
    private float attackTimer = 1f;
    private System.Random rand = new System.Random();
    private GameObject player;
    private GameObject healthBar;
    private Vector3 healthBarScale;

    public GameObject shield;
    private Boolean isShielded = false;
    private GameObject shieldClone;
    private Boolean canTeleport = true;

    public override void die()
    {
        Destroy(this.gameObject);
        Application.LoadLevel("start_screen");
    }

    public override void takeDamage(int damageReceived)
    {
        currentHealth -= damageReceived;
        healthBar.transform.localScale = new Vector3((currentHealth*1.0f/maxHealth)*healthBarScale.x, healthBarScale.y, 1);
        if (currentHealth <= 0)
        {
            die();
        }
    }

    public void teleport()
    {
        float yPos = 0;
        //if player is on lower half, set y tele pos
        if (player.transform.position.y < -5)
        {
            yPos = -8.5f;
        }
        //find x tele position
        float teleX = xSpawnPoints;
        if (-8 < player.transform.position.x && player.transform.position.x < 8)
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
        if (isShielded)
        {
            //check if player is sitting above boss
            float yRelative = transform.position.y - player.transform.position.y;
            float playerX = player.transform.position.x;
            float sign = Mathf.Sign(yRelative);
            //player is above boss so teleport to other side at same level
            if (sign == -1)
            {
                yPos = 0f;
            }
            //player is sitting below the boss so teleport to other side at same level
            else if (sign == 1)
            {
                yPos = -8.5f;
            }
            //else simply don't move and fire at player for being s
            else
            {
                return;
            }
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
            checkForAttack();
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
            checkForAttack();
            this.gameObject.transform.position = Vector2.Lerp(this.gameObject.transform.position, new Vector2(-xSpawnPoints, yPos), 3);
        }
    }

    private void checkForAttack()
    {
        //check if any orb attacks are charging when teleport is called. If they are they should be 
        GameObject orbAttack = GameObject.FindGameObjectWithTag("UnblockableOrbAttack");
        //check that there is an orbattack and it is scaling, not already launched.
        if (orbAttack != null && orbAttack.GetComponent<UnblockableOrbAttack>().startScale)
        {
            Destroy(orbAttack);
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
        maxHealth = 10;
        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        healthBarScale = healthBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //check if shield and boss can unshield as player is valid distance away
        if (isShielded)
        {
            if (Math.Abs(player.transform.position.x - this.transform.position.x) > 10)
            {
                shieldClone.transform.localScale = Vector3.Lerp(shieldClone.transform.localScale, new Vector3(0.1f, 0.1f), 4 * Time.deltaTime);
                if (shieldClone.transform.localScale.x < 0.25)
                {
                    Destroy(shieldClone);
                    isShielded = false;
                    canTeleport = true;
                }
            }
            else if (shieldClone.transform.localScale.x < 1)
            {
                shieldClone.transform.localScale = Vector3.Lerp(shieldClone.transform.localScale, new Vector3(1.2f, 1.2f), 5 * Time.deltaTime);
            }
        }
        //else check if player is close and shield should be generated
        else if (Math.Abs(player.transform.position.x - this.transform.position.x) < 10)
        {
            isShielded = true;
            shieldClone = (GameObject)Instantiate(shield, gameObject.transform.position, gameObject.transform.rotation);
            canTeleport = false;
        }
        attackTimer -= Time.deltaTime;
        if (canTeleport)
        {
            if (attackTimer <= 0)
            {
                attackTimer = 4f;
                int attackNo = rand.Next(1, 3);
                if (attackNo == 1)
                {
                    teleport();
                    spiritBomb();
                }
                else
                {
                    teleport();
                    blackOrbAttack();
                }
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
            projectileSpawner.spawnProjectile("unblockableAttack", transform.position.x, transform.position.y+1.1f, xProjectileOffset+2, yProjectileOffset, true);
        }
        else if (!(entityMovement.facingRight))
        {
            projectileSpawner.spawnProjectile("unblockableAttack", transform.position.x, transform.position.y+1.1f, xProjectileOffset+2, yProjectileOffset, false);
        }
    }
}
using UnityEngine;
using System.Collections;
using System;
using GooglePlayGames;

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

    private Action currentAttack;
    private int attackAnCounter = 0;

    private Boolean isDead = false;
    public GameObject[] orbs;

    public override void die()
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportProgress("CgkIpKjLyoEdEAIQBQ", 100.0f, (bool success) =>
            {
            });
        }

        Player checkPlayer = player.GetComponent<Player>();
        if (checkPlayer.hasRanged == false)
        {
            if (Social.localUser.authenticated)
            {
                Social.ReportProgress("CgkIpKjLyoEdEAIQBg", 100.0f, (bool success) =>
                {
                });
            }
        }
        //teleport the boss to the middle platform to die.
        this.gameObject.transform.position = Vector2.Lerp(this.gameObject.transform.position, new Vector2(0, -0.8f), 3);
        animator.SetBool("Dead", true);
    }

    public void destroyGameObject()
    {
        float pos = -1.5f;
        Destroy(this.gameObject);
        for(int i = 0; i< orbs.Length; i++)
        {
            Instantiate(orbs[i], new Vector3(pos, -0.8f), gameObject.transform.rotation);
            pos += 1.5f;
        }
    }

    public override void takeDamage(int damageReceived)
    {
        currentHealth -= damageReceived;
        healthBar.transform.localScale = new Vector3((currentHealth*1.0f/maxHealth)*healthBarScale.x, healthBarScale.y, 1);
        if (currentHealth <= 0)
        {
            die();
            isDead = true;
        }
    }

    public void teleport()
    {
        //top spawn point
        float yPos = 0f;
        //if player is on lower half, set y tele pos
        if (player.transform.position.y < -5)
        {
            yPos = -7.5f;
        }
        //find x tele position
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
                yPos = -7.5f;
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
        //renderer = this.gameObject.GetComponent<SpriteRenderer>();
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            //check if shielded and boss can unshield as player is valid distance away
            if (isShielded)
            {
                if (Math.Abs(player.transform.position.x - this.transform.position.x) > 10)
                {
                    shieldClone.transform.localScale = Vector3.Lerp(shieldClone.transform.localScale, new Vector3(0.1f, 0.1f), 4 * Time.deltaTime);
                    if (shieldClone.transform.localScale.x < 0.25)
                    {
                        Destroy(shieldClone);
                        isShielded = false;
                    }
                }
                //else if shielded and shield isn't it's correct size, increase the size
                else if (shieldClone.transform.localScale.x < 1)
                {
                    shieldClone.transform.localScale = Vector3.Lerp(shieldClone.transform.localScale, new Vector3(1.25f, 1.25f), 5 * Time.deltaTime);
                }
            }
            //else check if player is close and shield should be generated
            else if (Math.Abs(player.transform.position.x - this.transform.position.x) < 10)
            {
                isShielded = true;
                Vector3 pos = gameObject.transform.position;
                pos.y += 1.4f;
                shieldClone = (GameObject)Instantiate(shield, pos, gameObject.transform.rotation);
            }
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attackTimer = 5f;
                int attackNo = rand.Next(1, 3);
                if (attackNo == 1)
                {
                    StartCoroutine(teleFlicker(1, 0.1f, 0.1f, spiritBomb));
                }
                else
                {
                    StartCoroutine(teleFlicker(1, 0.1f, 0.1f, blackOrbAttack));
                }
            }
        }
    }

    void blackOrbAttack()
    {
        if (entityMovement.facingRight)
        {
            projectileSpawner.spawnProjectile("blackOrbAttack", transform.position.x, transform.position.y+0.6f, xProjectileOffset, yProjectileOffset, true);
        }
        else if (!(entityMovement.facingRight))
        {
            projectileSpawner.spawnProjectile("blackOrbAttack", transform.position.x, transform.position.y+0.6f, xProjectileOffset, yProjectileOffset, false);
        }
    }

    void spiritBomb()
    {
        if (entityMovement.facingRight)
        {
            projectileSpawner.spawnProjectile("unblockableAttack", transform.position.x, transform.position.y + 1.5f, xProjectileOffset + 2, yProjectileOffset, true);
        }
        else if (!(entityMovement.facingRight))
        {
            projectileSpawner.spawnProjectile("unblockableAttack", transform.position.x, transform.position.y + 1.5f, xProjectileOffset + 2, yProjectileOffset, false);
        }
    }

    IEnumerator teleFlicker(int nTimes, float timeOn, float timeOff, Action attack)
    {
        //do teleport flickering
        while (nTimes > 0)
        {
            //renderer.material.color = new Color(0f, 0f, 0f, 0f);
            yield return new WaitForSeconds(timeOn);
            //renderer.material.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(timeOff);
            nTimes--;
        }
        //check if boss is shielded, if they are, destroy the shield before teleporting
        if (isShielded)
        {
            Destroy(shieldClone);
            isShielded = false;
        }
        //teleport and set current attack for when attack animation finishes the doAttack fires the attack
        teleport();
        currentAttack = attack;
        if (attack == spiritBomb)
        {
            animator.SetBool("SpiritBomb", true);
        }
        else
        {
            animator.SetBool("OrbAttack", true);
        }
        
    }

    //method used in animation to launch attack at different times
    public void doAttack()
    {
        //count used to track which call it is on. It is called twice in the animation
        if (currentAttack == spiritBomb && attackAnCounter == 0)
        {
            //start the spiritbomb animation on the first call as it takes time to charge.
            spiritBomb();
        }
        else if (currentAttack == blackOrbAttack && attackAnCounter == 1)
        {
            blackOrbAttack();
        }
        attackAnCounter++;
        if (attackAnCounter == 2)
        {
            animator.SetBool("OrbAttack", false);
            animator.SetBool("SpiritBomb", false);
            attackAnCounter = 0;
        }
    }
}
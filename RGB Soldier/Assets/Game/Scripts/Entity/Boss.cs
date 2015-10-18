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
    public AudioSource source;

    private AudioClip chargeSound;
    private AudioClip largeShootSound;
    private AudioClip smallShootSound;

    private float shieldDownTimer = 5f;
    public SkinnedMeshRenderer renderer;

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
        }else
        {
            teleport();
            generateShield();
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
        if (-7.5 < player.transform.position.x && player.transform.position.x < 7.5)
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
        chargeSound = Resources.Load("Audio/boss_charge") as AudioClip;
        largeShootSound = Resources.Load("Audio/boss_shoot_large") as AudioClip;
        smallShootSound = Resources.Load("Audio/small_shoot_sound") as AudioClip;
        renderer = this.GetComponentInChildren<SkinnedMeshRenderer>();
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
        });
        generateShield();
    }

    private void generateShield()
    {
        isShielded = true;
        Vector3 pos = gameObject.transform.position;
        pos.y += 1.4f;
        shieldClone = (GameObject)Instantiate(shield, pos, gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShielded)
        {
            if (shieldClone.transform.localScale.x < 1)
            {
                shieldClone.transform.localScale = Vector3.Lerp(shieldClone.transform.localScale, new Vector3(1.25f, 1.25f), 2 * Time.deltaTime);
            }
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                attackTimer = 5f;
                int attackNo = rand.Next(1, 3);
                if (attackNo == 1)
                {
                    StartCoroutine(teleFlicker(20, 0.01f, 0.01f, spiritBomb));
                }
                else
                {
                    StartCoroutine(teleFlicker(20, 0.01f, 0.01f, blackOrbAttack));
                }
            }
        } else
        {
            shieldDownTimer -= Time.deltaTime;
            if (shieldDownTimer <= 0)
            {
                shieldDownTimer = 5f;
                generateShield();
            }
            else
            {
                if (shieldClone != null) {
                    if (shieldClone.transform.localScale.x < 0.25)
                    {
                        Destroy(shieldClone);
                    }
                    shieldClone.transform.localScale = Vector3.Lerp(shieldClone.transform.localScale, new Vector3(0.1f, 0.1f), 6 * Time.deltaTime);
                }
            }
        }
    }

    public void takeDownShield()
    {
        isShielded = false;
    }

    void blackOrbAttack()
    {
        if (entityMovement.facingRight)
        {
            source.PlayOneShot(smallShootSound, ((float)GameControl.control.soundBitsVolume) / 100);
            projectileSpawner.spawnProjectile("blackOrbAttack", transform.position.x, transform.position.y+0.6f, xProjectileOffset, yProjectileOffset, true);
        }
        else if (!(entityMovement.facingRight))
        {
            source.PlayOneShot(smallShootSound, ((float)GameControl.control.soundBitsVolume) / 100);
            projectileSpawner.spawnProjectile("blackOrbAttack", transform.position.x, transform.position.y+0.6f, xProjectileOffset, yProjectileOffset, false);
        }
    }

    void spiritBomb()
    {
        if (entityMovement.facingRight)
        {
            source.PlayOneShot(chargeSound, ((float)GameControl.control.soundBitsVolume) / 100);

            projectileSpawner.spawnProjectile("unblockableAttack", transform.position.x, transform.position.y + 1.5f, xProjectileOffset + 4, yProjectileOffset, true);
        }
        else if (!(entityMovement.facingRight))
        {
            source.PlayOneShot(chargeSound, ((float)GameControl.control.soundBitsVolume) / 100);

            projectileSpawner.spawnProjectile("unblockableAttack", transform.position.x, transform.position.y + 1.5f, xProjectileOffset + 4, yProjectileOffset, false);
        }
    }

    IEnumerator teleFlicker(int nTimes, float timeOn, float timeOff, Action attack)
    {
        //do teleport flickering
        while (nTimes > 0)
        {
            renderer.material.color = new Color(0f, 0f, 0f, 0f);
            yield return new WaitForSeconds(timeOn);
            renderer.material.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(timeOff);
            nTimes--;
        }
        //check if boss is shielded, if they are, destroy the shield before teleporting
        if (isShielded)
        {
            Destroy(shieldClone);
        }
        //teleport and set current attack for when attack animation finishes the doAttack fires the attack
        teleport();
        if (isShielded)
        {
            generateShield();
        }
        currentAttack = attack;
        if (attack == spiritBomb)
        {
            animator.SetBool("SpiritBomb", true);
        }
        else if (attack == blackOrbAttack)
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
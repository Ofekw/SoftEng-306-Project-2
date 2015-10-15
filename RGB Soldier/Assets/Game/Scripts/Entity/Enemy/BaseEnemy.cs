using System.Collections;
using UnityEngine;

// Enforces these modules to be loaded up with this module when placed on a prefab/game object
[RequireComponent(typeof(EntityMovement))]
[RequireComponent(typeof(EnemyTrailControl))]


public class BaseEnemy : KillableEntityInterface
{
    public EntityMovement entityMovement;
    public int damageGiven = 1;
    public GameObject orb;
    public int experienceGiven = 0;
    public bool isSpecialLevel;
    private EnemySpawnController spawnController;
    private bool powerUp = false;
    private EnemyTrailControl trailControl;
    public float knockBackStrength = 300;
    public AudioSource source;
	private Vector2 _startVector;
    private AudioClip dieSound;
	private Rigidbody2D _body;
    private Animator animator;                  //Used to store a reference to the Player's animator component.

    // Use this for initialization
    public void Start()
    {
        this.spawnController = FindObjectOfType<EnemySpawnController>();
        this.entityMovement = GetComponent<EntityMovement>();
        this.animator = animator = GetComponent<Animator>();
        dieSound = Resources.Load("Audio/monster_die") as AudioClip;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if (GameManager.instance.isPaused ())
			return;
		if (GameManager.instance.isBulletTime) {
			_body = gameObject.GetComponent<Rigidbody2D>();
			_body.velocity = Vector2.zero;
			return;
		}
        AIControl();
    }

    public virtual void AIControl()
    {
        float velocity = 1.0f;

        //Moving left so invert velocity
        if (!entityMovement.facingRight)
        {
            velocity *= -1;
        }

        entityMovement.Movement(velocity);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //Hit side wall so reverse direction of movement
        if (coll.gameObject.CompareTag("SideWall"))
        {
            entityMovement.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Hit side wall so reverse direction of movement
        if (other.gameObject.CompareTag("PlayerEnemyCollider") && !dead)
        {
            Player player = other.GetComponentInParent<Player>();
            this.animator = animator = GetComponent<Animator>();
            animator.SetTrigger("enemyAttack");
            player.takeDamageKnockBack(damageGiven, Mathf.Sign(player.transform.position.x - this.transform.position.x));

        }
    }

    public override void takeDamage(int damageReceived)
    {
        //basic decrementing health
        GameObject player = GameObject.FindWithTag("Player");
        knockBack(Mathf.Sign(this.transform.position.x - player.transform.position.x));
        currentHealth = currentHealth - damageReceived;
        if (currentHealth <= 0)
        {
            die();
        }
    }

    public override void die()
    {

       source.PlayOneShot(dieSound, ((float)GameControl.control.soundBitsVolume) / 100);
        GameControl.control.giveExperience(experienceGiven);
        dead = true;

        // ignores collision between dead enemy and player
        Collider2D collider = GetComponent<Collider2D>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(collider, player.GetComponent<Collider2D>());
        entityMovement.moveForce = 0F;
        damageGiven = 0;

        animator.SetBool("Dead", true);

        // kills the enemy after death animation
        //TODO: want to add flashing enemy or fade out
        StartCoroutine(delayDie());
    }

    IEnumerator delayDie()
    {
        // grabs the animation length of the death animation and waits for that many seconds
        float deathLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(deathLength);

        Destroy(gameObject);
	    if (!isSpecialLevel)
        {
        	spawnController.spawnCount--;
        	spawnController.OnDeathSpawn();
        	if (Random.Range(0, 2) == 0)
        	{
            	Instantiate(orb, gameObject.transform.position, gameObject.transform.rotation);
        	}
    	}
    }
    public void loopPowerup()
    {
        powerUp = true;
        if (entityMovement.maxSpeed < entityMovement.maxMaxSpeed)
        {
            entityMovement.maxSpeed += 5;
            entityMovement.moveForce += 15;
        }

        if (powerUp)
        {
            StartCoroutine(hideTrail());
        }
    }

    private void knockBack(float dir)
    {
        this.GetComponent<Rigidbody2D>().AddForce(new Vector2(knockBackStrength * dir, 0));
    }

    IEnumerator hideTrail()
    {
        this.trailControl = GetComponent<EnemyTrailControl>();
        trailControl.trail.enabled = false;
        yield return new WaitForSeconds(0.25f);
        trailControl.trail.enabled = true;
    }
}

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



    private Animator animator;                  //Used to store a reference to the Player's animator component.

    // Use this for initialization
    public void Start()
    {
        this.spawnController = FindObjectOfType<EnemySpawnController>();
        this.entityMovement = GetComponent<EntityMovement>();
        this.animator = animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (GameManager.instance.isPaused())
            return;
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
        if (other.gameObject.CompareTag("PlayerEnemyCollider"))
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
        GameControl.control.giveExperience(experienceGiven);
        dead = true;
        Destroy(gameObject);
	if (!isSpecialLevel){
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

using UnityEngine;

// Enforces these modules to be loaded up with this module when placed on a prefab/game object
[RequireComponent(typeof(EntityMovement))]


public class BaseEnemy : KillableEntityInterface {

    public EntityMovement entityMovement;
    public int damageGiven = 1;
    public GameObject orb;
    public int experienceGiven = 5;
    private EnemySpawnController spawnController;

    private Animator animator;                  //Used to store a reference to the Player's animator component.

    // Use this for initialization
    void Start () {
        this.spawnController = FindObjectOfType<EnemySpawnController>();
        this.entityMovement = GetComponent<EntityMovement>();
        this.animator = animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if (GameManager.instance.isPaused ())
			return;
        AIControl();


	}

     protected virtual void AIControl()
    {
         float velocity = 1.0f;

         //Moving left so invert velocity
        if(!entityMovement.facingRight)
        {
            velocity *= -1;
        }

        entityMovement.Movement(velocity);
    }

    private void OnCollisionEnter2D(Collision2D coll)
     {
        //Hit side wall so reverse direction of movement
        if(coll.gameObject.CompareTag("SideWall"))
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
            animator.SetTrigger("enemyAttack");
            player.hit(new Vector2(damageGiven, Mathf.Sign(player.transform.position.x - this.gameObject.transform.position.x)));
            Debug.Log(new Vector2(damageGiven, Mathf.Sign(player.transform.position.x - this.gameObject.transform.position.x)));
        }
    }

    public override void hit(Vector2 damageAndDirection)
    {
        knockBack(damageAndDirection.y);
        takeDamage((int)damageAndDirection.x);
    }

    private void knockBack(float dir)
    {
        Vector2 force = new Vector2(300 * dir, 200);
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
    }

    public override void takeDamage(int damageReceived)
    {
        //basic decrementing health
        currentHealth = currentHealth - damageReceived;
        if(currentHealth <= 0){
            die();
        }
    }

    public override void die()
    {
        GameControl.control.giveExperience(experienceGiven);
        dead = true;
        Destroy(gameObject);
        spawnController.spawn();
        if (Random.Range(0, 2) == 0)
        {
            Instantiate(orb, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

   
}

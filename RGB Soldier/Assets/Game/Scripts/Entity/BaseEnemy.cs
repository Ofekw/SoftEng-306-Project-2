using UnityEngine;

// Enforces these modules to be loaded up with this module when placed on a prefab/game object
[RequireComponent(typeof(EntityMovement))]


public class BaseEnemy : KillableEntityInterface {

    public EntityMovement entityMovement;
    public int damageGiven = 1;
    public GameObject orb;

    private Animator animator;                  //Used to store a reference to the Player's animator component.

    // Use this for initialization
    void Start () {
        this.entityMovement = GetComponent<EntityMovement>();
        this.animator = animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
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
            player.takeDamage(damageGiven);
           
        }
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
        dead = true;
        Destroy(gameObject);
        if (Random.Range(0, 2) == 0)
        {
            Instantiate(orb, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}

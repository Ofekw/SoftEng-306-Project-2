using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class EntityMovement : MonoBehaviour
{

    [HideInInspector]
    public bool facingRight = true;			// For determining which way the entity is currently facing.
    [HideInInspector]
    public bool jump = false;				// Condition for whether the entity should jump.


    public float moveForce = 500f;			// Amount of force added to move the entity left and right.
    public float maxSpeed = 10f;				// The fastest the entity can travel in the x axis.
    public float maxMaxSpeed = 20f;              //Absolute max speed after speed bonuses
    public AudioClip[] jumpClips;			// Array of clips for when the entity jumps.
    public float jumpForce = 150f;			// Amount of force added when the entity jumps.
    public int jumpCount = 0;               //Number of jumps made
    public float lastJumpTime;              //Time of last jump

    private Transform groundCheck;			// A position marking where to check if the entity is grounded.
    public bool grounded = true;			// Whether or not the entity is grounded.
    private Animator anim;					// Reference to the entity's animator component.

    // Use this for initialization
    void Start()
    {
        // Setting up references.
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
        lastJumpTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
		if (GameManager.instance.isPaused ())
			return;
        // The entity is grounded if a linecast to the groundcheck position hits anything on the ground layer.
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
       if (grounded && Time.time > (lastJumpTime + 0.5))
        {
            jumpCount = 0;
        }
    }

    //helper method, flags entity to jump at next movement call
    public void Jump()
    {
        //Check grounded, or has not jumped more than once and that hasnt jumped in last half second
        if ((grounded || jumpCount < 2) && Time.time > (lastJumpTime + 0.5))
        {
            lastJumpTime = Time.time;
            jumpCount++;
            this.jump = true;
        }
    }

    // Movement is called to move the entity based on it's horizontal velocity, this also processes movement animations and which way the sprite is facing
    public void Movement(float hVelocity)
    {
        // The Speed animator parameter is set to the absolute value of the horizontal input.
        //anim.SetFloat("Speed", Mathf.Abs(hVelocity));

        // If the entity is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
        if (hVelocity * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
            // ... add a force to the entity.
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * hVelocity * moveForce);

        // If the entity's horizontal velocity is greater than the maxSpeed...
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
            // ... set the entity's velocity to the maxSpeed in the x axis.
            GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        // If the input is moving the entity right and the entity is facing left...
        if (hVelocity > 0 && !facingRight)
            // ... flip the entity.
            Flip();
        // Otherwise if the input is moving the entity left and the entity is facing right...
        else if (hVelocity < 0 && facingRight)
            // ... flip the entity.
            Flip();

        // If the entity should jump...
        if (jump)
        {
            // Set the Jump animator trigger parameter.
            //anim.SetTrigger("Jump");

            // Play a random jump audio clip.
            int i = Random.Range(0, jumpClips.Length);
            //AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            // Add a vertical force to the entity.
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

            // Make sure the entity can't jump again until the jump conditions from Update are satisfied.
            jump = false;
        }
    }

    public void Flip()
    {
        // Switch the way the entity is labelled as facing.
        facingRight = !facingRight;

        // Multiply the entity's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

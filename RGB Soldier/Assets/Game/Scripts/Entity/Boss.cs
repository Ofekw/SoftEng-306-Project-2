using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(EntityMovement))]

public class Boss : KillableEntityInterface
{

    private Animator animator;
    bool isJumping = false;
    bool moveRight = false;
    bool moveLeft = false;
    public EntityMovement entityMovement;
    public Rigidbody2D projectile;


    public override void die()
    {
        throw new NotImplementedException();
    }

    public override void takeDamage(int damageReceived)
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start()
    {
        this.entityMovement = GetComponent<EntityMovement>();
        this.animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        //float hVelocity = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            moveLeft = true;
            animator.SetBool("isMovingLeft", true);
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            moveRight = true;
            animator.SetBool("isMovingRight", true);
        }

        if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            moveLeft = false;
            animator.SetBool("isMovingLeft", false);
        }

        if (Input.GetKeyUp(KeyCode.Keypad6))
        {
            moveRight = false;
            animator.SetBool("isMovingRight", false);
        }

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            entityMovement.Jump();
        }

        if (isJumping)
        {
            entityMovement.Jump();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            attack();
        }

        float hVelocity = 0f;
        if (moveRight && !moveLeft)
        {
            hVelocity = 1.0f;
        }
        else if (moveLeft && !moveRight)
        {
            hVelocity = -1.0f;
        }
        if (!moveRight && !moveLeft)
        {
            hVelocity = 0.0f;
        }

        //hVelocity = Input.GetAxis("Horizontal");
        //call the base movement module method to handle movement
        entityMovement.Movement(hVelocity);
    }

    void attack()
    {
        if ( animator.GetBool("isMovingRight"))
        {
            Rigidbody2D clone = (Rigidbody2D)Instantiate(projectile, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z), transform.rotation);
            //Set damage equal to dexterity stat
            clone.GetComponent<ProjectileScript>().damage = 1;
            //Set x speed 
            clone.velocity = new Vector2(100, 0);
        }
        else if (animator.GetBool("isMovingLeft"))
        {
            //Shoot to the left
            Rigidbody2D clone = (Rigidbody2D)Instantiate(projectile, new Vector3(transform.position.x - 5, transform.position.y, transform.position.z), transform.rotation);
            clone.GetComponent<ProjectileScript>().damage = 1;
            //Invert prefab
            Vector3 theScale = clone.transform.localScale;
            theScale.x *= -1;
            clone.transform.localScale = theScale;
            //Set x speed
            clone.velocity = new Vector2(-100, 0);
        }
    }
}
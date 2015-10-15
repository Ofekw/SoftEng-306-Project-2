using UnityEngine;
using System.Collections;
using System;

public class BlackOrbAttack : ProjectileScript
{

    public int speed;
    public int orbHitCount;
    public Boolean isGoingToPlayer = true;
    public Boolean startedRight;

    protected override void handleCollisonWithLayer(Collision2D hit, string layerTag)
    {
       
        if (layerTag == "Player" || layerTag == "Boss")
        {
            //Check if the player is attacking when it hits their box, if they are bounce the orb.
            if (layerTag == "Player" && (Time.time - hit.gameObject.GetComponent<Player>().lastAttack) < 0.25)
            {
                bounceOrb(hit, layerTag);
            } else
            {
                //If boss or player hit destroy orb
                hit.gameObject.SendMessage("takeDamage", damage);
                Destroy(this.gameObject);
            }
        }
        else if (layerTag == "PlayerProjectile" || layerTag == "PlayerMelee")
        {
            bounceOrb(hit, layerTag);
        } else if (layerTag == "BouncableOrb")
        {
            Destroy(this.gameObject);
            Destroy(hit.gameObject);
        }
    }

    private void bounceOrb(Collision2D hit, string layerTag)
    {
        if (layerTag == "PlayerProjectile")
        {
            Destroy(hit.gameObject);
            Instantiate(explosion, hit.gameObject.transform.position, hit.gameObject.transform.rotation);
            orbHitCount++;
        }
        else
        //must have been a melee collision
        {
            orbHitCount += 3;
        }
        Rigidbody2D body = this.GetComponent<Rigidbody2D>();
        //Takes three hits to rebound orb attack
        if (isGoingToPlayer && orbHitCount > 2)
        {
            //if the orb is moving left, move it right and vice versa
            if (!startedRight)
            {
                body.velocity = new Vector2(speed, 0);
            } else
            {
                body.velocity = new Vector2(-speed, 0);
            }
            isGoingToPlayer = false;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        else
        {
            //if the orb is moving right/left ensure it's speed is maintained and only if the orb is going to the player
            if (startedRight && isGoingToPlayer)
            {
                body.velocity = new Vector2(speed, 0);
            } else if(!startedRight && isGoingToPlayer)
            {
                body.velocity = new Vector2(-speed, 0);
            }
        }
    }

    void Update()
    {

    }

    void Start()
    {
        startedRight = BossProjectileSpawner.startedRight;
        speed = 15;
        orbHitCount = 0;
    }
}

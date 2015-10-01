using UnityEngine;
using System.Collections;
using System;

public class BlackOrbAttack : ProjectileScript
{

    public int speed;
    public int orbHitCount;
    public Boolean isGoingToPlayer = true;

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
                //If boss or player hit destroy orn
                hit.gameObject.SendMessage("takeDamage", damage);
                Destroy(this.gameObject);
            }
        }
        else if (layerTag == "PlayerProjectile" || layerTag == "PlayerMelee")
        {
            bounceOrb(hit, layerTag);
        }
    }

    private void bounceOrb(Collision2D hit, string layerTag)
    {
        if (layerTag == "PlayerProjectile")
        {
            Destroy(hit.gameObject);
            orbHitCount++;
        }
        else
        {
            orbHitCount += 3;
        }
        Rigidbody2D body = this.GetComponent<Rigidbody2D>();
        //Takes three hits to rebound orb attack
        if (isGoingToPlayer && orbHitCount > 2)
        {
            body.velocity = new Vector2(speed, 0);
            orbHitCount = 0;
            isGoingToPlayer = false;
        }
        else
        {
            body.velocity = new Vector2(-speed, 0);
        }
        body.angularVelocity *= -1;
    }

    void Update()
    {

    }

    void Start()
    {
        speed = 15;
        orbHitCount = 0;
    }
}

using UnityEngine;
using System.Collections;
using System;

public class UnblockableOrbAttack : ProjectileScript
{

    private Vector3 targetScale;
    private Vector3 baseScale;
    private float speed;
    public Boolean startScale;
    public Boolean isCorrectionDirection; 

    protected override void handleCollisonWithLayer(Collision2D hit, string layerTag)
    {
        if (layerTag == "Player")
        {
            print("Sending damage");
            hit.gameObject.SendMessage("takeDamage", damage);
            Destroy(this.gameObject);
        }
        else if (layerTag == "PlayerProjectile")
        {
            Destroy(hit.gameObject);
            Instantiate(explosion, hit.gameObject.transform.position, hit.gameObject.transform.rotation);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // Use this for initialization
    void Start () {
        targetScale = new Vector3(3f, 3f);
        baseScale = new Vector3(2f, 2f);
        startScale = true;
        speed = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (startScale)
        {
            
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, speed * Time.deltaTime);
            if (transform.localScale.x > (targetScale.x-1))
            {
                startScale = false;
                GetComponent<Animator>().SetBool("isCharging", false);
                if (BossProjectileSpawner.startedRight)
                {
                    Vector3 theScale = transform.localScale;
                    theScale.x *= -1;
                    transform.localScale = theScale;
                }
            }
        }
        else
        {
            int direction = 1;
            if (!BossProjectileSpawner.startedRight)
            {
                direction = -1;
            }
            Rigidbody2D body = this.gameObject.GetComponent<Rigidbody2D>();

            body.velocity = new Vector2(direction * 25, 0);
        }

    }
}

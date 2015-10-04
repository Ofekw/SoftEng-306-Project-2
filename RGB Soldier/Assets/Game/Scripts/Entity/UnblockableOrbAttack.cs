using UnityEngine;
using System.Collections;
using System;

public class UnblockableOrbAttack : ProjectileScript
{

    private Vector3 targetScale;
    private Vector3 baseScale;
    private int speed;

    protected override void handleCollisonWithLayer(Collision2D hit, string layerTag)
    {
        if (layerTag == "Player")
        {
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
        targetScale = new Vector3(20f, 20f);
        baseScale = new Vector3(2f, 2f);
        speed = 1;
	}

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, speed * Time.deltaTime);
    }
}

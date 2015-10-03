using UnityEngine;
using System.Collections;
using System;

public class ArrowProjectile : ProjectileScript {

    protected override void handleCollisonWithLayer(Collision2D hit, string layerTag)
    {
        if (layerTag == "Enemies")
        {
            hit.gameObject.SendMessage("takeDamage", damage);
            Destroy(this.gameObject);
            Instantiate(explosion, hit.gameObject.transform.position, hit.gameObject.transform.rotation);
        }
    }
}

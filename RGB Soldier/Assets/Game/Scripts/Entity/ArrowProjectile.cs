using UnityEngine;
using System.Collections;
using System;


public class ArrowProjectile : ProjectileScript {

    public GameObject damageIndicator;
    public Sprite[] dmg;

    protected override void handleCollisonWithLayer(Collision2D hit, string layerTag)
    {
        if (layerTag == "Enemies")
        {
            hit.gameObject.SendMessage("takeDamage", damage);
            Destroy(this.gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(damageIndicator, transform.position, transform.rotation);
            SpriteRenderer renderer = GameObject.Find("numeric-1").GetComponent<SpriteRenderer>();
            renderer.sprite = dmg[damageIndicator.gameObject.GetComponent<DamageIndicators>().CalculateRangedDamageIndicator()];
        } else if (layerTag == "bouncableOrb")
        {
            Destroy(this.gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}

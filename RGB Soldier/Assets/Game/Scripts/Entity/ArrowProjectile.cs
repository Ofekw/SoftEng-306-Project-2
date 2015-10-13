using UnityEngine;
using System.Collections;
using System;


public class ArrowProjectile : ProjectileScript {

    public GameObject damageIndicator;
    public Sprite[] dmg;
	private Vector2 origVel;

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
        }
    }

	void Start() {
		origVel = gameObject.GetComponent<Rigidbody2D> ().velocity;
	}

	void Update() {
		Rigidbody2D arrow = gameObject.GetComponent<Rigidbody2D> ();
		if (GameManager.instance.isBulletTime) {
			arrow.velocity = Vector2.zero;
		} else {
			arrow.velocity = origVel;
		}
	}
}

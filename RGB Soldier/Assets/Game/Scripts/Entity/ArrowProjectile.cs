using UnityEngine;
using System.Collections;
using System;


public class ArrowProjectile : ProjectileScript {

    public GameObject damageIndicator;
    public Sprite[] dmg;
	private int _direction;
	private Rigidbody2D _arrow;
	private bool _first = true;

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
        else if (layerTag == "bouncableOrb")
        {
            Destroy(this.gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
        else
        {
            Destroy(this.gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

	void Update() {
		if (_first) {
			//run through this code on first update. Can't use Start() for this as it somehow breaks things
			_first = false;
			_arrow = gameObject.GetComponent<Rigidbody2D> ();
			_direction = _arrow.velocity.x > 0 ? 1 : -1; //1 is right, -1 is left
		}
		if (GameManager.instance.isBulletTime) {
			_arrow.velocity = Vector2.zero;
		} else {
			_arrow.velocity = new Vector2(20*_direction, 0);
		}
	}
}

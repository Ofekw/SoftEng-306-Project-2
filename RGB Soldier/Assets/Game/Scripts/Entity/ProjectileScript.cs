using UnityEngine;
using System.Collections;
[RequireComponent(typeof(DamageIndicators))]

public class ProjectileScript : MonoBehaviour {

    public int damage = 1;
    public Vector3 startPoint;
    public GameObject shooter;
    public GameObject explosion;
    public GameObject damageIndicator;
    public Sprite[] dmg;

	void Start () {
        startPoint = this.gameObject.transform.position;
        shooter = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), shooter.gameObject.GetComponent<Collider2D>());
    }
	
	void Update () {
		if (GameManager.instance.isPaused ())
			return;
        if(this.GetComponent<Rigidbody2D>().velocity.magnitude < 5) {
            Destroy(this.gameObject);
        }
    }

    void Awake()
    {
        // load all frames in array
      //  Sprite[] dmg = Resources.LoadAll<Sprite>("damage");
    }

    void OnCollisionEnter2D(Collision2D hit) {

        Instantiate(explosion, transform.position, transform.rotation);
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Instantiate(damageIndicator, transform.position, transform.rotation);
            SpriteRenderer renderer = GameObject.Find("numeric-1").GetComponent<SpriteRenderer>();
            renderer.sprite = dmg[damageIndicator.gameObject.GetComponent<DamageIndicators>().CalculateRangedDamageIndicator()];
            
            hit.gameObject.SendMessage("takeDamage", damage);
            Destroy(this.gameObject);
        }
        if (hit.gameObject.tag == "ground" || hit.gameObject.tag == "SideWall")
        {
            Destroy(this.gameObject);
        }

    }
}
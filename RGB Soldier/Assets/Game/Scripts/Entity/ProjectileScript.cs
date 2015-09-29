using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public int damage = 1;
    public Vector3 startPoint;
    public GameObject shooter;

	void Start () {
        startPoint = this.gameObject.transform.position;
        shooter = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), shooter.gameObject.GetComponent<Collider2D>());
    }
	
	void Update () {
        if(this.GetComponent<Rigidbody2D>().velocity.magnitude < 5) {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D hit) {
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            hit.gameObject.SendMessage("takeDamage", damage);
            Destroy(this.gameObject);
        }
        if (hit.gameObject.tag == "ground" || hit.gameObject.tag == "SideWall")
        {
            Destroy(this.gameObject);
        }
    }
}
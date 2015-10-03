using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public int damage = 1;
    public Vector3 startPoint;
    public GameObject shooter;
    public GameObject explosion;

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

    void OnCollisionEnter2D(Collision2D hit) {

        Instantiate(explosion, transform.position, transform.rotation); 
        if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            hit.gameObject.SendMessage("hit", new Vector2(damage, Mathf.Sign(hit.transform.position.x - this.gameObject.transform.position.x)));
            //Destroy(this.gameObject);
        }
        if (hit.gameObject.tag == "ground" || hit.gameObject.tag == "SideWall")
        {
            Destroy(this.gameObject);
        }
    }
}
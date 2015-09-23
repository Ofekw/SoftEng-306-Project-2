using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public int range;
    public Vector3 startPoint;
    public GameObject shooter;

	void Start () {
        startPoint = this.gameObject.transform.position;
        shooter = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), shooter.gameObject.GetComponent<Collider2D>());
    }
	
	void Update () {
        //If we want a max range on projectiles
        if (Mathf.Abs(startPoint.x - this.gameObject.transform.position.x) > range){
            //Destroy(this.gameObject);
        }
        if(this.GetComponent<Rigidbody2D>().velocity.magnitude < 5) {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D hit) {
        if (hit.gameObject.tag == "Enemy")
        {
            hit.gameObject.SendMessage("takeDamage", 1);
            Destroy(this.gameObject);
        }
        if (hit.gameObject.tag == "ground" || hit.gameObject.tag == "SideWall")
        {
            Destroy(this.gameObject);
        }
    }
}
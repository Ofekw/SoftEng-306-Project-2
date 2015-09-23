using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public int range;
    public Vector3 startPoint;
    public GameObject shooter;

	// Use this for initialization
	void Start () {
        startPoint = this.gameObject.transform.position;
        shooter = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), shooter.gameObject.GetComponent<Collider2D>());
    }
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(startPoint.x - this.gameObject.transform.position.x) > range){
            //Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D hit) {
        if (hit.gameObject.tag == "Enemy")
        {
            hit.gameObject.SendMessage("takeDamage", 1);
            Destroy(this.gameObject);
        }
        if (hit.gameObject.tag == "ground")
        {
            Destroy(this.gameObject);
        }
       //Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D hit) {
        if (hit.gameObject.tag == "Enemy" && hit.gameObject != shooter)
        {
            //hit.gameObject.SendMessage("takeDamage", 1);
            //Destroy(this.gameObject);
        }
    }
}
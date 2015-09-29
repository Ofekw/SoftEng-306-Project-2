using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour
{

    public int damage;
    public Vector3 startPoint;
    public GameObject shooter;
    public string shooterTag;
    public string collidesWithTag;

    void Start()
    {
        startPoint = this.gameObject.transform.position;
        shooter = GameObject.FindGameObjectWithTag(shooterTag);
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), shooter.gameObject.GetComponent<Collider2D>());
    }

    void Update()
    {
        if (this.GetComponent<Rigidbody2D>().velocity.magnitude < 5)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        if (hit.gameObject.tag == collidesWithTag)
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
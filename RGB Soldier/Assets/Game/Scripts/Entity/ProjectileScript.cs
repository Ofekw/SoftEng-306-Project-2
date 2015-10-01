using UnityEngine;
using System.Collections;

public abstract class ProjectileScript : MonoBehaviour
{

    public int damage;
    public Vector3 startPoint;
    public GameObject shooter;
    public string shooterTag;
    public string collidesWithLayer;
    public System.Collections.Generic.List<string> collidesWithLayers = new System.Collections.Generic.List<string>();

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
            //Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D hit) { 
        foreach(string collidesWith in collidesWithLayers) {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer(collidesWith))
            {
                handleCollisonWithLayer(hit, collidesWith);
            }
        }
        //All objects will be destroyed when hitting ground or side wall
        if (hit.gameObject.tag == "ground" || hit.gameObject.tag == "SideWall")
        {
            Destroy(this.gameObject);
        }
    }

    protected abstract void handleCollisonWithLayer(Collision2D hit, string layerTag);
}
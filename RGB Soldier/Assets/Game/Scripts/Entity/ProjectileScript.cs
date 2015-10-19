using UnityEngine;
using System.Collections;
[RequireComponent(typeof(DamageIndicators))]

public abstract class ProjectileScript : MonoBehaviour
{
    public int damage;
    public Vector3 startPoint;
    public GameObject shooter;
    public GameObject explosion;
    public string shooterTag;
    public string collidesWithLayer;
    public System.Collections.Generic.List<string> collidesWithLayers = new System.Collections.Generic.List<string>();

    //Initialise
    void Start()
    {
        startPoint = this.gameObject.transform.position;
        shooter = GameObject.FindGameObjectWithTag(shooterTag);
        Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), shooter.gameObject.GetComponent<Collider2D>());
    }

    void Update() {
	    if (GameManager.instance.isPaused () || GameManager.instance.isBulletTime){
			    return;
        }
        //Destroy if slowed
        if(this.GetComponent<Rigidbody2D>().velocity.magnitude < 5) {
            if (this.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile"))
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Awake()
    {
        // load all frames in array
      //  Sprite[] dmg = Resources.LoadAll<Sprite>("damage");
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
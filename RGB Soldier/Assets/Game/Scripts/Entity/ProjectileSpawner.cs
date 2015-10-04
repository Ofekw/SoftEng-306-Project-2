using UnityEngine;
using System.Collections;

public abstract class ProjectileSpawner : MonoBehaviour {

    public int damage;
    public System.Collections.Generic.Dictionary<string, Rigidbody2D> projectiles;
    public float projectileSpeed;
    //in degree's per second
    public float angularVelocity;
    public int direction;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Method should be called in the entity, passing the attackName to be used and the direction the attack is to go.
    public void spawnProjectile(string attackName, float xPos, float yPos, float xOffSet, float yOffSet, bool isMovingRight)
    {
        direction = 1;
        if (!(isMovingRight))
        {
            direction = -1;
        }
        setAttackSettings(attackName, xPos, yPos);
        Rigidbody2D clone = (Rigidbody2D)Instantiate(projectiles[attackName], new Vector3(xPos + direction*xOffSet, yPos + yOffSet, 0), transform.rotation);
        //Set damage equal to dexterity stat
        clone.GetComponent<ProjectileScript>().damage = damage;
        //Set x speed 
        Vector3 theScale = clone.transform.localScale;
        theScale.x *= direction;
        clone.transform.localScale = theScale;

        clone.velocity = new Vector2(direction*projectileSpeed, 0);
        clone.angularVelocity = angularVelocity;
    }

    //Method should be overridden in child class to define damage and speed settings for different attacks
    public abstract void setAttackSettings(string attack, float xPos, float yPos);
}

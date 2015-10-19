using UnityEngine;
using System.Collections;

public class BossShield : MonoBehaviour {

    public int damage;

	// Use this for initialization
	void Start () {
        damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            //If player jumps into shield they should take damage.
            GameObject.FindGameObjectWithTag("Player").SendMessage("takeDamage", damage);
        } else if (coll.gameObject.CompareTag("BossProjectile") && !coll.gameObject.GetComponent<BlackOrbAttack>().isGoingToPlayer)
        {
            //take down boss shield
            GameObject.FindGameObjectWithTag("Boss").SendMessage("takeDownShield");
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.CompareTag("Projectile"))
        {
            Destroy(coll.gameObject);
        }
    }
}

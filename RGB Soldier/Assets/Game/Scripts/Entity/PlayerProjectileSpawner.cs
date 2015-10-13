using UnityEngine;
using System.Collections;
using System;

public class PlayerProjectileSpawner : ProjectileSpawner
{
    public Rigidbody2D projectile;
    public string attackName;

    public override void setAttackSettings(string attack, float xPos, float yPos)
    {
        //Should set damage and projectileSpeed variables based on the key
        //This method is called when calling spawnProjectile.
        if (attack == "arrowAttack")
        {
            damage = 1*this.gameObject.GetComponent<Player>().dexterity;
            projectileSpeed = 20;
        }
    }

    //Add attack's with their related game object here as below. The key is a string of the attack's name.
    void Start () {
        projectiles = new System.Collections.Generic.Dictionary<string, Rigidbody2D>();
        projectiles.Add(attackName, projectile);
	}
	
	// Update is called once per frame
	void Update () {

	}
}

using UnityEngine;
using System.Collections;

public class BossProjectileSpawner : ProjectileSpawner {

    public Rigidbody2D projectile;
    public string attackName;
    public int blackOrbSpeed = 15;

    public override void setAttackSettings(string attack)
    {
        //Should set damage and projectileSpeed variables based on the key
        //This method is called when calling spawnProjectile.
        if (attack == "blackOrbAttack")
        {
            damage = 2;
            projectileSpeed = blackOrbSpeed;
            angularVelocity = 360;
        }
    }

    //Add attack's with their related game object here as below. The key is a string of the attack's name.
    void Start()
    {
        projectiles = new System.Collections.Generic.Dictionary<string, Rigidbody2D>();
        projectiles.Add(attackName, projectile);
    }

}


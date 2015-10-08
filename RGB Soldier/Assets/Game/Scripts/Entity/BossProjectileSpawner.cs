using UnityEngine;
using System.Collections;
using System;

public class BossProjectileSpawner : ProjectileSpawner {

    public Rigidbody2D bounceOrb;
    private string bounceOrbName;
    public Rigidbody2D unblockableOrb;
    private string unblockAbleOrbName;
    private float blackOrbSpeed = 15;
    private float unblockableOrbSpeed = 0f;
    public static Boolean startedRight;

    public override void setAttackSettings(string attack, float xPos, float yPos)
    {
        //Should set damage and projectileSpeed variables based on the key
        //This method is called when calling spawnProjectile.
        if (xPos < 0)
        {
            startedRight = true;
        }
        else
        {
            startedRight = false;
        }
        if (attack == "blackOrbAttack")
        {
            damage = 1;
            projectileSpeed = blackOrbSpeed;
            angularVelocity = 0;
        } else if (attack == "unblockableAttack")
        {
            damage = 10;
            projectileSpeed = unblockableOrbSpeed;
            angularVelocity = 0;
        }
    }

    //Add attack's with their related game object here as below. The key is a string of the attack's name.
    void Start()
    {
        bounceOrbName = "blackOrbAttack";
        unblockAbleOrbName = "unblockableAttack";
        projectiles = new System.Collections.Generic.Dictionary<string, Rigidbody2D>();
        projectiles.Add(bounceOrbName, bounceOrb);
        projectiles.Add(unblockAbleOrbName, unblockableOrb);
    }

}


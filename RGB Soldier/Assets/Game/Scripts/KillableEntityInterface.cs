using UnityEngine;
using System.Collections;

public abstract class KillableEntityInterface : MonoBehaviour{
    public int maxHealth = 3;
    public int currentHealth = 3;
    public bool dead = false;			// Whether or not the enemy is dead.


    public abstract void takeDamage(int damageReceived);

    public abstract void die();
}

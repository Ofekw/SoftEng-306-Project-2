using UnityEngine;
using System.Collections;
[RequireComponent(typeof(EntityMovement))]
public class playerMultiplayer : MonoBehaviour {

    public EntityMovement entityMovement;
    public ProjectileSpawner projectileSpawner;
    public float projectileSpeed = 10;
    public float xProjectileOffset = 0f;
    public float yProjectileOffset = 0f;
    public bool attacking = false;
    public float attackCooldown = 0.3f;
    public float lastAttack;
    public float attackDuration = 0.2f;
    public BoxCollider2D meleeCollider;
    public float knockBackStrength = 500;

    public int strength = 3;    //Strength - Melee
    public int agility = 3;    //Agility- Speed
    public int dexterity = 3;   //Dexterity- Range
    public int vitality = 3;    //Vitality - Health

    public int abilityPoints; // Points to spend on skill

    public bool temporaryInvulnerable = false;
    public float temporaryInvulnerableTime;
    public float invulnTime = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;

public class Powerup : CollectibleEntity {

	private PowerupType type = PowerupType.ATTACK; // initialise type

	public PowerupType getType()
	{
		return type;
	}

	void Start()
	{
		switch (Random.Range (0, 3))
		{
		case 0: 
			type = PowerupType.AGILITY;
			break;
		case 1: 
			type = PowerupType.HEALTH;
			break;
		case 2: 
			type = PowerupType.ATTACK;
			break;
		case 3: 
			type = PowerupType.SPECIAL;
			break;
		}
	}

	public override void entityCollected()
	{
		Destroy(gameObject);
	}
	
	private void OnCollisionEnter2D(Collision2D coll)
	{
		//If player walks into orb call collection method
		if (coll.gameObject.CompareTag("Player"))
		{
			entityCollected();
		}
	}
}

public enum PowerupType
{
	AGILITY, HEALTH, ATTACK, SPECIAL		
}
using UnityEngine;

public class Powerup : CollectibleEntity {

	public PowerupType type; // initialise type

	public PowerupType getType()
	{
		return type;
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
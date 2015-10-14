using UnityEngine;

// Enforces these modules to be loaded up with this module when placed on a prefab/game object
[RequireComponent(typeof(EntityMovement))]
public class BulletTimeOrb : CollectibleEntity {

    private PowerupSpawnController spawnController;

    void Start()
    {
        spawnController = FindObjectOfType<PowerupSpawnController>();
    }

	public override void entityCollected()
	{
        spawnController.enableSpawner();
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

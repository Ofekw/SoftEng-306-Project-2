using UnityEngine;

public class BulletTimeOrb : CollectibleEntity {

    private BulletTimeSpawnController spawnController;

    void Start()
    {
        spawnController = FindObjectOfType<BulletTimeSpawnController>();
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

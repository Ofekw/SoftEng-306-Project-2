using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public bool flip;
    public GameObject enemySpawned;

    //Spawn Enemy type at spawner position
	public void Spawn()
    {
        GameObject clone = (GameObject)Instantiate(enemySpawned, this.transform.position, this.transform.rotation);
        if (flip)
        {
            clone.gameObject.GetComponent<EntityMovement>().Flip();
        }
    }
}

using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemySpawned;

    //Spawn Enemy type at spawner position
	public void Spawn()
    {
        Instantiate(enemySpawned, this.transform.position, this.transform.rotation);
    }
}

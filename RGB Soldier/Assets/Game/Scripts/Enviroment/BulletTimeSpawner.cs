using UnityEngine;
using System.Collections;

public class BulletTimeSpawner : MonoBehaviour
{
    public GameObject powerup;

    /// <summary>
    /// Spawns a Focus orb at the spawner position
    /// </summary>
    public void Spawn()
    {
        GameObject clone = (GameObject)Instantiate(powerup, transform.position, transform.rotation);
    }
}

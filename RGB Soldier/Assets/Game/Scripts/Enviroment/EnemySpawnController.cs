using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour
{

    public float spawnPeriod = 5f;
    public EnemySpawner[] spawners;
    private float spawnTimer = 0;
    private int spawnerToCall = 0;

    // Use this for initialization
    void Start()
    {
        if (spawners.Length == 0)
        {
            throw new UnityException("No spawners specified for: " + this.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
		if (GameManager.instance.isPaused () || GameManager.instance.isBulletTime)
			return;
        spawnTimer += Time.deltaTime; // Delta time is the time between frames, we increment this until we hit the spawn time

        if (spawnTimer > spawnPeriod)
        {
            spawnTimer = 0f;

            spawn();

        }
    }

    public void spawn()
    {
        spawnerToCall++;
        spawnerToCall = spawnerToCall % spawners.Length;

        spawners[spawnerToCall].Spawn();
    }
}

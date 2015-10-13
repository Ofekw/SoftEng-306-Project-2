using UnityEngine;
using System.Collections;

public class EnemySpawnController : MonoBehaviour
{

    public float spawnPeriod = 5f;
    public EnemySpawner[] spawners;
    private float spawnTimer = 0;
    private int spawnerToCall = 0;
    public int spawnCount = 0;

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
		if (GameManager.instance.isPaused ())
			return;
		if (GameManager.instance.isBulletTime) {
			spawnTimer -= Time.deltaTime; //counteract spawn timer increment
		}
        spawnTimer += Time.deltaTime; // Delta time is the time between frames, we increment this until we hit the spawn time

        if (spawnTimer > spawnPeriod || spawnCount == 0)
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
        spawnCount++;
    }

    public void OnDeathSpawn()
    {
        if (spawnCount < 10)
        {
            StartCoroutine(Wait());
        }

        spawnCount++;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        spawnerToCall++;
        spawnerToCall = spawnerToCall % spawners.Length;
        print("ON DEATH SPAWN");
        spawners[spawnerToCall].OnDeathSpawn();
    }
}

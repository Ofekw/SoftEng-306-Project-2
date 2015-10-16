using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BulletTimeSpawnController : MonoBehaviour
{

    public float spawnPeriod = 30f;
    public BulletTimeSpawner[] spawners;
    public GameObject focus;
    private float spawnTimer = 0;
    private int spawnerToCall = 0;
    private bool _canSpawn = true;  // when false spawnTimer doesn't increase and no new orbs can spawn
    public GameObject focus;

    //reset the spawn timer and allow new powerups to spawn. called by BulletTimeOrb when collected
    public void enableSpawner()
    {
        _canSpawn = true;
        spawnTimer = 0f;
    }

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
        if (GameManager.instance.isPaused())
            return;
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnPeriod && _canSpawn )
        {
            spawnTimer = 0f;
            spawn();
            _canSpawn = false;
        }
    }

    public void spawn()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(focus, player.transform.position, player.transform.rotation);
        spawnerToCall = Random.Range(0, spawners.Length);  // pick random spawner for powerup
        spawners[spawnerToCall].Spawn();
    }
}


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class BulletTime : Objective
{
    private GameObject player;
    public NormalEnemy enemyPrefab;
    private List<NormalEnemy> enemies = new List<NormalEnemy>();
    public BulletTimeSpawnController powerupSpawnController;

    public GameObject jumpButton;
    public GameObject meleeButton;
    public GameObject rangeButton;
    public GameObject joystick;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        BulletTimeSpawner powerDrop = powerupSpawnController.spawners[0];
        powerDrop.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5);
        enemyPrefab.GetComponent<EntityMovement>().moveForce = 60;
        enemyPrefab.GetComponent<EntityMovement>().maxSpeed = 5f;
        enemies.Add((NormalEnemy)Instantiate(enemyPrefab, new Vector3(player.transform.position.x - 13, player.transform.position.y), this.transform.rotation));
        enemies.Add((NormalEnemy)Instantiate(enemyPrefab, new Vector3(player.transform.position.x - 15, player.transform.position.y), this.transform.rotation));
        enemyPrefab.GetComponent<EntityMovement>().moveForce = 0;
        enemyPrefab.GetComponent<EntityMovement>().maxSpeed = 0;
        powerDrop.Spawn();

        jumpButton.SetActive(true);
        meleeButton.SetActive(true);
        rangeButton.SetActive(true);
        joystick.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(2);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(2);
        }
        jumpButton.SetActive(false);
        meleeButton.SetActive(false);
        rangeButton.SetActive(false);
        joystick.GetComponent<Image>().enabled = false;
    }

    public override bool isCompleted()
    {
        foreach (NormalEnemy enemy in enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return (true);
    }

    public override string getDescription()
    {
        return "When you see this animation, a Focus PowerUp has been drop. \n Pick it up to stop time.";
    }
}

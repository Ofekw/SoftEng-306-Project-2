using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class BulletTime : Objective
{
    private GameObject player;
    public Zombie enemyPrefab;
    private List<Zombie> enemies = new List<Zombie>();
    public PowerupSpawnController powerupSpawnController;

    public GameObject jumpButton;
    public GameObject meleeButton;
    public GameObject rangeButton;
    public GameObject joyStick;
    public Joystick joyStickScript;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        PowerupSpawner powerDrop = powerupSpawnController.spawners[0];
        powerDrop.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5);
        enemyPrefab.GetComponent<EntityMovement>().moveForce = 60;
        enemyPrefab.GetComponent<EntityMovement>().maxSpeed = 5f;
        enemies.Add((Zombie)Instantiate(enemyPrefab, new Vector3(player.transform.position.x - 13, player.transform.position.y), this.transform.rotation));
        enemies.Add((Zombie)Instantiate(enemyPrefab, new Vector3(player.transform.position.x - 15, player.transform.position.y), this.transform.rotation));
        enemyPrefab.GetComponent<EntityMovement>().moveForce = 0;
        enemyPrefab.GetComponent<EntityMovement>().maxSpeed = 0;
        powerDrop.Spawn();

        jumpButton.SetActive(true);
        meleeButton.SetActive(true);
        rangeButton.SetActive(true);
        joyStick.SetActive(true);
        yield return new WaitForSeconds(2);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(2);
        }
        jumpButton.SetActive(false);
        meleeButton.SetActive(false);
        rangeButton.SetActive(false);
        joyStick.SetActive(false);
    }

    public override bool isCompleted()
    {
        foreach (Zombie zombie in enemies){
            if (zombie != null){
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

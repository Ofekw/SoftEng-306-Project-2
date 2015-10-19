using UnityEngine;
using System.Collections;

/* 
 * This tutorial teaches the player about the melee attack
 */
public class Melee : Objective
{
    public GameObject meleeButton;
    public GameObject enemyPrefab;
    private GameObject player;
    private GameObject enemy;

    public override IEnumerator startObjective()
    {
        //Enables the melee button
        meleeButton.SetActive(true);
        player = GameObject.FindWithTag("Player");
        //Checks that the player faces the right way
        if (player.transform.localScale.x > 0)
        {
            player.GetComponent<Player>().entityMovement.Flip();
        }
        //Spawns an enemy infront of the player
        enemy = (GameObject)Instantiate(enemyPrefab , new Vector3(player.transform.position.x+3, player.transform.position.y), this.transform.rotation);
        yield return new WaitForSeconds(2);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(2);
        }
        //Disables the melee button
        meleeButton.SetActive(false);
    }

    public override bool isCompleted()
    {
        //Checks if the player killed the enemy
        return (enemy == null);
    }

    public override string getDescription()
    {
        return "Use your melee to kill the zombie";
    }
}

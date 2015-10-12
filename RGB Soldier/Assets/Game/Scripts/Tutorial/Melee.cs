using UnityEngine;
using System.Collections;

public class Melee : Objective
{
    public GameObject meleeButton;
    public GameObject enemyPrefab;
    private GameObject player;
    private GameObject enemy;

    public override IEnumerator startObjective()
    {
        meleeButton.SetActive(true);
        player = GameObject.FindWithTag("Player");
        Vector3 position = new Vector3(player.transform.position.x+3, player.transform.position.y);
        enemy = (GameObject)Instantiate(enemyPrefab , position, this.transform.rotation);
        yield return new WaitForSeconds(2);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(2);
        }
        meleeButton.SetActive(false);
    }

    public override bool isCompleted()
    {
        return (enemy == null);
    }

    public override string getDescription()
    {
        return "Use your melee to kill the zombie";
    }
}

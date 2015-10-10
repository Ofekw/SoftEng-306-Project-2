using UnityEngine;
using System.Collections;

public class Special : Objective {
    public GameObject enemyPrefab;
    private GameObject player;
    private GameObject enemy1;
    private GameObject enemy2;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        GameManager.instance.specialCharge = 1000;
        Vector3 position1 = new Vector3(player.transform.position.x + 10, player.transform.position.y);
        Vector3 position2 = new Vector3(player.transform.position.x + 5, player.transform.position.y);
        enemy1 = (GameObject)Instantiate(enemyPrefab, position1, this.transform.rotation);
        enemy2 = (GameObject)Instantiate(enemyPrefab, position2, this.transform.rotation);
        yield return new WaitForSeconds(2);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(2);
        }
    }

    public override bool isCompleted()
    {
        return (enemy1 == null && enemy2 == null);
    }

    public override string getDescription()
    {
        return "Shake your phone to use your special attack";
    }
}

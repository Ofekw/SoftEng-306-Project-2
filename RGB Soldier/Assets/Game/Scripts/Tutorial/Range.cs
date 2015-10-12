using UnityEngine;
using System.Collections;

public class Range : Objective
{
    public GameObject rangeButton;
    public GameObject enemyPrefab;
    private GameObject player;
    private GameObject enemy;

    public override IEnumerator startObjective()
    {
        rangeButton.SetActive(true);
        player = GameObject.FindWithTag("Player");
        Vector3 position = new Vector3(player.transform.position.x + 10, player.transform.position.y);
        enemy = (GameObject)Instantiate(enemyPrefab, position, this.transform.rotation);
        yield return new WaitForSeconds(2);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(2);
        }
        rangeButton.SetActive(false);
    }

    public override bool isCompleted()
    {
        return (enemy == null);
    }

    public override string getDescription()
    {
        return "Use your range attack to kill the zombie";
    }
}

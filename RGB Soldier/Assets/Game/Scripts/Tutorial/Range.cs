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
        if (player.transform.localScale.x > 0)
        {
            player.GetComponent<Player>().entityMovement.Flip();
        }
        enemy = (GameObject)Instantiate(enemyPrefab, new Vector3(player.transform.position.x + 10, player.transform.position.y), this.transform.rotation);
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

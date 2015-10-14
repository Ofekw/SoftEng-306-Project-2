using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Special : Objective {
    public GameObject stageTextObject;
    public GameObject enemyPrefab;
    private GameObject player;
    private GameObject enemy1;
    private GameObject enemy2;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        Text stageText = stageTextObject.GetComponent<Text>();
        stageText.text = "When the middle bar is full, your special is ready";
        yield return new WaitForSeconds(2);
        stageText.text = getDescription();
        Vector3 position1 = new Vector3(player.transform.position.x - 10, player.transform.position.y);
        Vector3 position2 = new Vector3(player.transform.position.x - 5, player.transform.position.y);
        enemy1 = (GameObject)Instantiate(enemyPrefab, position1, this.transform.rotation);
        enemy2 = (GameObject)Instantiate(enemyPrefab, position2, this.transform.rotation);
        GameManager.instance.specialCharge = 1000;
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

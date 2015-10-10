using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Looper : Objective
{
    public GameObject stageTextObject;
    private GameObject player;
    private float required_y = (float)-0.8211294;
    private string description = "";

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        while (player.transform.position.x < 19)
        {
            player.transform.position = new Vector3(player.transform.position.x + (float)0.1, player.transform.position.y, player.transform.position.z); ;
            yield return new WaitForSeconds((float)0.01);
        }
        Text stageText = stageTextObject.GetComponent<Text>();
        stageText.text = "Drop down the gap to respawn above";
        GameControl.control.playerAgl = 2;
        while (!isCompleted())
        {
            yield return new WaitForSeconds(2);
        }
        stageText.text = "Remember! Enemies get a speed boost when they drop into a gap";
        yield return new WaitForSeconds(2);
        GameControl.control.playerAgl = 0;
    }

    public override bool isCompleted()
    {
        return (player.transform.position.y == required_y);
    }

    public override string getDescription()
    {
        return description;
    }
}

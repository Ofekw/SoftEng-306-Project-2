using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Looper : Objective
{
    public GameObject stageTextObject;
    private GameObject player;
    private float required_y = (float)1.6788;
    private string description = "";
    public GameObject joyStick;

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
        joyStick.SetActive(true);
        while (!isCompleted())
        {
            yield return new WaitForSeconds((float)0.1);
        }
        joyStick.SetActive(false);
        stageText.text = "Remember! Enemies get a speed boost when they drop into a gap";
        yield return new WaitForSeconds(3);
    }

    public override bool isCompleted()
    {
        return (player.transform.position.y >= required_y);
    }

    public override string getDescription()
    {
        return description;
    }
}

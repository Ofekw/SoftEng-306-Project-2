using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Looper : Objective
{
    public GameObject stageTextObject;
    private Player player;
    private float required_y = (float)1.6788;
    public GameObject joyStickObject;
    public Joystick joyStick;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        while (player.transform.position.x < 18)
        {
            joyStick.UpdateVirtualAxes(new Vector3(129, 59));
            yield return new WaitForSeconds((float)0.01);
        }
        joyStick.UpdateVirtualAxes(new Vector3(94, 59));
        
        Text stageText = stageTextObject.GetComponent<Text>();
        stageText.text = "Drop down the gap to respawn above";
        joyStickObject.SetActive(true);
        while (!isCompleted())
        {
            yield return new WaitForSeconds((float)0.1);
        }
        joyStickObject.SetActive(false);
        stageText.text = "Remember! Enemies get a speed boost when they drop into a gap";
        yield return new WaitForSeconds(3);
    }

    public override bool isCompleted()
    {
        return (player.transform.position.y >= required_y);
    }

    public override string getDescription()
    {
        return "";
    }
}

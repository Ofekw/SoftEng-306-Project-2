using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Looper : Objective
{
    public GameObject stageTextObject;
    private Player player;
    private float required_y = (float)1.6788;
    public GameObject joystick;
    private Joystick joyStickScript;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        joyStickScript = joystick.GetComponent<Joystick>();
        while (player.transform.position.x < 18)
        {
            joyStickScript.UpdateVirtualAxes(new Vector3(joyStickScript.m_StartPos.x + 20, joyStickScript.m_StartPos.y));
            yield return new WaitForSeconds((float)0.01);
        }
        joyStickScript.OnPointerUp(null);
        
        Text stageText = stageTextObject.GetComponent<Text>();
        stageText.text = "Drop down the gap to respawn above";
        joystick.GetComponent<Image>().enabled = true;
        while (!isCompleted())
        {
            yield return new WaitForSeconds((float)0.1);
        }
        joystick.GetComponent<Image>().enabled = false;
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

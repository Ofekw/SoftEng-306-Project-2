using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : Objective
{
    private GameObject player;
    private float initial_x;
    public GameObject joystick;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        initial_x = player.transform.position.x;
        joystick.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(1);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(1);
        }
        joystick.GetComponent<Image>().enabled = false;
    }

    public override bool isCompleted()
    {
        float current_x = player.transform.position.x;
        return (current_x > initial_x) || (initial_x > current_x);
    }

    public override string getDescription()
    {
        return "Use joystick to move player.";
    }
}

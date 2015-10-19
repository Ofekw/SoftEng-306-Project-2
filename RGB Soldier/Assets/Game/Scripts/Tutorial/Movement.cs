using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

/* 
 * This tutorial teaches the player about movement
 */
public class Movement : Objective
{
    private GameObject player;
    private float initial_x;
    public GameObject joystick;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        initial_x = player.transform.position.x;
        //Enables the joystick
        joystick.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(1);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(1);
        }
        //Disables the joystick
        joystick.GetComponent<Image>().enabled = false;
    }

    public override bool isCompleted()
    {
        //Checks if the player has moved position or not
        float current_x = player.transform.position.x;
        return (current_x > initial_x) || (initial_x > current_x);
    }

    public override string getDescription()
    {
        return "Use joystick to move player.";
    }
}

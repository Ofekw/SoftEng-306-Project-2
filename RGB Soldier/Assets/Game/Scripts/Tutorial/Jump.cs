using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/* 
 * This tutorial is teach players about the jump mechanics. 
 */
public class Jump : Objective {
    private GameObject player;
    private float required_x = (float)-12;
    public GameObject jumpButton;
    public GameObject joystick;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        //Enables the buttons
        joystick.GetComponent<Image>().enabled = true;
        jumpButton.SetActive(true);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(1);
        }
        //Disables the buttons
        jumpButton.SetActive(false);
        joystick.GetComponent<Image>().enabled = false;
    }

    public override bool isCompleted()
    {
        //The player has to double jump to reach the high platform
        return (player.transform.position.x  > required_x);
    }

    public override string getDescription()
    {
        return "Tap the Up Arrow twice to double jump";
    }
}

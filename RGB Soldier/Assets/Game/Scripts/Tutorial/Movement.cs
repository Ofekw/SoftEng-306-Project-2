using UnityEngine;
using System.Collections;

public class Movement : Objective
{
    private GameObject player;
    private float initial_x;
    public GameObject joyStick;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        initial_x = player.transform.position.x;
        joyStick.SetActive(true);
        yield return new WaitForSeconds(1);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(1);
        }
        joyStick.SetActive(false);
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

using UnityEngine;
using System.Collections;

public class Jump : Objective {
    private GameObject player;
    private float required_x = (float)-12;
    public GameObject jumpButton;
    public GameObject joyStick;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        joyStick.SetActive(true);
        jumpButton.SetActive(true);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(1);
        }
        jumpButton.SetActive(false);
        joyStick.SetActive(false);
    }

    public override bool isCompleted()
    {
        return (player.transform.position.x  > required_x);
    }

    public override string getDescription()
    {
        return "Tap the Up Arrow twice to double jump";
    }
}

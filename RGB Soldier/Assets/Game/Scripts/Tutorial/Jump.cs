using UnityEngine;
using System.Collections;

public class Jump : Objective {
    private GameObject player;
    private float required_x = (float)-12;
    public GameObject jumpButton;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        GameControl.control.playerAgl = 2;
        jumpButton.SetActive(true);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(1);
        }
        jumpButton.SetActive(false);
        GameControl.control.playerAgl = 0;
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

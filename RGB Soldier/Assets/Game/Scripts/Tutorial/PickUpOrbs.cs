using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PickUpOrbs : Objective {
    public GameObject orbPrefab;
    private GameObject player;
    private GameObject orb;
    public GameObject joystick;
    private Joystick joyStickScript;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        joyStickScript = joystick.GetComponent<Joystick>();
        orb = (GameObject)Instantiate(orbPrefab, new Vector3(player.transform.position.x - 6, player.transform.position.y + 1), player.transform.rotation);
        joystick.GetComponent<Image>().enabled = true;
        yield return new WaitForSeconds(2);
        while (!isCompleted())
        {
            yield return new WaitForSeconds(2);
        }
    }

    public override bool isCompleted()
    {
        return (false);
    }

    public override string getDescription()
    {
        return "Pick up the orb to end the level";
    }

}

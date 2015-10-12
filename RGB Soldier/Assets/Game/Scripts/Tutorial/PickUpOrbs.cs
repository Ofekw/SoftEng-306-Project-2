using UnityEngine;
using System.Collections;

public class PickUpOrbs : Objective {
    public GameObject orbPrefab;
    private GameObject player;
    private GameObject orb;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        Vector3 position = new Vector3(player.transform.position.x - 6, player.transform.position.y + 1);
        orb = (GameObject)Instantiate(orbPrefab, position, player.transform.rotation);
        GameControl.control.playerAgl = 1;
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

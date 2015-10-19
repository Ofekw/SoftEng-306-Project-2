using UnityEngine;
using System.Collections;

public class CharacterChanger : MonoBehaviour {

    public string characterName;
    public GameObject character;
    public SkinnedMeshRenderer render;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        for (var i = 1; i < 4; i++)
        {
            if (i != GameControl.control.selectedCharacter)
            {
                characterName = "Player" + i;
                character = GameObject.Find(characterName);
                character.transform.FindChild("p_sotai").GetComponent<SkinnedMeshRenderer>().enabled = false;
            }
            else
            {
                characterName = "Player" + GameControl.control.selectedCharacter;
                character = GameObject.Find(characterName);
                character.transform.FindChild("p_sotai").GetComponent<SkinnedMeshRenderer>().enabled = true;
            }
        }
        GameControl.control.playerSprite = GameControl.control.selectedCharacter;
	}

    public void switchUp()
    {
        if (GameControl.control.selectedCharacter < 3)
            GameControl.control.selectedCharacter++;
        else
            GameControl.control.selectedCharacter = 1;
    }

    public void switchDown()
    {
        if (GameControl.control.selectedCharacter > 1)
        {
            GameControl.control.selectedCharacter = GameControl.control.selectedCharacter - 1;
        }
        else
            GameControl.control.selectedCharacter = 3;
    }

}

using UnityEngine;
using System.Collections;

public class CharacterSwitcher : MonoBehaviour {

    public string characterName;
    public GameObject character;
    public SkinnedMeshRenderer render;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameControl.control.selectedCharacter < 2)
                GameControl.control.selectedCharacter++;
            else
                GameControl.control.selectedCharacter = 1;
        }

        for (var i = 1; i < 3; i++)
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

    }
}

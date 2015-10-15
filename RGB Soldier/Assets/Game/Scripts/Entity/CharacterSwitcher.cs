using UnityEngine;
using System.Collections;

public class CharacterSwitcher : MonoBehaviour {

    public string characterName;
    public int selectedCharacter = 1;
    public GameObject character;
    public SkinnedMeshRenderer render;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedCharacter < 2)
                selectedCharacter++;
            else
                selectedCharacter = 1;
        }

        for (var i = 1; i < 3; i++)
        {
            if (i != selectedCharacter)
            {
                Debug.Log("1");
                characterName = "Player" + i;
                character = GameObject.Find(characterName);
                character.transform.FindChild("p_sotai").GetComponent<SkinnedMeshRenderer>().enabled = false;
            }
            else
            {
                Debug.Log("2");
                characterName = "Player" + selectedCharacter;
                character = GameObject.Find(characterName);
                character.transform.FindChild("p_sotai").GetComponent<SkinnedMeshRenderer>().enabled = true;
            }

        }

    }
}

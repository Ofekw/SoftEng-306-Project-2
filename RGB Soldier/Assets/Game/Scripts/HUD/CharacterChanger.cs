using UnityEngine;
using System.Collections;

public class CharacterChanger : MonoBehaviour {

    public string characterName;
    public int selectedCharacter = 1;
    public GameObject character;
    public SkinnedMeshRenderer render;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        for (var i = 1; i < 4; i++)
        {
            if (i != selectedCharacter)
            {
                characterName = "Player" + i;
                character = GameObject.Find(characterName);
                character.transform.FindChild("p_sotai").GetComponent<SkinnedMeshRenderer>().enabled = false;
            }
            else
            {
                characterName = "Player" + selectedCharacter;
                character = GameObject.Find(characterName);
                character.transform.FindChild("p_sotai").GetComponent<SkinnedMeshRenderer>().enabled = true;
            }
        }
	}

    public void switchUp()
    {
        if (selectedCharacter < 3)
            selectedCharacter++;
        else
            selectedCharacter = 1;
    }

    public void switchDown()
    {
        Debug.Log("LIEKRER");
        if (selectedCharacter > 1)
        {
            selectedCharacter = selectedCharacter - 1;
        }
        else
            selectedCharacter = 3;
    }

}

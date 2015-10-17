using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowPlayerInfo : MonoBehaviour {

    public Button theButton;

	// Use this for initialization
	void Start () {
        string level = GameControl.control.playerLevel.ToString();
        string agl = GameControl.control.playerAgl.ToString();
        string dex = GameControl.control.playerDex.ToString();
        string intel = GameControl.control.playerInt.ToString();
        string str = GameControl.control.playerStr.ToString();
        string vit = GameControl.control.playerVit.ToString();
        string currentLevel = GameControl.control.currentGameLevel.ToString();
        string abilityPoint = GameControl.control.abilityPoints.ToString();

        string toPrint = level + " " + agl + " " + dex + " " + intel + " " + str + " " + vit + " " + currentLevel + " " + abilityPoint;
        theButton = GetComponent<Button>();
        theButton.GetComponentInChildren<Text>().text = toPrint;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onPress()
    {
        GameControl.control.playerLevel += 1;
        GameControl.control.playerAgl += 1;
        GameControl.control.playerDex += 1;
        GameControl.control.playerInt += 1;
        GameControl.control.playerStr += 1;
        GameControl.control.playerVit += 1;
        GameControl.control.currentGameLevel += 1;
        GameControl.control.abilityPoints += 1;
    }
}

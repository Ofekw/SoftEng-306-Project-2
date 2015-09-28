using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TemporaryCheckScript : MonoBehaviour {

    public int playerLevel;
    public int playerExp;
    public int playerStr;
    public int playerAgl;
    public int playerDex;
    public int playerInt;
    public int playerVit;
    public int currentGameLevel;
    public int abilityPoints;

    public Button theButton;


	// Use this for initialization
	void Start () {
        playerLevel = GameControl.control.playerLevel;
        playerExp = GameControl.control.playerExp;
        playerStr = GameControl.control.playerStr;
        playerAgl = GameControl.control.playerAgl;
        playerDex = GameControl.control.playerDex;
        playerInt = GameControl.control.playerInt;
        playerVit = GameControl.control.playerVit;
        abilityPoints = GameControl.control.abilityPoints;

        theButton = GetComponent<Button>();
        theButton.GetComponentInChildren<Text>().text = playerLevel.ToString() + " " + playerExp.ToString() + " " + abilityPoints.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void load()
    {
        GameControl.control.Save();
        GameControl.control.Load();

        playerLevel = GameControl.control.playerLevel;
        playerExp = GameControl.control.playerExp;
        playerStr = GameControl.control.playerStr;
        playerAgl = GameControl.control.playerAgl;
        playerDex = GameControl.control.playerDex;
        playerInt = GameControl.control.playerInt;
        playerVit = GameControl.control.playerVit;
        abilityPoints = GameControl.control.abilityPoints;

        theButton.GetComponentInChildren<Text>().text = playerLevel.ToString() + " " + playerExp.ToString() + " " + abilityPoints.ToString();
    }
}

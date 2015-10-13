using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlockStages : MonoBehaviour {

    public Sprite unlockImage;
    public Button[] stageButtons;
    public GameObject[] stageTexts;

 
	// Use this for initialization
	void Start () {
        int gameLevel = GameControl.control.currentGameLevel;
        //if (gameLevel <= 0)
       // {
      //      GameControl.control.currentGameLevel = 1;
     //   }

        // for all unlocked stages - show level stage number 
        for (int i = 0; i < gameLevel; i++)
        {
            stageButtons[i].image.sprite = unlockImage;
            stageButtons[i].interactable = true;
            stageTexts[i].SetActive(true);
           
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

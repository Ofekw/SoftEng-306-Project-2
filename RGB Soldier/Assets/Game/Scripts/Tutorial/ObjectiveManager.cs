using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

/* This class manages all the objectives in the tutorial level 
 */
public class ObjectiveManager : MonoBehaviour
{
    //A list of objectives 
    public List<Objective> objectives = new List<Objective>();
    public GameObject stageTextObject;
    public GameObject meleeButton;
    public GameObject rangeButton;
    public GameObject jumpButton;
    public GameObject joystick;
    private Joystick joyStickScript;
    private Text stageText;

    //Disables all the HUD buttons and sets the stage text. Then starts the objectives
    void Start()
    {
        joyStickScript = joystick.GetComponent<Joystick>();
        joystick.GetComponent<Image>().enabled = false;
        meleeButton.SetActive(false);
        rangeButton.SetActive(false);
        jumpButton.SetActive(false); 
        stageText = stageTextObject.GetComponent<Text>();
        stageText.text = "Tutorial";
        stageTextObject.SetActive(true);
        Invoke("startTutorial", 2);
    }

    //Starts the tutorial
    private void startTutorial()
    {
        StartCoroutine(runObjectives());
    }

    //Runs all the objectives in the list
    private IEnumerator runObjectives()
    {
        foreach (Objective obj in objectives)
        {
            stageText.fontSize = 20;
            stageText.text = obj.getDescription();
            yield return StartCoroutine(obj.startObjective());
            stageText.text = "Well Done!";
            yield return new WaitForSeconds(2);
        }
        stageTextObject.SetActive(false);
    }
}

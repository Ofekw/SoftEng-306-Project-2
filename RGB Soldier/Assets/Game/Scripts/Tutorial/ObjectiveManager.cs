using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveManager : MonoBehaviour
{
    //A list of objectives 
    public List<Objective> objectives = new List<Objective>();
    public GameObject stageTextObject;
    public GameObject gameControl;
    public GameObject meleeButton;
    public GameObject rangeButton;
    public GameObject jumpButton;
    public GameObject joyStick;
    private Text stageText;
    //private Objective current;

    void Start()
    {
        joyStick.SetActive(false);
        meleeButton.SetActive(false);
        rangeButton.SetActive(false);
        jumpButton.SetActive(false);
        stageText = stageTextObject.GetComponent<Text>();
        stageText.text = "Tutorial";
        stageTextObject.SetActive(true);
        Invoke("startTutorial", 2);
    }

    private void startTutorial()
    {
        StartCoroutine(runObjectives());
    }

    private IEnumerator runObjectives()
    {
        foreach (Objective obj in objectives)
        {
            stageText.fontSize = 20;
            stageText.text = obj.getDescription();
            yield return StartCoroutine(obj.startObjective());
            stageText.text = "Well Done!!";
            yield return new WaitForSeconds(2);
        }
        stageTextObject.SetActive(false);
    }
}

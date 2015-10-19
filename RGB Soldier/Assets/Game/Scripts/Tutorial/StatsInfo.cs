using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/* 
 * This tutorial teaches the player about the stats
 */
public class StatsInfo : Objective {
    private GameObject player;
    public GameObject stageTextObject;
    public GameObject infoPanel;
    public GameObject pauseTutorialText;
    public Button pauseButton;
    public GameObject highlight;

    public override IEnumerator startObjective()
    {
        player = GameObject.FindWithTag("Player");
        Text stageText = stageTextObject.GetComponent<Text>();
        yield return new WaitForSeconds(5);
        stageText.text = "";
        //Invokes the pause button to show the stats 
        pauseButton.onClick.Invoke();
        pauseTutorialText.SetActive(true);
        while (!isCompleted())
        {
            highlight.SetActive(true);
            yield return StartCoroutine(WaitForRealSeconds((float)0.5));
            highlight.SetActive(false);
            yield return StartCoroutine(WaitForRealSeconds((float)0.5));
        }
        Text tutorialText = pauseTutorialText.GetComponent<Text>();
        tutorialText.text = "Press the Play button to resume the game";
        yield return new WaitForSeconds((float)0.01);
        pauseTutorialText.SetActive(false);
    }

    /*
     * This method is used to wait for a certain amount of time 
     * Obtained from http://forum.unity3d.com/threads/waitforrealseconds-solved.31346/
    */
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }

    public override bool isCompleted()
    {
        //Checks if the player has seen the stats info
        return (infoPanel.activeSelf);
    }

    public override string getDescription()
    {
        return "Enemies give you experience when you kill them. During any time, you may increase your stats";
    }
}

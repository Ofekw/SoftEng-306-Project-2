using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public static bool paused;
    public Canvas pauseScreen;
    private GameObject stageImage;
    // Use this for initialization
    void Start()
    {
        paused = false;
        pauseScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		Time.timeScale = paused ? 0 : 1;
		GameManager.State gameState = paused ? GameManager.State.Paused : GameManager.State.Running;
		GameManager.instance.SetState(gameState);
        
    }

    public void TogglePause()
    {
		paused = !paused;
		GameManager.State gameState = paused ? GameManager.State.Paused : GameManager.State.Running;
		GameManager.instance.SetState (gameState);
        if (paused)
        {
            onPause();
        }
        else
        {
            onResume();
        }

    }

	public void onPause() {
		//Disable player controls
		var buttons = GameObject.FindGameObjectsWithTag ("PlayerControl");
        GameObject.Find("PauseScreenPlayer").transform.FindChild("p_sotai").GetComponent<SkinnedMeshRenderer>().enabled = true;
        GameObject.Find("PausedGUI").GetComponent<Canvas>().enabled = true;
        stageImage = GameObject.Find("StageImage");
        
        // dont disable stage image for tutorial
        if (stageImage != null && !GameManager.instance.isTutorial)
        {
            stageImage.SetActive(false);
        }
        foreach (GameObject b in buttons) {
			Button button = b.GetComponent<Button>();
			button.interactable = false;
		}
        pauseScreen.enabled = true;
	}

	public void onResume() {
		//Enable player controls
		var buttons = GameObject.FindGameObjectsWithTag ("PlayerControl");
        GameObject.Find("PauseScreenPlayer").transform.FindChild("p_sotai").GetComponent<SkinnedMeshRenderer>().enabled = false;
        foreach (GameObject b in buttons) {
			Button button = b.GetComponent<Button>();
			button.interactable = true;
		}
        //update stats from pause screen and continue
        pauseScreen.enabled = false;

    }
}

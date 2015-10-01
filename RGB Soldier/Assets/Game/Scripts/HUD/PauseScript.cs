using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public bool paused;
    // Use this for initialization
    void Start()
    {
        paused = false;
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
		print ("Game status: " + GameManager.instance.getState ().ToString ());
		if (paused) {
			onPause ();
		} else {
			onResume();
		}
	}

	public void onPause() {
		//Disable player controls
		var buttons = GameObject.FindGameObjectsWithTag ("PlayerControl");
		foreach (GameObject b in buttons) {
			Button button = b.GetComponent<Button>();
			button.interactable = false;
		}
	}

	public void onResume() {
		//Enable player controls
		var buttons = GameObject.FindGameObjectsWithTag ("PlayerControl");
		foreach (GameObject b in buttons) {
			Button button = b.GetComponent<Button>();
			button.interactable = true;
		}
	}
}
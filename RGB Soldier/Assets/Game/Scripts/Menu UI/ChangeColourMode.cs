using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;

public class ChangeColourMode : MonoBehaviour {

    public ToggleGroup colourModes;
    public bool isEnabled;
	// Use this for initialization
	void Start () {
        // set the colour mode from persisted data
        Toggle[] t = colourModes.GetComponentsInChildren<Toggle>();
        int colourMode =  GameControl.control.colourMode;
        t[colourMode].isOn = true;

        // on pause screen dont allow toggling
        if (!isEnabled)
        {
            foreach (Toggle i in t) {
                i.enabled = false;

            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		Toggle[] t = colourModes.GetComponentsInChildren<Toggle>();
        if (GameManager.instance != null && GameManager.instance.isPaused())
		{
			foreach (Toggle i in t) {
				i.enabled = false;
				
			}
		}
	}

    public void colourModeUpdate(bool update)
    {

        // toggle group only allows 1 active toggle
        Toggle activeToggle = colourModes.ActiveToggles().FirstOrDefault();
        Toggle[] t = colourModes.GetComponentsInChildren<Toggle>();

        int i = Array.IndexOf(t, activeToggle);
        GameControl.control.colourMode = i;
    }
}

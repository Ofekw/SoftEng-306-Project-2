using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using System;

public class ChangeColourMode : MonoBehaviour {

    public ToggleGroup colourModes;

	// Use this for initialization
	void Start () {
        Toggle[] t = colourModes.GetComponentsInChildren<Toggle>();
        int colourMode =  GameControl.control.colourMode;
        t[colourMode].isOn = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void colourModeUpdate(bool update)
    {
        Toggle active = colourModes.ActiveToggles().FirstOrDefault();
        Toggle[] t = colourModes.GetComponentsInChildren<Toggle>();
        int i = Array.IndexOf(t, active);
        GameControl.control.colourMode = i;

    }
}

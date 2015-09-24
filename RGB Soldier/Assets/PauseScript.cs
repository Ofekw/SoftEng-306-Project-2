using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {
	public bool paused;
	// Use this for initialization
	void Start () {
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			paused = !paused;
		}
		if (paused) 
		{
			Time.timeScale = 0;
		}
		else if (!paused)
		{
			Time.timeScale = 1;
		}
	}
}

using UnityEngine;
using System.Collections;

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
    }

    public void TogglePause()
    {
        paused = !paused;
        print("Game paused: " + paused);
    }
}
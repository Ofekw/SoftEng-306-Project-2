using UnityEngine;
using System.Collections;

public class RightButtonScript : MonoBehaviour {

    public bool rightPressed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPointerDown()
    {
        Debug.Log("Down");
        rightPressed = true;
    }

    void onPointerUp()
    {
        rightPressed = false;
    }
}

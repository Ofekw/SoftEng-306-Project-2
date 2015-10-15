using UnityEngine;
using System.Collections;

public class CharacterSizing : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.orthographicSize = (20.0f / Screen.width * Screen.height / 2.0f);

    }
}

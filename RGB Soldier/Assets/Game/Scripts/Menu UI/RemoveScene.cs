using UnityEngine;
using System.Collections;

public class RemoveScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public void deleteScene()
    {
        Destroy(this.transform.gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

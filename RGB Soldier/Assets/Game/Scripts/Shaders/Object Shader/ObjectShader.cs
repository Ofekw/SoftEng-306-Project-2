using UnityEngine;
using System.Collections;

public class ObjectShader : MonoBehaviour {

    public Material[] materials = new Material[3];
    public Shader redGreenShader;
    public Shader blueYellowShader;

    Renderer rend;
    public int current = 0;


	// Use this for initialization
	void Start () {
        rend = this.gameObject.GetComponentInParent<SpriteRenderer>();

        materials[0] = new Material(rend.sharedMaterial);
        materials[1] = new Material(redGreenShader);
        materials[2] = new Material(blueYellowShader);
        //ChangeColourBlindMode(GameControl.control.colourMode);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Key pressed");
            ChangeColourBlindMode((current+1) % 3);
        }
	}

    //0 is none
    //1 is red-green
    //2 is blue-yellow
    void ChangeColourBlindMode(int mode)
    {
        Debug.Log("Changed to " + mode);
        rend.sharedMaterial = materials[mode];
        current = mode;
    }
}
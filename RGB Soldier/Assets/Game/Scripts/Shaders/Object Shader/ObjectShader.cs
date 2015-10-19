using UnityEngine;
using System.Collections;

public class ObjectShader : MonoBehaviour {

    public Shader[] shaders = new Shader[3];
    public Shader redGreenShader;
    public Shader blueYellowShader;

    public SkinnedMeshRenderer rend;
    public int current = 0;


	// Use this for initialization
	void Start () {

        shaders[0] = rend.material.shader;
        shaders[1] = redGreenShader;
        shaders[2] = blueYellowShader;
        ChangeColourBlindMode(GameControl.control.colourMode);
    }

    // Update is called once per frame
    void Update () {
        //Used for debugging in the unity editor
        if (Input.GetKeyDown(KeyCode.J))
        {
            ChangeColourBlindMode((current+1) % 3);
        }
	}

    //0 is none
    //1 is red-green
    //2 is blue-yellow
    //Change the colour mode
    void ChangeColourBlindMode(int mode)
    {
        for (int i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i].shader = shaders[mode];
        }
        current = mode;
    }
}
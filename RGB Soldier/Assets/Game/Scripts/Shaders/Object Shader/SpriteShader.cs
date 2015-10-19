using UnityEngine;
using System.Collections;

public class SpriteShader : MonoBehaviour {

    public Shader[] shaders = new Shader[3];
    public Shader redGreenShader;
    public Shader blueYellowShader;

    public SpriteRenderer rend;
    public int current = 0;


	// Use this for initialization
	void Start () {
        rend = gameObject.GetComponent<SpriteRenderer>();
        shaders[0] = rend.material.shader;
        shaders[1] = redGreenShader;
        shaders[2] = blueYellowShader;
        ChangeColourBlindMode(GameControl.control.colourMode);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ChangeColourBlindMode((current+1) % 3);
        }
	}

    //0 is none
    //1 is red-green
    //2 is blue-yellow
    void ChangeColourBlindMode(int mode)
    {
        for (int i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i].shader = shaders[mode];
        }
        current = mode;
    }
}
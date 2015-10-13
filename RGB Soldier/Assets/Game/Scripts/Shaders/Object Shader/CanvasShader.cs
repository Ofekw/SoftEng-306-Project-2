using UnityEngine;
using System.Collections;

public class CanvasShader : MonoBehaviour {

    public Material[] materials = new Material[3];
    public Shader redGreenShader;
    public Shader blueYellowShader;

    public CanvasRenderer rend;
    public Canvas canvas;
    public int current = 0;


	void Start () {
        rend = this.gameObject.GetComponent<CanvasRenderer>();
        canvas = this.gameObject.GetComponent<Canvas>();

        ChangeColourMode(GameControl.control.currentGameLevel);
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeColourMode(Mathf.RoundToInt(Random.value*3));
        }
	}

    //0 is Green
    //1 is Blue
    //2 is Red
    void ChangeColourMode(int mode)
    {
        CanvasRenderer[] renderers = GetComponentsInChildren<CanvasRenderer>();
        for (int i = 0; i < renderers.Length; i++ )
        {
            if (mode == 0)
            {
                renderers[i].SetColor(Color.green);
            } else if (mode == 1)
            {
                renderers[i].SetColor(Color.blue);
            } else if (mode == 2)
            {
                renderers[i].SetColor(Color.red);
            } 
        }

    
    }
}
using UnityEngine;
using System.Collections;

public class CanvasShader : MonoBehaviour {
    public CanvasRenderer rend;
    public Canvas canvas;
    public int current = 0;
    public bool allColours;
    public float lastChange;


	void Start () {
        rend = this.gameObject.GetComponent<CanvasRenderer>();
        canvas = this.gameObject.GetComponent<Canvas>();

        ChangeColourMode(GameControl.control.currentGameLevel);
        allColours = GameControl.control.currentGameLevel > 3;
        lastChange = Time.time;
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            current++;
            current = current % 3;
            ChangeColourMode(current);
        }
	}

    //0 is Green
    //1 is Blue
    //2 is Red
    void ChangeColourMode(int mode)
    {
        CanvasRenderer[] renderers = GetComponentsInChildren<CanvasRenderer>();
        Debug.Log(renderers.Length);
        for (int i = 0; i < renderers.Length; i++ )
        {
            if (renderers[i].CompareTag("DontChangeCanvasColour"))
            {
                Debug.Log("Not rendering " + renderers[i].gameObject.name);
            }
            else
            {
                Debug.Log("IUYEGWUEJRG " + renderers[i].gameObject.name);

                if (mode == 0)
                {
                    renderers[i].SetColor(Color.green);
                }
                else if (mode == 1)
                {
                    renderers[i].SetColor(Color.blue);
                }
                else if (mode == 2)
                {
                    renderers[i].SetColor(Color.red);
                }
            }
        }
    }
}
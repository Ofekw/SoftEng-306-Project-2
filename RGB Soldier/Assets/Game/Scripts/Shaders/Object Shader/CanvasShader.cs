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
        CanvasRenderer[] renderers = GetComponentsInChildren<CanvasRenderer>(true);
        for (int i = 0; i < renderers.Length; i++ )
        {
            if (renderers[i].CompareTag("AllowColourCanvas"))
            {
                if (mode == 0)
                {
                    renderers[i].SetColor(new Color(0.4f,1f,0.4f,1.0f));
                }
                else if (mode == 1)
                {
                    renderers[i].SetColor(new Color(0.0f, 0.9f, 0.9f, 1.0f));
                }
                else if (mode == 2)
                {
                    renderers[i].SetColor(new Color(1f, 0.2f, 0.2f, 1.0f));
                }
            }
        }
    }
}
using UnityEngine;
using System.Collections;

public class ObjectShader : MonoBehaviour {

    public Shader shader;

	// Use this for initialization
	void Start () {
        Renderer rend = this.gameObject.GetComponent<SpriteRenderer>();
        rend.sharedMaterial = new Material(shader);
        rend.sortingLayerName = "Foreground";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ChangeColourBlindMode(int mode)
    {
           
    }
}

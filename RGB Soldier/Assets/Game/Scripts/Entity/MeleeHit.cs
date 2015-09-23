using UnityEngine;
using System.Collections;

public class MeleeHit : MonoBehaviour {


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Crate" || hit.gameObject.tag == "Enemy")
        {
            hit.gameObject.SendMessage("takeDamage", 1);
        }
    }    
}
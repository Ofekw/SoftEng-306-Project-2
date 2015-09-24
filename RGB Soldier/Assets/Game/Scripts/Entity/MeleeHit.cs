using UnityEngine;
using System.Collections;

public class MeleeHit : MonoBehaviour {

    public GameObject player;


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
            //Tell the meleed object that it has taken damage
            //Damage is equal to players strength
            hit.gameObject.SendMessage("takeDamage", player.GetComponent<Player>().strength);
        }
    }    
}
using UnityEngine;
using System.Collections;

public class MeleeHit : MonoBehaviour {

    public GameObject player;
    public int meleeMultiplier = 2;
    public Sprite[] dmg;
    public GameObject damageIndicator;


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {

            Instantiate(damageIndicator, transform.position, transform.rotation);
            SpriteRenderer renderer = GameObject.Find("numeric-1").GetComponent<SpriteRenderer>();
            renderer.sprite = dmg[damageIndicator.gameObject.GetComponent<DamageIndicators>().CalculateMeleedDamageIndicator()];
            //Tell the meleed object that it has taken damage
            //Damage is equal to players strength
            hit.gameObject.SendMessage("takeDamage", meleeMultiplier*player.GetComponent<Player>().strength);
        }
    }    
}
using UnityEngine;
using System.Collections;
using System;

public class DesctructableCrate : KillableEntityInterface
{
    public override void die()
    {
        Destroy(this.gameObject);
    }

    public override void takeDamage(int damageReceived)
    {
        die();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

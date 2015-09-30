using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

    private GameObject damageText;
    private Text damageTextModifier;

	// Use this for initialization
	void Start () {
        Invoke("Die", 2);
	}

    void Die() {
        damageText = GameObject.Find("AttackText");
        Destroy(damageText);
    }

	
	// Update is called once per frame
	void Update () {
        int speed = 90;
        float y = Time.deltaTime * speed;
        transform.Translate(0, y, 0);
	}
}

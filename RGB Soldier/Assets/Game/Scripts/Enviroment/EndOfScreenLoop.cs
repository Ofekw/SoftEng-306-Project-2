using UnityEngine;
using System.Collections;

public class endOfScreenLoop : MonoBehaviour {

    public float spawnHeight = 14f;

    void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = new Vector3(other.transform.position.x,spawnHeight,other.transform.position.z);
    }
}

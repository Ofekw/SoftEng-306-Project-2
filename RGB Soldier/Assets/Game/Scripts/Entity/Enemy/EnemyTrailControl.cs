using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TrailRenderer))]

public class EnemyTrailControl : MonoBehaviour {

    public TrailRenderer trail;

	// Use this for initialization
	void Start () {

    trail = GetComponent<TrailRenderer>();
    trail.enabled = false;
    trail.sortingLayerName = "Background";
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

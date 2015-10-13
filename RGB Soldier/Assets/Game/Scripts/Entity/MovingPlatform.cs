using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public GameObject platform;
    public float moveSpeed;
    public Transform currentPoint;
    public Transform[] points;
    public int selectionPoint;

	// Use this for initialization
	void Start () {
        currentPoint = points[selectionPoint];
	}
	
	// Update is called once per frame
	void Update () {

        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);

        if (platform.transform.position == currentPoint.position) {
            selectionPoint++;

            if (selectionPoint == points.Length)
            {
                selectionPoint = 0;
            }

            currentPoint = points[selectionPoint];
        }
	}
}

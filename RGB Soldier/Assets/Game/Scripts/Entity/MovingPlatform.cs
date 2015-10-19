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

        // platform moves towards position
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);

        // once platform is at the position it needs to be at, it moves onto the next position in the points array
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

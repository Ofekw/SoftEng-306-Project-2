using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    // Object that is going to be moving
    public GameObject platform;

    // Speed of the object
    public float moveSpeed;

    // One of the points that it is in the array of points that the object is moving between
    public Transform currentPoint;

    // array of points
    public Transform[] points;
    public int selectionPoint;

	// Use this for initialization
	void Start () {
        // Make the point one of the points in the array
        currentPoint = points[selectionPoint];
	}
	
	// Update is called once per frame
	void Update () {

        // Move the object towards the currentPoint position
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);

        // Once the object is at its destination..
        if (platform.transform.position == currentPoint.position) {

            // Move onto the next point in the array
            selectionPoint++;

            if (selectionPoint == points.Length)
            {
                selectionPoint = 0;
            }

            currentPoint = points[selectionPoint];
        }
	}
}

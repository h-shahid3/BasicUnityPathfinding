using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Wayfinding : MonoBehaviour
{

    public Stopwatch stopwatch = new Stopwatch();
    public GameObject[] waypoints; //Array of waypoints.
    public float speed = 10.0f; // Speed of object
    public float turnSpeed = 10.0f; //Rotation speed of object
    int currentWPIndex; //Index in the waypoint array referring to the current waypoint.

    // Start is called before the first frame update
    void Start()
    {
        stopwatch.Start(); //Start timing NOW
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, waypoints[currentWPIndex].transform.position) < 1) //Condition is true if distance between object and current waypoint is less than 4
            currentWPIndex++; // Select next waypoint

        if(currentWPIndex >= waypoints.Length){ //Ensures waypoint indeex does not go out of bounds
            stopwatch.Stop();
            System.TimeSpan ts = stopwatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = System.String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            UnityEngine.Debug.Log(elapsedTime);
            currentWPIndex = 0; //Set to starting waypoint once again
        }
        Quaternion turntoWaypoint = Quaternion.LookRotation(waypoints[currentWPIndex].transform.position - this.transform.position); //Creates rotation for turning towards the next waypoint
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, turntoWaypoint, turnSpeed * Time.deltaTime); //Rotates the object at the turning speed defined towards the waypoint
        this.transform.Translate(0,0, speed * Time.deltaTime); //Moves object in direction of next waypoint
    }
}

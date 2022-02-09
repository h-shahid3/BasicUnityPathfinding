using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Diagnostics;

public class AIController : MonoBehaviour
{
    public GameObject endPoint;
    private int flag = 0;
    public Stopwatch stopwatch = new Stopwatch();
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = 10;
        stopwatch.Start(); //Start timing NOW
        agent.SetDestination(endPoint.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButtonDown(0)){
            flag = 1;
            stopwatch.Start(); //Start timing NOW
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.SetDestination(hit.point);
            }
        }*/
        if(Vector3.Distance(this.transform.position, endPoint.transform.position) < 1){
            stopwatch.Stop();
            System.TimeSpan ts = stopwatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = System.String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            UnityEngine.Debug.Log(elapsedTime);
        }
    }
}

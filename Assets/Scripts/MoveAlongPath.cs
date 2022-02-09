using UnityEngine;
using UnityEngine.AI;
using System.Diagnostics;
public class MoveAlongPath : MonoBehaviour {

    public NavMeshAgent agent;
    Transform currentGoal;
    float speed = 10.0f;
    float accuracy = 1.0f;
    float rotSpeed = 2.5f;
    public GameObject wpManager;
    GameObject[] waypoints;
    GameObject currentWP;
    int currentWPAlongPath = 0;
    public Stopwatch stopwatch = new Stopwatch();
    Graph g;

    //private int[] speedArray = {3,5};
    //private int index = 0;

    void Start() {
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = 10.0f;
        waypoints = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentWP = waypoints[0];
    }

    public void MoveToWP2() {
        g.AStarAlg(currentWP, waypoints[1]);
        currentWPAlongPath = 0;//reset
    }

    public void MoveToWP2Terrain(){
        g.AStarAlg(currentWP, waypoints[1]);
        currentWPAlongPath = 0;//reset
    }

    public void MoveToWP6Terrain() {
        g.AStarAlg(currentWP, waypoints[5]);
        currentWPAlongPath = 0;//reset
    }

    public void MoveTo2ndEnd() {
        stopwatch.Start(); //Start timing NOW
        g.AStarAlg(currentWP, waypoints[3]);
        currentWPAlongPath = 0;//reset
    }

    void LateUpdate() {
        /*switch(currentWPAlongPath){
                case 1: {
                    agent.speed = 10.0f;//normal
                    break;
                }
                case 2: {
                    agent.speed = 7.0f;//sand
                    break;
                }
                case 3: {
                    agent.speed = 10.0f;
                    break;
                }
                case 4: {
                    agent.speed = 5.0f;//water
                    break;
                }
                case 5: {
                    agent.speed = 3.0f;//mud
                    break;
                }
                
        }*/
        if (g.getPathLength() == 0 || currentWPAlongPath == g.getPathLength()){ //if at point already
            stopwatch.Stop();
            System.TimeSpan ts = stopwatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = System.String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            UnityEngine.Debug.Log(elapsedTime);
            return;
        }
        currentWP = g.getPathPoint(currentWPAlongPath);
        if (Vector3.Distance(
            g.getPathPoint(currentWPAlongPath).transform.position,
            transform.position) < accuracy) { //Condition is true if distance between object and current waypoint is less than specified accuracy
            currentWPAlongPath++;// Select next waypoint
        }

        if (currentWPAlongPath < g.getPathLength()) { //if not at dest yet
            GameObject ppgoal = g.getPathPoint(currentWPAlongPath);
            currentGoal = ppgoal.transform;
            /*Vector3 lookAtGoal = new Vector3(currentGoal.position.x,
                                            this.transform.position.y,
                                            currentGoal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);*/
            agent.SetDestination(currentGoal.position);
        }

    }
}
using UnityEngine;

[System.Serializable]
public struct Link {
    public enum direction { UNI, BI };

    public enum terrain {NORMAL, SAND, WATER, MUD};
    // Nodes
    public GameObject wp1;
    public GameObject wp2;
    // Direction UNI or BI
    public direction dir;

    public terrain ter;
}

public class WPManager : MonoBehaviour {

    public GameObject[] waypoints;
    public Link[] links;
    public Graph graph = new Graph();
    void Start() {
        if (waypoints.Length > 0) { //Add links and waypoints to graph
            foreach (GameObject wp in waypoints) {
                graph.AddNode(wp);
            }
            foreach (Link link in links) {
                graph.AddEdge(link.wp1, link.wp2, (int)link.ter);
                if (link.dir == Link.direction.BI)
                    graph.AddEdge(link.wp2, link.wp1,(int)link.ter);
            }
        }
    }

    void Update() {
    }
}

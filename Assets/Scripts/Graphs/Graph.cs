using System.Collections.Generic;
using UnityEngine;

public class Graph {
    List<Edge> edges = new List<Edge>();
    List<Node> nodes = new List<Node>();
    List<Node> pathList = new List<Node>();

    public Graph() { }

    public void AddNode(GameObject id) {
        Node node = new Node(id);
        nodes.Add(node);
    }

    public void AddEdge(GameObject fromNode, GameObject toNode, int edgeTerr) {
        Node from = findNode(fromNode);
        Node to = findNode(toNode);

        if (from != null && to != null) {
            Edge e = new Edge(from, to, edgeTerr);
            edges.Add(e);
            from.edgelist.Add(e);
        }
    }

    Node findNode(GameObject objID) {
        foreach (Node n in nodes) {
            if (n.getId() == objID)
                return n;
        }
        return null;
    }


    public int getPathLength() {
        return pathList.Count;
    }

    public GameObject getPathPoint(int index) {
        return pathList[index].id;
    }

    public bool AStarAlg(GameObject startId, GameObject endId) {
        Node startNode = findNode(startId);
        Node endNode = findNode(endId);
        if (startNode == null || endNode == null) {
            return false;
        }
        bool gScoreIsBetter = false;

        List<Node> openList = new List<Node>();
        List<Node> closeList = new List<Node>();
        float gScore = 0.0f;

        startNode.g = 0;
        startNode.h = Mathf.Pow(distance(startNode, endNode),4);
        startNode.f = startNode.h;
        openList.Add(startNode);
        while (openList.Count > 0) {
            int i = lowestF(openList);
            Node currentNode = openList[i];
            if (currentNode.id == endId)   //Reached end and need to add correct nodes
            {
                pathList.Clear();
                pathList.Add(endNode);
                Node prev = endNode.cameFrom;
                while (prev != startNode && prev != null) {
                    pathList.Insert(0, prev);
                    prev = prev.cameFrom; //traversing backwards
                }
                pathList.Insert(0, startNode);//Insert starting point at front
                return true;//end of alg
            }
            openList.RemoveAt(i);
            closeList.Add(currentNode);
            Node neighbour;
            foreach (Edge edge in currentNode.edgelist) {
                neighbour = edge.endNode;
                neighbour.g = currentNode.g + distance(currentNode, neighbour) * 4;
                if (closeList.IndexOf(neighbour) > -1)
                    continue;
                gScore = currentNode.g + distance(currentNode, neighbour) * (edge.terrain+1);
                if (openList.IndexOf(neighbour) == -1) {
                    openList.Add(neighbour);
                    gScoreIsBetter = true;
                    //Debug.Log("fval:" +currentNode.getId().ToString() +" and " + endNode.getId().ToString()+ " = " +neighbour.f);
                } else if (gScore < neighbour.g) {
                    gScoreIsBetter = true;
                    //Debug.Log("fval:" +currentNode.getId().ToString() +" and " + endNode.getId().ToString()+ " = " +neighbour.f);
                }
                else{
                    gScoreIsBetter = false;
                }
                if(gScoreIsBetter){
                    neighbour.cameFrom = currentNode;
                    neighbour.g = gScore;
                    neighbour.h = Mathf.Pow(distance(currentNode, endNode),(edge.terrain + 1));
                    neighbour.f = neighbour.g + neighbour.h;
                }
            }
        }

        return false;
    }

    float distance(Node a, Node b) {
        float dx = a.xPos - b.xPos;
        float dy = a.yPos - b.yPos;
        float dz = a.zPos - b.zPos;
        float dist = dx * dx + dy * dy + dz * dz;
        return (dist);
    }

    int lowestF(List<Node> l) {
        float lowestf = 0;
        int count = 0;
        int iteratorCount = 0;

        for (int i = 0; i < l.Count; i++) {
            if (i == 0) {
                lowestf = l[i].f;
                iteratorCount = count;
            } else if (l[i].f <= lowestf) {
                lowestf = l[i].f;
                iteratorCount = count;
            }
            count++;
        }
        return iteratorCount;
    }
}
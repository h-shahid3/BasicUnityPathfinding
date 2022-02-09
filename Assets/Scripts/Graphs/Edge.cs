public class Edge {
    public int terrain;
    public Node startNode;
    public Node endNode;
    public Edge(Node from, Node to, int edgeTerr) {
        startNode = from;
        endNode = to;
        terrain = edgeTerr;
    }
}

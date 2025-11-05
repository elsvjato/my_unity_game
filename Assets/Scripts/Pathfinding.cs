using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public List<Vector2> FindPath(Node startNode, Node targetNode)
    {
        if (startNode == null || targetNode == null)
        {
            Debug.LogWarning("Path error: startNode or targetNode");
            return null;
        }

        PriorityQueue<Node, float> openSet = new PriorityQueue<Node, float>();
        Dictionary<Node, float> gCosts = new Dictionary<Node, float>();
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();

        gCosts[startNode] = 0;//dictionary for cost for every node from start
        openSet.Enqueue(startNode, 0);
        cameFrom[startNode] = null;

        while (openSet.Count > 0)
        {
            Node current = openSet.Dequeue();

            if (current == targetNode)
            {
                return ReconstructPath(cameFrom, current);
            }

            foreach (Node neighbor in current.neighbors)
            {
                float tentativeGCost = gCosts[current] + Vector2.Distance(current.Position, neighbor.Position);

                if (!gCosts.ContainsKey(neighbor) || tentativeGCost < gCosts[neighbor])
                {
                    gCosts[neighbor] = tentativeGCost;//cost for neighbor nodes through current
                    float hCost = Vector2.Distance(neighbor.Position, targetNode.Position); //euristic aproximate cost from neighbor to target
                    float fCost = tentativeGCost + hCost;//total cost
                    openSet.Enqueue(neighbor, fCost);
                    cameFrom[neighbor] = current;
                }
            }
        }

        Debug.LogWarning("path to player is not found");
        return null;
    }

    private List<Vector2> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
    {
        List<Vector2> path = new List<Vector2>();
        while (current != null)
        {
            path.Add(current.Position);
            current = cameFrom[current];
        }
        path.Reverse();
        return path;
    }
}
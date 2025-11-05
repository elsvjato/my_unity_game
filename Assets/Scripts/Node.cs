using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> neighbors; //list of neighbors
    public bool requiresJump;
    public Vector2 Position => transform.position; //node position

    private void OnDrawGizmos()
    {
        Gizmos.color = requiresJump ? Color.blue : Color.green;
        Gizmos.DrawSphere(transform.position, 0.2f);
        Gizmos.color = Color.red;

        if (neighbors != null)
        {
            foreach (var neighbor in neighbors)
            {
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
}
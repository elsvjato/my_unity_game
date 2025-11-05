using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyBehaviourAI : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float pathUpdateInterval = 0.2f;
    private List<Vector3> currentPath;
    private int pathIndex = 0;
    public TilemapPathfinding pathfinding;
    private float pathUpdateTimer = 0f;
    public Tilemap walkableTilemap;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer isn't found");
        }

        //starting point
        UpdatePath();
    }

    private void Update()
    {
        //update path
        pathUpdateTimer += Time.deltaTime;
        if (pathUpdateTimer >= pathUpdateInterval && pathIndex < currentPath?.Count)
        {
            UpdatePath();
            pathUpdateTimer = 0f;
        }

        if (currentPath != null && pathIndex < currentPath.Count)
        {
            Vector3 targetPosition = currentPath[pathIndex];

            if (!IsWithinWalkableZone(targetPosition))
            {
                Debug.LogWarning("player is out of WalkableZone!");
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f || IsCloseEnough(targetPosition))
            {
                pathIndex++;
                if (pathIndex >= currentPath.Count)
                {
                    pathIndex = currentPath.Count - 1; //if index is out of range
                    Debug.LogWarning("player reached");
                }
            }
        }
        else
        {
            Debug.Log("path is not found. ghost stops");
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
        }
    }

    private bool IsWithinWalkableZone(Vector3 position)
    {
        //whether position is in WalkableZone
        Vector3Int cellPosition = walkableTilemap.WorldToCell(position);
        return walkableTilemap.HasTile(cellPosition);
    }

    private bool IsCloseEnough(Vector3 targetPosition)
    {
        //approximatification
        return Mathf.Approximately(transform.position.x, targetPosition.x) &&
               Mathf.Approximately(transform.position.y, targetPosition.y) &&
               Mathf.Approximately(transform.position.z, targetPosition.z);
    }

    public void StartFollowing(Vector3 playerPosition)
    {
        Vector3 enemyPosition = transform.position;

        //generating new path
        List<Vector3> newPath = pathfinding.FindPath(enemyPosition, playerPosition);

        if (newPath != null && newPath.Count > 0)
        {
            currentPath = newPath;
            pathIndex = 0;
            Debug.Log("new path is found: " + string.Join(" -> ", currentPath));
        }
        else
        {
            Debug.LogWarning("path is not found.");
        }
    }

    public void UpdatePath()
    {
        if (pathfinding == null || walkableTilemap == null)
        {
            Debug.LogError("TilemapPathfinding or WalkableTilemap is not signed");
            return;
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null)
        {
            Debug.LogWarning("player is not found.");
            return;
        }

        Vector3 playerPosition = playerObject.transform.position;

        //calculating new path
        StartFollowing(playerPosition);
    }

    private void OnDrawGizmos()
    {
        if (currentPath != null)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < currentPath.Count - 1; i++)
            {
                Gizmos.DrawLine(currentPath[i], currentPath[i + 1]);
            }
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.1f); //current ghost position
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPathfinding : MonoBehaviour
{
    public Tilemap walkableTilemap; // Tilemap з прохідними зонами
    private Dictionary<Vector3Int, List<Vector3Int>> neighborsCache = new Dictionary<Vector3Int, List<Vector3Int>>();

    public List<Vector3> FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        Vector3Int startCell = walkableTilemap.WorldToCell(startPosition);
        Vector3Int targetCell = walkableTilemap.WorldToCell(targetPosition);

        if (!IsWalkable(startCell) || !IsWalkable(targetCell))
        {
            Debug.LogWarning("Початкова або кінцева клітинка не є прохідною.");
            return null;
        }

        PriorityQueue<Vector3Int, float> openSet = new PriorityQueue<Vector3Int, float>();
        Dictionary<Vector3Int, float> gCosts = new Dictionary<Vector3Int, float>();
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();

        gCosts[startCell] = 0;
        openSet.Enqueue(startCell, 0);

        // Вказуємо, що початковий вузол веде до себе самого
        cameFrom[startCell] = startCell;

        while (openSet.Count > 0)
        {
            Vector3Int current = openSet.Dequeue();

            if (current == targetCell)
            {
                return ReconstructPath(cameFrom, current);
            }

            foreach (Vector3Int neighbor in GetNeighbors(current))
            {
                if (!IsWalkable(neighbor)) continue;

                float tentativeGCost = gCosts[current] + Vector3.Distance(walkableTilemap.GetCellCenterWorld(current), walkableTilemap.GetCellCenterWorld(neighbor));

                if (!gCosts.ContainsKey(neighbor) || tentativeGCost < gCosts[neighbor])
                {
                    gCosts[neighbor] = tentativeGCost;
                    float hCost = Vector3.Distance(walkableTilemap.GetCellCenterWorld(neighbor), walkableTilemap.GetCellCenterWorld(targetCell)); // Евристика
                    float fCost = tentativeGCost + hCost;
                    openSet.Enqueue(neighbor, fCost);
                    cameFrom[neighbor] = current;
                }
            }
        }

        Debug.LogWarning("Шлях до гравця не знайдено.");
        return null;
    }

    private List<Vector3Int> GetNeighbors(Vector3Int cell)
    {
        if (neighborsCache.ContainsKey(cell)) return neighborsCache[cell];

        List<Vector3Int> neighbors = new List<Vector3Int>
        {
            cell + Vector3Int.up,
            cell + Vector3Int.down,
            cell + Vector3Int.left,
            cell + Vector3Int.right
        };

        neighborsCache[cell] = neighbors;
        return neighbors;
    }

    private bool IsWalkable(Vector3Int cell)
    {
        return walkableTilemap.HasTile(cell); // Перевіряємо, чи має клітинка тайл
    }

    private List<Vector3> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current)
    {
        List<Vector3> path = new List<Vector3>();
        Vector3Int? previous = current;

        // Реконструюємо шлях, поки не дійдемо до початкового вузла
        while (previous.HasValue && cameFrom.ContainsKey(previous.Value) && cameFrom[previous.Value] != previous.Value)
        {
            path.Add(walkableTilemap.GetCellCenterWorld(previous.Value));
            previous = cameFrom[previous.Value]; // Отримуємо попередній вузол
        }

        // Додаємо початковий вузол до шляху
        if (previous.HasValue)
        {
            path.Add(walkableTilemap.GetCellCenterWorld(previous.Value));
        }

        path.Reverse();
        return path;
    }
}
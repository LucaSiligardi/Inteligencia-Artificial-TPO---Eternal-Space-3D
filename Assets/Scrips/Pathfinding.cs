using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding Instance;
    private PathfindingGrid _grid;

    void Awake()
    {
        Instance = this;
        _grid = GetComponent<PathfindingGrid>();
    }

    public List<Vector3> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        GridNode startNode = _grid.NodeFromWorldPoint(startPos);
        GridNode targetNode = _grid.NodeFromWorldPoint(targetPos);

        List<GridNode> openSet = new List<GridNode>();
        HashSet<GridNode> closedSet = new HashSet<GridNode>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            // Nodo con menor fCost
            GridNode currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost ||
                    (openSet[i].fCost == currentNode.fCost &&
                     openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            // Llegamos al destino
            if (currentNode == targetNode)
                return RetracePath(startNode, targetNode);

            foreach (GridNode neighbour in _grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                    continue;

                int newCost = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newCost < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCost;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;
                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        return null; // no encontró camino
    }

    private List<Vector3> RetracePath(GridNode startNode, GridNode endNode)
    {
        List<Vector3> path = new List<Vector3>();
        GridNode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.worldPosition);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    private int GetDistance(GridNode a, GridNode b)
    {
        int dstX = Mathf.Abs(a.gridX - b.gridX);
        int dstY = Mathf.Abs(a.gridY - b.gridY);
        return dstX > dstY ? 14 * dstY + 10 * (dstX - dstY)
                           : 14 * dstX + 10 * (dstY - dstX);
    }
}
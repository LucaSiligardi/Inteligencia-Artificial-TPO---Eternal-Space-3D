using System.Collections.Generic;
using UnityEngine;

public class PathfindingGrid : MonoBehaviour
{
    [Header("Configuración de la grilla")]
    public Vector2 gridWorldSize;
    public float nodeRadius = 1f;
    public LayerMask obstacleLayer;

    private GridNode[,] _grid;
    private float _nodeDiameter;
    private int _gridSizeX, _gridSizeY;
    private Transform _playerTransform;

    void Awake()
    {
        _nodeDiameter = nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(gridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(gridWorldSize.y / _nodeDiameter);
    }

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            _playerTransform = player.transform;

        CreateGrid();
    }

    void Update()
    {
        if (_playerTransform != null)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                _playerTransform.position.z + 20f
            );
            CreateGrid();
        }
    }

    void CreateGrid()
    {
        _grid = new GridNode[_gridSizeX, _gridSizeY];
        Vector3 bottomLeft = transform.position
            - Vector3.right * gridWorldSize.x / 2
            - Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft
                    + Vector3.right * (x * _nodeDiameter + nodeRadius)
                    + Vector3.up * (y * _nodeDiameter + nodeRadius);

                bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, obstacleLayer);
                _grid[x, y] = new GridNode(worldPoint, walkable, x, y);
            }
        }
    }

    public GridNode NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = Mathf.Clamp01(
            (worldPosition.x - transform.position.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float percentY = Mathf.Clamp01(
            (worldPosition.y - transform.position.y + gridWorldSize.y / 2) / gridWorldSize.y);

        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);
        return _grid[x, y];
    }

    public List<GridNode> GetNeighbours(GridNode node)
    {
        List<GridNode> neighbours = new List<GridNode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                if (checkX >= 0 && checkX < _gridSizeX &&
                    checkY >= 0 && checkY < _gridSizeY)
                {
                    neighbours.Add(_grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,
            new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (_grid != null)
        {
            foreach (GridNode node in _grid)
            {
                Gizmos.color = node.walkable ? Color.white : Color.red;
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (_nodeDiameter - 0.1f));
            }
        }
    }
}
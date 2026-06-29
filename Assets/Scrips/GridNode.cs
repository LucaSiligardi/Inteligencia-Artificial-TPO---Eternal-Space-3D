using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode
{
    public Vector3 worldPosition;
    public bool walkable;
    public int gridX, gridY;

    // Para A*
    public int gCost; // costo desde el inicio
    public int hCost; // costo estimado hasta el destino
    public int fCost => gCost + hCost;
    public GridNode parent;

    public GridNode(Vector3 worldPosition, bool walkable, int gridX, int gridY)
    {
        this.worldPosition = worldPosition;
        this.walkable = walkable;
        this.gridX = gridX;
        this.gridY = gridY;
    }
}
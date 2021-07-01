using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public GameObject[] tiles;
    public GameManager gameManager;
    public Grid grid;
    public Vector3 centerOfRotation;

    // Returns true if nothing is on the left of each tile.
    public bool checkLeft()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (Mathf.Round(grid.getGridCoordinate(tiles[i].transform.position).x) == 0)
            {
                return false;
            }
            Vector3 pos = grid.getGridCoordinate(tiles[i].transform.position);
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            if (grid.getOccupied(x - 1, y))
            {
                return false;
            }
        }
        return true;
    }

    // Returns true if nothing is on the right of each tile.
    public bool checkRight()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (Mathf.Round(grid.getGridCoordinate(tiles[i].transform.position).x) == 9)
            {
                return false;
            }
            Vector3 pos = grid.getGridCoordinate(tiles[i].transform.position);
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            if (grid.getOccupied(x + 1, y))
            {
                return false;
            }
        }
        return true;
    }

    public bool checkDown()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (Mathf.Round(grid.getGridCoordinate(tiles[i].transform.position).y) == 0)
            {
                return false;
            }
            Vector3 pos = grid.getGridCoordinate(tiles[i].transform.position);
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            if (grid.getOccupied(x, y - 1))
            {
                return false;
            }
        }
        return true;
    }

    public void rotateRight()
    {
        tryRotate(Mathf.PI * 1.5f);
    }
    public void rotateLeft()
    {
        tryRotate(Mathf.PI * 0.5f);
    }
    public void rotate180()
    {
        tryRotate(Mathf.PI);
    }

    // Attempts rotate piece for the given angle, in radians.
    public void tryRotate(float angle)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (grid.getOccupied(grid.getGridCoordinate(Utils.returnRotate(tiles[i].transform.position, transform.TransformPoint(centerOfRotation), angle))))
            {
                return;
            }
        }
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].transform.position = Utils.returnRotate(tiles[i].transform.position, transform.TransformPoint(centerOfRotation), angle);
        }
    }
}

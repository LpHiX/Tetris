using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public GameObject[] tiles;
    public GameManager gameManager;
    public Grid grid;

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
}

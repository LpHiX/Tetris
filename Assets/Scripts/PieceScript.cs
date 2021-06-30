using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public GameObject[] tiles;
    public GameManager gameManager;
    private Grid grid;

    private void Start()
    {
        grid = gameManager.grid;
    }

    // Returns true if nothing is on the left of each tile.
    public bool checkLeft()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (Mathf.Round(gameManager.grid.getGridCoordinate(tiles[i].transform.position).x) == 0)
            {
                Debug.Log("Left detected");
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
            if (Mathf.Round(gameManager.grid.getGridCoordinate(tiles[i].transform.position).x) == 9)
            {
                Debug.Log("Right detected");
                return false;
            }
        }
        return true;
    }

    public bool checkDown()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (Mathf.Round(gameManager.grid.getGridCoordinate(tiles[i].transform.position).y) == 0)
            {
                Debug.Log("Down detected");
                return false;
            }
        }
        return true;
    }
}

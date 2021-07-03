using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceScript : MonoBehaviour
{
    public GameObject[] tiles;
    public GameManager gameManager;
    public Grid grid;
    public Vector3 centerOfRotation;
    public Vector3 centerOfMass;
    public int pieceIndex;
    private int rotationState = 0;

    // Returns true if something is to the left
    public bool checkLeft()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (Mathf.Round(grid.getGridCoordinate(tiles[i].transform.position).x) == 0)
            {
                return true;
            }
            Vector3 pos = grid.getGridCoordinate(tiles[i].transform.position);
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            if (grid.getOccupied(x - 1, y))
            {
                return true;
            }
        }
        return false;
    }

    // Returns true if something is to the right
    public bool checkRight()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (Mathf.Round(grid.getGridCoordinate(tiles[i].transform.position).x) == gameManager.xLength-1)
            {
                return true;
            }
            Vector3 pos = grid.getGridCoordinate(tiles[i].transform.position);
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            if (grid.getOccupied(x + 1, y))
            {
                return true;
            }
        }
        return false;
    }
    // Returns true if something is to the bottom
    public bool checkDown()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (Mathf.Round(grid.getGridCoordinate(tiles[i].transform.position).y) == 0)
            {
                return true;
            }
            Vector3 pos = grid.getGridCoordinate(tiles[i].transform.position);
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            if (grid.getOccupied(x, y - 1))
            {
                return true;
            }
        }
        return false;
    }

    public void rotateRight()
    {
        tryRotate(1);
    }
    public void rotateLeft()
    {
        tryRotate(3);
    }
    public void rotate180()
    {
        tryRotate(2);
    }

    // Attempts to rotate from the current state to the next state. 0 = spawn state, 1 = right from spawn, 2 = 180 degree, 3 = left from spawn
    public void tryRotate(int state)
    {
        float angle = (-Mathf.PI) * state / 2;
        int nextRotationState = (state + rotationState) % 4;

        // Iterate through all the wall kick tests
        int test = 0;
        NextTest:
        if (test == 5)
        {
            Debug.Log("Rotate failed");
            return;
        }
        //Vector3 wallKickTranslation = new Vector3(0, 0, 0);
        Vector3 wallKickTranslation = pieceIndex != 0 ? Utils.getWallKickData(rotationState, nextRotationState, test) : Utils.getWallKickDataI(rotationState, nextRotationState, test);
        // Debug drawing ---------
        for (int i = 0; i < tiles.Length; i++)
        {
            Utils.drawPoint(Utils.returnRotate(tiles[i].transform.position, transform.TransformPoint(centerOfRotation), angle) + wallKickTranslation * gameManager.cellSize, 0.5f, 1f);
        } // --------------
        for (int i = 0; i < tiles.Length; i++)
        {
            if (grid.getOccupied(grid.getGridCoordinate(Utils.returnRotate(tiles[i].transform.position, transform.TransformPoint(centerOfRotation), angle)) + wallKickTranslation))
            {
                Debug.Log("Found wall " +test);
                test++;
                goto NextTest;
            }
        }
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i].transform.position = Utils.returnRotate(tiles[i].transform.position, transform.TransformPoint(centerOfRotation), angle);
        }
        transform.position = transform.position + (pieceIndex != 0 ? Utils.getWallKickData(rotationState, nextRotationState, test) : Utils.getWallKickDataI(rotationState, nextRotationState, test)) * gameManager.cellSize;
        rotationState = nextRotationState;
    }
    private Vector3 getWallKickData(int from, int to, int test)
    {
        Vector3 translation = new Vector3(0,0,0);
        
        return translation;
    }
}

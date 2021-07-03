using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Transform mainObject;
    private GameObject[,] gridArray;
    public GameObject backtile;

    //Grid height has to be x2, to account for if things gets pushed up and also to account for where blocks spawn
    public Grid(int width, int height, float cellSize, Transform mainObject = null, GameObject backtile = null)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.mainObject = mainObject;
        this.backtile = backtile;

        createGrid();
    }

    private void createGrid()
    {
        gridArray = new GameObject[width, height * 2];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1) / 2; y++)
            {
                //Utils.createWorldText(gridArray[x, y].ToString(), mainObject, getLocalPosition(x, y), 10);
                Debug.DrawLine(getWorldPosition(x - 0.5f, y - 0.5f), getWorldPosition(x - 0.5f, y + 0.5f), Color.white, 100f);
                Debug.DrawLine(getWorldPosition(x - 0.5f, y - 0.5f), getWorldPosition(x + 0.5f, y - 0.5f), Color.white, 100f);
                GameObject.Instantiate(backtile, getWorldPosition(x, y), Quaternion.identity);
            }
        }
        Debug.DrawLine(getWorldPosition(-0.5f, height - 0.5f), getWorldPosition(width - 0.5f, height - 0.5f), Color.white, 100f);
        Debug.DrawLine(getWorldPosition(width - 0.5f, -0.5f), getWorldPosition(width - 0.5f, height - 0.5f), Color.white, 100f);
    }
    public void clearFullLines()
    {
        for (int y = 0; y < gridArray.GetLength(1); y++)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                if (!getOccupied(x, y)) goto noneFound;
            }
            Debug.Log("Full line at " + y);
            cleanLine(y);
            y--;
        noneFound:
            continue;
        }
    }
    public void cleanLine(int cleanedHeight)
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            GameObject.Destroy(getPlaced(x, cleanedHeight));
        }


        for (int y = cleanedHeight; y < gridArray.GetLength(1) - 1; y++)
        {
            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                setPlaced(x, y, getPlaced(x, y + 1));
            }
        }
    }
    public GameObject getPlaced(int x, int y)
    {
        try
        {
            return gridArray[x, y];
        }
        catch (System.Exception ex) when (
            ex is System.IndexOutOfRangeException
        )
        {
            if(ex is System.IndexOutOfRangeException)
            {
                Debug.LogWarning("Tried to get a tile from outside the grid");
            }
        }
        return null;
    }


    public void setPlaced(int x, int y, GameObject tile)
    {
        try
        {
            gridArray[x, y] = tile;
            if (tile != null)
            {
                tile.transform.position = getWorldPosition(x, y);
            }
        }
        catch (System.IndexOutOfRangeException)
        {
            Debug.LogWarning("Cannot place tile out of grid");
        }
    }
    public bool getOccupied(Vector3 target)
    {
        return getOccupied(Mathf.RoundToInt(target.x), Mathf.RoundToInt(target.y));
    }
    public bool getOccupied(int x, int y)
    {
        try
        {
            if (gridArray[x, y] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (System.IndexOutOfRangeException)
        {
            return true;
        }
    }

    public Vector3 getWorldPosition(float x, float y)
    {
        return mainObject.transform.TransformPoint(getLocalPosition(x, y));
    }

    public Vector3 getLocalPosition(float x, float y)
    {
        return new Vector3(x, y) * cellSize;
    }

    public Vector3 getGridCoordinate(Vector3 position)
    {
        return new Vector3(position.x / cellSize + width / 2 - (float)(width - 1) % 2 / 2, position.y / cellSize + height / 2 - 0.5f);
    }
}

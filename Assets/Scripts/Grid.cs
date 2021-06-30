using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Transform mainObject;
    private int[,] gridArray;

    public Grid(int width, int height, float cellSize, Transform mainObject = null, GameObject backtile = null)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.mainObject = mainObject;

        gridArray = new int[width, height];

        //Debug.Log("Instantiated Grid: " + width + " , " + height);

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //Utils.createWorldText(gridArray[x, y].ToString(), mainObject, getLocalPosition(x, y), 10);
                Debug.DrawLine(getWorldPosition(x - 0.5f, y - 0.5f), getWorldPosition(x - 0.5f, y + 0.5f), Color.white, 100f);
                Debug.DrawLine(getWorldPosition(x - 0.5f, y - 0.5f), getWorldPosition(x + 0.5f, y - 0.5f), Color.white, 100f);
                GameObject.Instantiate(backtile, getWorldPosition(x, y), Quaternion.identity);
            }
        }
        Debug.DrawLine(getWorldPosition(-0.5f, height - 0.5f), getWorldPosition(width - 0.5f, height - 0.5f), Color.white, 100f);
        Debug.DrawLine(getWorldPosition(width - 0.5f, - 0.5f), getWorldPosition(width - 0.5f, height - 0.5f), Color.white, 100f);
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
        return new Vector3(position.x / cellSize + width / 2 - 0.5f, position.y / cellSize + height / 2 - 0.5f);
    }
}

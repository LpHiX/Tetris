using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private int[,] gridArray;

    public Grid(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;

        gridArray = new int[width, height];

        Debug.Log("Instantiated Grid: " + width + " , " + height);

        for (int y = 0; y < gridArray.GetLength(0); y++)
        {
            for (int x = 0; x < gridArray.GetLength(1); x++)
            {
                //Debug.Log("x:" + x + " y:" + y);
                Utils.createWorldText(gridArray[x, y].ToString(), null, getWorldPosition(x, y));
            }
        }
    }

    private Vector3 getWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

}

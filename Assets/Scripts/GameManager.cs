using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int xLength = 10;
    public int yLength = 20;
    public float cellSize = 0.64f;
    public GameObject backtile;

    public GameObject iPiece;
    public GameObject jPiece;
    public GameObject lPiece;
    public GameObject oPiece;
    public GameObject sPiece;
    public GameObject tPiece;
    public GameObject zPiece;

    public int previews;
    public static int currentPiece;

    private List<int> bag = new List<int>();
    public GameObject player;
    public Grid grid;

    // Start is called before the first frame update
    void Awake()
    {
        transform.position = new Vector3(-(xLength - 1) * cellSize / 2, -(yLength - 1) * cellSize / 2, 0);
        grid = new Grid(xLength, yLength, cellSize, transform, backtile);
        for (int i = 0; i <= previews; i++)
        {
            bag.Add(Random.Range(0, 7));
        }
        currentPiece = bag[0];
    }

    private void Update()
    {

    }

    public GameObject getPiece(int piece)
    {
        switch (piece)
        {
            case 0:
                return iPiece;
            case 1:
                return jPiece;
            case 2:
                return lPiece;
            case 3:
                return oPiece;
            case 4:
                return sPiece;
            case 5:
                return tPiece;
            case 6:
                return zPiece;
            default:
                return tPiece;
        }
    }
    
    public static int getCurrentInt()
    {
        return currentPiece;
    }

    public void placePiece()
    {

    }
}

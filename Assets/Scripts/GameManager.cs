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

    private PlayerController playerController;

    // Start is called before the first frame update
    void Awake()
    {
        transform.position = new Vector3(-(xLength - 1) * cellSize / 2, -(yLength - 1) * cellSize / 2, 0);
        grid = new Grid(xLength, yLength, cellSize, transform, backtile);
        addToBag();
        currentPiece = bag[0];
        playerController = player.GetComponent<PlayerController>();
    }
    public void placePiece()
    {
        bag.RemoveAt(0);
        currentPiece = bag[0];
        bag.Add(Random.Range(0, 7));
        playerController.placeTiles();
        if (bag.Count < previews)
        {
            addToBag();
        }

        grid.clearFullLines();
    }

    private void addToBag()
    {
        List<int> prebag = new List<int>();
        for (int i = 0; i < 7; i++)
        {
            int random = Random.Range(0, 7);
            while (prebag.Contains(random))
            {
                random = Random.Range(0, 7);
            }
            prebag.Add(random);
        }
        bag.AddRange(prebag);
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
}

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
    public int currentInt;

    public List<int> bag = new List<int>();
    public GameObject player;
    public Grid grid;
    public PreviewUI previewUI;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Awake()
    {
        transform.position = new Vector3(-(xLength - 1) * cellSize / 2, -(yLength - 1) * cellSize / 2, 0);
        grid = new Grid(xLength, yLength, cellSize, transform, backtile);
        previewUI = new PreviewUI(this, grid);

        // Padding the bag, as PlayerController.newpiece() removes the first item
        bag.Add(0);
        addToBag();

        currentInt = bag[0];
        playerController = player.GetComponent<PlayerController>();
        previewUI.updatePreviews();
    }
    public void placePiece()
    {
        playerController.placeTiles();
        grid.clearFullLines();
    }

    public void addToBag()
    {
        do
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
            string debug = "Prebag: ";
            foreach (int i in prebag)
            {
                debug += i + ", ";
            }
            Debug.Log(debug);
            bag.AddRange(prebag);
        } while (bag.Count <= previews);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewUI
{
    private GameManager gameManager;
    private Grid grid;
    private GameObject backtile;
    private PieceScript[] previewItems;

    public GameObject heldPiece;
    public PieceScript heldPieceScript;

    public PreviewUI(GameManager gameManager, Grid grid)
    {
        this.gameManager = gameManager;
        this.grid = grid;
        backtile = grid.backtile;
        previewItems = new PieceScript[gameManager.previews];

        createBackdrops();
    }

    public void holdPiece(GameObject holdPiece)
    {
        if(heldPiece != null) heldPieceScript.destroyThis();
        heldPiece = holdPiece;
        heldPieceScript = holdPiece.GetComponent<PieceScript>();
        heldPiece.transform.position = grid.getWorldPosition(-gameManager.xLength/2 + 1,gameManager.yLength - 2f) - heldPieceScript.centerOfMass;
    }

    private void createBackdrops()
    {

        // Create Hold Backdrop
        int[,] holdUIArray = new int[5,3];
        for (int x = 0; x < holdUIArray.GetLength(0); x++)
        {
            for (int y = 0; y < holdUIArray.GetLength(1); y++)
            {
                float xPad = x - 1 - gameManager.xLength / 2;
                float yPad = y + gameManager.yLength - 3;
                GameObject.Instantiate(backtile, grid.getWorldPosition(xPad, yPad), Quaternion.identity);
            }
        }

        // Create Preview Backdrop
        int[,] previewUIArray = new int[5, 3 * gameManager.previews];
        for (int x = 0; x < previewUIArray.GetLength(0); x++)
        {
            for (int y = 0; y < previewUIArray.GetLength(1); y++)
            {
                float xPad = x + 1 + gameManager.xLength;
                float yPad = y + gameManager.yLength - 3 * gameManager.previews;
                GameObject.Instantiate(backtile, grid.getWorldPosition(xPad, yPad), Quaternion.identity);
            }
        }

        //Create Preview List


    }

    public void updatePreviews()
    {
        for(int i = 0; i < gameManager.previews; i++)
        {
            destroyPreview(i);
            addToPreview(gameManager.bag[i + 1], i);
        }
    }

    private void addToPreview(int pieceIndex, int preview)
    {
        GameObject previewObject = GameObject.Instantiate(gameManager.getPiece(pieceIndex));
        previewItems[preview] = previewObject.GetComponent<PieceScript>();
        previewObject.transform.position = grid.getWorldPosition(3 + gameManager.xLength, gameManager.yLength - 3 * preview - 2) - previewItems[preview].centerOfMass;
    }

    private void destroyPreview(int preview)
    {
        if(previewItems[preview] != null)
        {
            previewItems[preview].destroyThis();
        }
    }

}

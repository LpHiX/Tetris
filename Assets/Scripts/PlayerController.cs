using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    Controls controls;

    public GameObject game;

    private GameManager gameManager;
    private PreviewUI previewUI;
    private Grid grid;
    private int piece;
    private GameObject currentObject;
    private PieceScript pieceScript;

    public float DAS = .133f;
    public float ARR = .05f;
    public float SDF = .05f;

    private float DAStimer = 0;
    private float ARRtimer = 0;
    private float SDFtimer = 0;

    private bool holdCooldown = false;

    bool rightPressed;
    bool leftPressed;
    bool softDropping;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.RotateLeft.started += ctx => rotateLeft();
        controls.Player.RotateRight.started += ctx => rotateRight();
        controls.Player.Rotate180.started += ctx => rotate180();
        controls.Player.HardDrop.started += ctx => hardDrop();
        controls.Player.Hold.started += ctx => hold();
        controls.Player.Right.started += ctx =>
        {
            rightPressed = true;
            if (!pieceScript.checkRight())
            {
                DAStimer = DAS;
                transform.position = transform.position + new Vector3(gameManager.cellSize, 0, 0);
            }
        };
        controls.Player.Right.canceled += ctx => rightPressed = false;
        controls.Player.Left.started += ctx =>
        {
            leftPressed = true;
            if (!pieceScript.checkLeft())
            {
                DAStimer = DAS;
                transform.position = transform.position + new Vector3(-gameManager.cellSize, 0, 0);
            }
        };
        controls.Player.Left.canceled += ctx => leftPressed = false;
        controls.Player.SoftDrop.started += ctx => softDropping = true;
        controls.Player.SoftDrop.canceled += ctx => softDropping = false;
    }

    void Start()
    {
        gameManager = game.GetComponent<GameManager>();
        grid = gameManager.grid;
        previewUI = gameManager.previewUI;
        newPiece();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        DAStimer -= Time.deltaTime;
        ARRtimer -= Time.deltaTime;
        SDFtimer -= Time.deltaTime;

        if (rightPressed) moveRight();
        if (leftPressed) moveLeft();
        if (softDropping) softDrop();
    }

    public void hold()
    {
        if (previewUI.heldPiece != null) {
            if (holdCooldown == false)
            {
                int heldInt = previewUI.heldPieceScript.pieceIndex;
                Debug.Log(heldInt);
                pieceScript.destroyThis();
                previewUI.holdPiece(Instantiate(gameManager.getPiece(gameManager.currentInt)));
                gameManager.currentInt = heldInt;
                transform.position = grid.getWorldPosition(Mathf.Floor((gameManager.xLength - 1) / 2), gameManager.yLength - 1);
                currentObject = Instantiate(gameManager.getPiece(heldInt), transform, false);
                pieceScript = currentObject.GetComponent<PieceScript>();
                pieceScript.gameManager = gameManager;
                pieceScript.grid = grid;
                pieceScript.pieceIndex = gameManager.currentInt;
                holdCooldown = true;
            }
        }
        else
        {
            pieceScript.destroyThis();
            previewUI.holdPiece(Instantiate(gameManager.getPiece(gameManager.currentInt)));
            currentObject = null;
            newPiece();
            holdCooldown = true;
        }
    }

    public void newPiece()
    {
        if(currentObject != null)
        {
            Destroy(currentObject);
        }
        gameManager.bag.RemoveAt(0);
        gameManager.currentInt = gameManager.bag[0];
        if (gameManager.bag.Count <= gameManager.previews)
        {
            gameManager.addToBag();
        }
        transform.position = grid.getWorldPosition(Mathf.Floor((gameManager.xLength - 1) / 2), gameManager.yLength - 1);
        currentObject = Instantiate(gameManager.getPiece(gameManager.currentInt), transform, false);
        pieceScript = currentObject.GetComponent<PieceScript>();
        pieceScript.gameManager = gameManager;
        pieceScript.grid = grid;
        pieceScript.pieceIndex = gameManager.currentInt;
        previewUI.updatePreviews();
        //StopCoroutine(Gravity());
        //StartCoroutine(Gravity());
    }

    public void hardDrop()
    {
        while(!pieceScript.checkDown())
        {
            transform.position = transform.position + new Vector3(0, -gameManager.cellSize, 0);
        }
        gameManager.placePiece();
    }

    public void placeTiles()
    {
        for (int i = 0; i < pieceScript.tiles.Length; i++)
        {
            pieceScript.tiles[i].transform.parent = null;
            int x = Mathf.RoundToInt(grid.getGridCoordinate(pieceScript.tiles[i].transform.position).x);
            int y = Mathf.RoundToInt(grid.getGridCoordinate(pieceScript.tiles[i].transform.position).y);
            grid.setPlaced(x, y, pieceScript.tiles[i]);
        }
        holdCooldown = false;
        newPiece();
    }

    public void rotateRight()
    {
        pieceScript.rotateRight();
    }
    public void rotateLeft()
    {
        pieceScript.rotateLeft();
    }

    public void rotate180()
    {
        pieceScript.rotate180();
    }

    public void moveLeft()
    {
        if (!pieceScript.checkLeft())
        {
            if (DAStimer <= 0 && ARR == 0)
            {
                while (!pieceScript.checkLeft())
                {
                    transform.position = transform.position + new Vector3(-gameManager.cellSize, 0, 0);
                }
            }
            else if (DAStimer <= 0 && ARRtimer <= 0)
            {
                ARRtimer = ARR;
                transform.position = transform.position + new Vector3(-gameManager.cellSize, 0, 0);
            }
        }
    }

    void moveRight()
    {
        if (!pieceScript.checkRight())
        {
            if (DAStimer <= 0 && ARR == 0)
            {
                while (!pieceScript.checkRight())
                {
                    transform.position = transform.position + new Vector3(gameManager.cellSize, 0, 0);
                }
            }
            else if (DAStimer <= 0 && ARRtimer <= 0)
            {
                ARRtimer = ARR;
                transform.position = transform.position + new Vector3(gameManager.cellSize, 0, 0);
            }
        }
    }
    public void softDrop()
    {
        if (SDF == 0)
        {
            while (!pieceScript.checkDown())
            {
                transform.position = transform.position + new Vector3(0, -gameManager.cellSize, 0);
            }
        }
        else if (SDFtimer <= 0)
        {
            SDFtimer = SDF;
            if (!pieceScript.checkDown())
            {
                transform.position = transform.position + new Vector3(0, -gameManager.cellSize, 0);
            }
        }
    }

    public void debugTiles()
    {
        Debug.Log(grid.getGridCoordinate(currentObject.transform.position) + " MAIN");
        for (int i = 0; i < pieceScript.tiles.Length; i++)
            {
            Debug.Log(grid.getGridCoordinate(pieceScript.tiles[i].transform.position));
        }
    }
    IEnumerator Gravity()
    {
        if (transform.position.y >= grid.getWorldPosition(0,1).y)
        {
            if (!pieceScript.checkDown())
            {
                transform.position = transform.position + new Vector3(0, -gameManager.cellSize, 0);
            }
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Gravity());
        }
        yield return null;
    }
}

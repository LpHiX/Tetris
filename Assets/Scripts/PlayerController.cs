using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject game;

    private GameManager gameManager;
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

    void Start()
    {
        gameManager = game.GetComponent<GameManager>();
        grid = gameManager.grid;
        transform.position = grid.getWorldPosition(Mathf.Floor((gameManager.xLength-1)/2), gameManager.yLength-1);
        currentObject = Instantiate(gameManager.getPiece(GameManager.getCurrentInt()), transform, false) ;
        pieceScript = currentObject.GetComponent<PieceScript>();
        pieceScript.gameManager = gameManager;
        pieceScript.grid = grid;
        pieceScript.pieceIndex = GameManager.getCurrentInt();
        //StartCoroutine(Gravity());
    }

    // Update is called once per frame
    void Update()
    {
        DAStimer -= Time.deltaTime;
        ARRtimer -= Time.deltaTime;
        SDFtimer -= Time.deltaTime;

        if (Input.GetButton("left")) moveLeft();
        if (Input.GetButton("right")) moveRight();
        if (Input.GetButton("softDrop")) inputDown();

        if (Input.GetButtonDown("rotateLeft")) rotateLeft();
        if (Input.GetButtonDown("rotateRight")) rotateRight();
        if (Input.GetButtonDown("rotate180")) rotate180();
        if (Input.GetButtonDown("hardDrop")) hardDrop() ;

        if (Input.GetKeyDown("v")) debugTiles();
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
        Destroy(currentObject);
        transform.position = grid.getWorldPosition(Mathf.Floor((gameManager.xLength - 1) / 2), gameManager.yLength - 1);
        currentObject = Instantiate(gameManager.getPiece(GameManager.getCurrentInt()), transform, false);
        pieceScript = currentObject.GetComponent<PieceScript>();
        pieceScript.gameManager = gameManager;
        pieceScript.grid = grid;
        pieceScript.pieceIndex = GameManager.getCurrentInt();
        //StopCoroutine(Gravity());
        //StartCoroutine(Gravity());
    }

    void rotateRight()
    {
        pieceScript.rotateRight();
    }
    void rotateLeft()
    {
        pieceScript.rotateLeft();
    }

    void rotate180()
    {
        pieceScript.rotate180();
    }

    void moveLeft()
    {
        if (!pieceScript.checkLeft())
        {
            if (Input.GetButtonDown("left"))
            {
                DAStimer = DAS;
                ARRtimer = ARR;
                transform.position = transform.position + new Vector3(-gameManager.cellSize, 0, 0);
            }
            if (DAStimer <= 0 && ARRtimer <= 0)
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
            if (Input.GetButtonDown("right"))
            {
                DAStimer = DAS;
                ARRtimer = ARR;
                transform.position = transform.position + new Vector3(gameManager.cellSize, 0, 0);
            }
            if (DAStimer <= 0 && ARRtimer <= 0)
            {
                ARRtimer = ARR;
                transform.position = transform.position + new Vector3(gameManager.cellSize, 0, 0);
            }
        }
    }
    void inputDown()
    {
        if (SDFtimer <= 0)
        {
            SDFtimer = SDF;
            if (!pieceScript.checkDown())
            {
                transform.position = transform.position + new Vector3(0, -gameManager.cellSize, 0);
            }
        }
    }

    void debugTiles()
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

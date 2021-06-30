using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject game;

    private GameManager gameManager;
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
        transform.position = gameManager.grid.getWorldPosition(Mathf.Floor((gameManager.xLength-1)/2), gameManager.yLength-1);
        currentObject = Instantiate(gameManager.getPiece(GameManager.getCurrentInt()), transform, false) ;
        pieceScript = currentObject.GetComponent<PieceScript>();
        pieceScript.gameManager = gameManager;
        StartCoroutine(Gravity());
    }

    // Update is called once per frame
    void Update()
    {
        DAStimer -= Time.deltaTime;
        ARRtimer -= Time.deltaTime;
        SDFtimer -= Time.deltaTime;

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (Input.GetKey("a")) moveLeft();
        if (Input.GetKey("d")) moveRight();
        if (Input.GetKey("s")) inputDown();

        if (Input.GetKeyDown("v")) debugTiles();
    }
    void FixedUpdate()
    {
    }

    void moveLeft()
    {
        if (pieceScript.checkLeft())
        {
            if (Input.GetKeyDown("a"))
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
        if (pieceScript.checkRight())
        {
            if (Input.GetKeyDown("d"))
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
            moveDown();
        }
    }
    void moveDown()
    {
        if (pieceScript.checkDown())
        {
            transform.position = transform.position + new Vector3(0, -gameManager.cellSize, 0);
        }
    }

    void debugTiles()
    {
        for(int i = 0; i < pieceScript.tiles.Length; i++)
            {
            Debug.Log(gameManager.grid.getGridCoordinate(pieceScript.tiles[i].transform.position));
        }
    }
    IEnumerator Gravity()
    {
        if (transform.position.y >= gameManager.grid.getWorldPosition(0,1).y)
        {
            moveDown();
            yield return new WaitForSeconds(0.5f);

            StartCoroutine(Gravity());
        }
        yield return null;
    }
}

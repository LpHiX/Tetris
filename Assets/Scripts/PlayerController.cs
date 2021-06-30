using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject game;

    private GameManager gameManager;
    private int piece;
    private GameObject currentObject;

    public float DAS = .133f;
    public float ARR = .05f;
    public float SDF = .05f;

    private float DAStimer = 0;
    private float ARRtimer = 0;
    private float SDFtimer = 0;

    void Start()
    {
        gameManager = game.GetComponent<GameManager>();
        transform.position = getWorldPosition(Mathf.Floor((gameManager.xLength-1)/2), gameManager.yLength-1);
        currentObject = Instantiate(gameManager.getPiece(GameManager.getCurrentInt()), transform, false) ;
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
    }
    void FixedUpdate()
    {
    }

    void moveLeft()
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
    void inputDown()
    {
        if (SDFtimer <= 0)
        {
            SDFtimer = SDF;
            moveDown();
        }
    }
    void moveRight()
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

    void moveDown()
    {
        transform.position = transform.position + new Vector3(0, -gameManager.cellSize, 0);
    }

    IEnumerator Gravity()
    {
        if (transform.position.y <= getWorldPosition(0,1).y)
        {
            Debug.Log("Over");
        }
        else
        {
            moveDown();
            Debug.Log("Down");
            yield return new WaitForSeconds(0.5f);

            StartCoroutine(Gravity());
        }
        yield return null;
    }
    public Vector3 getWorldPosition(float x, float y)
    {
        return gameManager.transform.TransformPoint(new Vector3(x, y) * gameManager.cellSize);  
    }
}

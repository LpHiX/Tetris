using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Gravity(transform));
    }

    // Update is called once per frame
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown("a"))
        {
            transform.position = transform.position + new Vector3(-0.398f, 0, 0);
        }
        if (Input.GetKeyDown("d"))
        {
            transform.position = transform.position + new Vector3(+0.398f, 0, 0);
        }
    }

    IEnumerator Gravity(Transform target)
    {
        if (transform.position.y < -4.352)
        {
            Debug.Log("Over");
        }
        else
        {
            target.position = transform.position = transform.position + new Vector3(0, -0.398f, 0);
            Debug.Log("Down");
            yield return new WaitForSeconds(0.5f);

            StartCoroutine(Gravity(transform));
        }
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataScript : MonoBehaviour
{

    public int width = 10;
    public int height = 20;
    public int previews = 5;
    public float DAS = .133f;
    public float ARR = .05f;
    public float SDF = .05f;

    public Controls controls;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Data");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        controls = new Controls();
        DontDestroyOnLoad(this.gameObject);
    }

    public void readWidth(string s)
    {
        int.TryParse(s, out width);
    }
    public void readHeight(string s)
    {
        int.TryParse(s, out height);
    }
    public void readPreviews(string s)
    {
        int.TryParse(s, out previews);
    }
    public void readDAS(string s)
    {
        float.TryParse(s, out DAS);
    }
    public void readARR(string s)
    {
        float.TryParse(s, out ARR);
    }
    public void readSDF(string s)
    {
        float.TryParse(s, out SDF);
    }

}

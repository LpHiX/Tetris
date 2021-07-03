using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {
    public const int sortingOrderDefault = 5000;

    public static TextMesh createWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAlignment = TextAlignment.Center, int sortingOrder = sortingOrderDefault)
    {
        if (color == null) color = Color.white;
        return createWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }
    public static TextMesh createWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize * 10;
        textMesh.characterSize = 0.1f;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        Debug.Log(textMesh.transform.position);
        return textMesh;
    }
    
    // Gets the position of a point, after it is rotated around a given point by a given angle
    public static Vector3 returnRotate(Vector3 target, Vector3 origin, float angle)
    {
        Vector3 displacement = target - origin;
        //Apply rotation matrix
        float x = displacement.x * Mathf.Cos(angle) - displacement.y * Mathf.Sin(angle);
        float y = displacement.x * Mathf.Sin(angle) + displacement.y * Mathf.Cos(angle);
        return origin + new Vector3(x, y, displacement.z);
    }

    public static void drawPoint(Vector3 point, float size, float time)
    {
        Vector3 horizontal = point + new Vector3(-size/2, 0, 0);
        Vector3 vertical = point + new Vector3(0, -size/2, 0);
        Debug.DrawLine(horizontal, horizontal + new Vector3(size, 0, 0), Color.white, time);
        Debug.DrawLine(vertical, vertical + new Vector3(0, size, 0), Color.white, time);
    }
    public static Vector3 getWallKickData(int from, int to, int testNumber)
    {
        Vector3[,] wallKickData = new Vector3[,] {
            {new Vector3(0,0), new Vector3(-1,0), new Vector3(-1,1), new Vector3(0,-2), new Vector3(-1,-2)},
            {new Vector3(0,0), new Vector3(1,0), new Vector3(1,-1), new Vector3(0,2), new Vector3(1,2)},
            {new Vector3(0,0), new Vector3(1,0), new Vector3(1,-1), new Vector3(0,2), new Vector3(1,2)},
            {new Vector3(0,0), new Vector3(-1,0), new Vector3(-1,1), new Vector3(0,-2), new Vector3(-1,-2)},
            {new Vector3(0,0), new Vector3(1,0), new Vector3(1,1), new Vector3(0,-2), new Vector3(1,-2)},
            {new Vector3(0,0), new Vector3(-1,0), new Vector3(-1,-1), new Vector3(0,2), new Vector3(-1,2) },
            {new Vector3(0,0), new Vector3(-1,0), new Vector3(-1,-1), new Vector3(0,2), new Vector3(-1,2) },
            {new Vector3(0,0), new Vector3(1,0), new Vector3(1,1), new Vector3(0,-2), new Vector3(1,-2) }};
        var rotation = (from, to);
        switch (rotation)
        {
            case var t when t == (0, 1):
                return wallKickData[0, testNumber];
            case var t when t == (1, 0):
                return wallKickData[1, testNumber];
            case var t when t == (1, 2):
                return wallKickData[2, testNumber];
            case var t when t == (2, 1):
                return wallKickData[3, testNumber];
            case var t when t == (2, 3):
                return wallKickData[4, testNumber];
            case var t when t == (3, 2):
                return wallKickData[5, testNumber];
            case var t when t == (3, 0):
                return wallKickData[6, testNumber];
            case var t when t == (0, 3):
                return wallKickData[7, testNumber];
            default:
                return new Vector3(0, 0);
        }
    }
    public static Vector3 getWallKickDataI(int from, int to, int testNumber)
    {
        Vector3[,] wallKickDataI = new Vector3[,]    {
        {new Vector3(0,0), new Vector3(-2,0), new Vector3(1,0), new Vector3(-2,-1), new Vector3(1,2)},
        {new Vector3(0,0), new Vector3(2,0), new Vector3(-1,0), new Vector3(2,1), new Vector3(-1,-2)},
        {new Vector3(0,0), new Vector3(-1,0), new Vector3(2,0), new Vector3(-1,2), new Vector3(2,-1)},
        {new Vector3(0,0), new Vector3(1,0), new Vector3(-2,0), new Vector3(1,-2), new Vector3(-2,1)},
        {new Vector3(0,0), new Vector3(2,0), new Vector3(-1,0), new Vector3(2,1), new Vector3(-1,-2)},
        {new Vector3(0,0), new Vector3(-2,0), new Vector3(1,0), new Vector3(-2,-1), new Vector3(1,2)},
        {new Vector3(0,0), new Vector3(1,0), new Vector3(-2,0), new Vector3(1,-2), new Vector3(-2,1)},
        {new Vector3(0,0), new Vector3(-1,0), new Vector3(2,0), new Vector3(-1,2), new Vector3(2,-1)}};
        var rotation = (from, to);
        switch (rotation)
        {
            case var t when t == (0, 1):
                return wallKickDataI[0, testNumber];
            case var t when t == (1, 0):
                return wallKickDataI[1, testNumber];
            case var t when t == (1, 2):
                return wallKickDataI[2, testNumber];
            case var t when t == (2, 1):
                return wallKickDataI[3, testNumber];
            case var t when t == (2, 3):
                return wallKickDataI[4, testNumber];
            case var t when t == (3, 2):
                return wallKickDataI[5, testNumber];
            case var t when t == (3, 0):
                return wallKickDataI[6, testNumber];
            case var t when t == (0, 3):
                return wallKickDataI[7, testNumber];
            default:
                return new Vector3(0, 0);
        }
    }
}

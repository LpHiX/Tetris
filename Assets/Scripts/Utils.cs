﻿using System.Collections;
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
}

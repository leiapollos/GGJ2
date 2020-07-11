using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelSection : MonoBehaviour
{
    public Transform StartPoint, EndPoint;

    public Vector3 startPos { get { return StartPoint.position; } }
    public Vector3 endPos { get { return EndPoint.position; } }

    public Vector3 StartOffset()
    {
        return transform.position - StartPoint.position;
    }

    public Vector3 EndOffset()
    {
        return transform.position - EndPoint.position;
    }


}

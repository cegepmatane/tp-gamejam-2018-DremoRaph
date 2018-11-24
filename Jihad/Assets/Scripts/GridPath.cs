using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPath : MonoBehaviour
{
    
    public Color PathColor = Color.green;
    public bool displayPath = true;

    public Transform[] pathPoints;

    

    private void Start()
    {
        PathsManager.instance.listPaths.Add(this);
    }

    private void OnDrawGizmosSelected()
    {
        if (pathPoints.Length <= 1) 
        if (!displayPath) return;

        Vector3 previous = pathPoints[0].position;
        Gizmos.color = PathColor;

        for(int i = 1; i < pathPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(previous, pathPoints[i].position);
            previous = pathPoints[i].position;
        }
    }
}
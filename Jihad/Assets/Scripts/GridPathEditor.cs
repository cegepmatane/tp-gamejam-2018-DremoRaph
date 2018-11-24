using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridPath))]
public class GridPathEditor : Editor {

    private GridPath path;

    private void OnEnable()
    {
        path = (GridPath)target;
    }
}

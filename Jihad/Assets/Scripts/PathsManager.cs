using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathsManager : MonoBehaviour {

    public List<GridPath> listPaths;

    public static PathsManager instance;

    private void Awake()
    {
        listPaths = new List<GridPath>();
        instance = this;
    }
}

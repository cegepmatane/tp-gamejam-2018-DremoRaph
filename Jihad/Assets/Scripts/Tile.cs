using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public MapGrid.GridPoint gridPoint;
    public int baseCost;
    [HideInInspector]
    public int posX, posY;

    private void Awake()
    {
        //TOTO Calc posX Y
    }
}


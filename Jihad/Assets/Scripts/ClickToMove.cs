using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class ClickToMove : MonoBehaviour
{
    public GameObject destinationPoint;
    public MapGrid grid;

    private PlayerController playerCtrl;

    private float nextFire;
        
    // Use this for initialization
    void Awake()
    {
        playerCtrl = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MapGrid.GridPoint point = grid.WorldPointToGridPoint(mouseWorldPos);
            if(grid.tableauTiles[point.x, point.y].baseCost == -1 ) return;

            destinationPoint.transform.position = mouseWorldPos;
            playerCtrl.InitPathfinding();
        }
    }
}

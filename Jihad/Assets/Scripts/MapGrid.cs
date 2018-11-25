using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid : MonoBehaviour
{
    private List<Tile> Tiles;
    public Tile[,] tableauTiles;
    public List<Tile> spawnTiles;

    private void Awake()
    {
        Tiles = new List<Tile>(GetComponentsInChildren<Tile>());
        spawnTiles = new List<Tile>();
        tableauTiles = new Tile[GridSize, GridSize];

        foreach (Tile t_Tile in Tiles)
        {
            t_Tile.gridPoint = WorldPointToGridPoint(t_Tile.transform.position);
            tableauTiles[t_Tile.gridPoint.x, t_Tile.gridPoint.y] = t_Tile;
            if (t_Tile.baseCost > 0) spawnTiles.Add(t_Tile);
        }
    }

    public List<Tile> GetSpawnTiles()
    {
        return spawnTiles;
    }

    public Tile GetTileFromGrid(GridPoint a_gridPoint)
    {
        return tableauTiles[a_gridPoint.x, a_gridPoint.y];
    }

    public Tile GetTileFromGrid(int a_X, int a_Y)
    {
        return tableauTiles[a_X, a_Y];
    }
    // Une classe pour pouvoir return null si on clique en dehors de la grille
    public class GridPoint
    {
        public int x, y;

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            GridPoint objGP = (GridPoint)obj;

            if(this.x != objGP.x) return false;
            if(this.y != objGP.y) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public float CellHeight = 1f;
    public float CellWidth = 1f;
    public Color GridColor = Color.yellow;
    public bool showGrid = true;

    public int GridSize = 100;

    public GameObject[] availableTiles;
    public int selectedTile;

    private void OnDrawGizmos()
    {
        if (!showGrid) return;
        Gizmos.color = GridColor;

        DrawGrid();


    }

    private void DrawGrid()
    {
        for (int i = 0; i <= GridSize; i++)
        {
            // Draw Line Horizontal
            //MODIFIED
            float t_PosX = (i * CellWidth) - (GridSize * CellWidth / 2);

            float t_LineHeight = CellHeight * GridSize;
            Gizmos.DrawLine(
                        new Vector3(t_PosX, -(t_LineHeight / 2), 0),
                        new Vector3(t_PosX, t_LineHeight / 2, 0)
                        );

            // Draw Line Vertical
            //MODIFIED
            float t_PosY = (i * CellHeight) - (GridSize * CellHeight / 2);

            float t_LineWidth = CellWidth * GridSize;
            Gizmos.DrawLine(
                        new Vector3(-(t_LineWidth / 2), t_PosY, 0),
                        new Vector3(t_LineWidth / 2, t_PosY, 0)
                        );
        }
    }

    public GridPoint WorldPointToGridPoint(Vector2 a_Point)
    {
        // La moitier de la taille de la grille
        float t_GridHalfTotalWidth = CellWidth * GridSize / 2;
        float t_GridHalfTotalHeight = CellHeight * GridSize / 2;

        // return si on clique en dehors de la grille
        if (a_Point.x < -t_GridHalfTotalWidth || a_Point.x > t_GridHalfTotalWidth)
            return null;
        if (a_Point.y < -t_GridHalfTotalHeight || a_Point.y > t_GridHalfTotalHeight)
            return null;

        var t_gridPoint = new GridPoint();

        t_gridPoint.x = (int)((a_Point.x + t_GridHalfTotalWidth) / CellWidth);
        t_gridPoint.y = (int)((a_Point.y + t_GridHalfTotalHeight) / CellHeight);
        // Inverse
        t_gridPoint.y = -t_gridPoint.y + GridSize - 1;

        return t_gridPoint;
    }

    public Vector3 GridPointToWorldPoint(GridPoint a_gridPoint)
    {
        float t_GridHalfTotalWidth = CellWidth * GridSize / 2;
        float t_GridHalfTotalHeight = CellHeight * GridSize / 2;

        Vector2 t_worldPoint;

        t_worldPoint.x = (a_gridPoint.x * CellWidth) + (CellWidth / 2) - t_GridHalfTotalWidth;
        t_worldPoint.y = -((a_gridPoint.y * CellHeight) + (CellHeight / 2) - t_GridHalfTotalHeight);

        return t_worldPoint;
    }
}

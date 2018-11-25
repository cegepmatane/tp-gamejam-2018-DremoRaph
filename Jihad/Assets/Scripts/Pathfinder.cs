using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class Pathfinder : MonoBehaviour {

    public Transform endTransform;
    public MapGrid grid;
    public bool DebugMode = true;
    public GameObject transformObj;

    private Tile m_endTile;
    public Node[,] m_nodes;
    private Path currentPath;
    private bool pathset = false;

    public void Start()
    {
        if (endTransform == null)
        {
            GameObject obj = new GameObject();
            obj.name = "DestinationPlayer";
            endTransform = obj.transform;

            transformObj = obj;
        }

        if(grid == null)
            grid = FindObjectOfType<MapGrid>();
    }

    private void InitNodes(int x, int y)
    {
        m_nodes = new Node[x, y];

        for (int i = 0; i < grid.GridSize; i++) 
            for (int j = 0; j < grid.GridSize; j++)
            {
                if (grid.tableauTiles[i, j] == null) continue;

                m_nodes[i, j] = new Node();
                m_nodes[i, j].tile = grid.tableauTiles[i, j];
            }
       
        foreach (Node n in m_nodes)
            if (n == null) continue;
            else n.h = Heuristic(n.tile.gridPoint.x, n.tile.gridPoint.y, m_endTile.gridPoint.x, m_endTile.gridPoint.y);
        
    }

    public Path GetPath(Transform a_Entity)
    {
        MapGrid.GridPoint t_startGridPoint = grid.WorldPointToGridPoint(a_Entity.position);
        Tile t_startTile = grid.GetTileFromGrid(t_startGridPoint);

        MapGrid.GridPoint t_endGridPoint = grid.WorldPointToGridPoint(endTransform.position);
        m_endTile = grid.GetTileFromGrid(t_endGridPoint);

        InitNodes(grid.GridSize, grid.GridSize);

        if (t_startTile == null)
        {
            Debug.LogError("m_startTile == null");
            return null;
        }
        if (m_endTile == null)
        {
            Debug.LogError("m_endTile == null");
            return null;
        }

        if (DebugMode)
        {
            t_startTile.GetComponent<SpriteRenderer>().color = Color.green;
            m_endTile.GetComponent<SpriteRenderer>().color = Color.red;
        }

        List<Node> t_openList = new List<Node>();
        List<Node> t_closedList = new List<Node>();    

        Node startNode = m_nodes[t_startTile.gridPoint.x, t_startTile.gridPoint.y];
        Node endNode = m_nodes[m_endTile.gridPoint.x, m_endTile.gridPoint.y];
        Node current = new Node();
        t_openList.Add(startNode);

        bool done = false;
        while(!done)
        {
            if(t_openList.Count == 0)
            {
                Debug.LogError("nopath");
                return null;
            }

            int minF = t_openList.Min(t => t.f);
            current = t_openList.Find(t => t.f == minF);

            t_openList.Remove(current);
            t_closedList.Add(current);

            if(current.tile == m_endTile)
            {
                done = true;
                break;
            }

            List<Node> t_neighbours = GetNeighbours(current);
            foreach(Node n in t_neighbours)
            {
                if (n.tile.baseCost == -1 || t_closedList.Contains(n)) continue;

                int neighbourCost = (n.tile.gridPoint.x == current.tile.gridPoint.x) || (n.tile.gridPoint.y == current.tile.gridPoint.y) ? 10 : 14;

                int pathCost = current.g + (neighbourCost * n.tile.baseCost);
                if (!t_openList.Contains(n) || pathCost < n.g)
                {
                    n.g = pathCost;
                    n.f = n.g + n.h;
                    n.parent = current;

                    if(!t_openList.Contains(n)) t_openList.Add(n);
                }
            }
        }

        Path t_path = new Path();

        if (endNode.parent != null)
        {
            //found
            Debug.Log("Fiound");
            t_path.tiles = new List<Tile>();

            current = endNode;
            while(current != startNode)
            { 
                t_path.tiles.Add(current.tile);
                current = current.parent;
            }
            t_path.tiles.Add(startNode.tile);
            t_path.tiles.Reverse();
        }

        currentPath = t_path;
        return t_path;
    }

    private int Heuristic(int a_startX, int a_startY, int a_endX, int a_endY)
    {
        int dX = Mathf.Abs(a_startX - a_endX);
        int dY = Mathf.Abs(a_startY - a_endY);

        return dX + dY;
    }

    private List<Node> GetNeighbours(Node a_node)
    {
        int xMin, xMax, yMin, yMax;
        List<Node> t_list = new List<Node>();

        xMin = a_node.tile.gridPoint.x - 1 < 0 ? 0 : a_node.tile.gridPoint.x - 1;
        xMax = a_node.tile.gridPoint.x + 1 >= grid.GridSize ? grid.GridSize - 1 : a_node.tile.gridPoint.x + 1;
        yMin = a_node.tile.gridPoint.y - 1 < 0 ? 0 : a_node.tile.gridPoint.y - 1;
        yMax = a_node.tile.gridPoint.y + 1 >= grid.GridSize ? grid.GridSize - 1 : a_node.tile.gridPoint.y + 1;

        for (int i = xMin; i <= xMax; i++)
            for (int j = yMin; j <= yMax; j++)
                if(m_nodes[i, j] != null)
                    t_list.Add(m_nodes[i, j]);

        return t_list;
    }

    private void OnDrawGizmos()
    {
        if (!pathset) return;
        Gizmos.color = Color.cyan;

        for (int i = 0; i < currentPath.tiles.Count - 1; i++)
        {
            Gizmos.DrawLine(currentPath.tiles[i].transform.position, currentPath.tiles[i + 1].transform.position);
        }
    }

    public class Node
    {
        public Tile tile;
        public int f, g, h;
        public Node parent;
    }
}

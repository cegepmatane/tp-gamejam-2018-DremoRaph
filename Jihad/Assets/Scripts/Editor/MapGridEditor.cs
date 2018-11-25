using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGrid))]

public class MapGridEditor : Editor
{
    private MapGrid grid;

    private void OnEnable()
    {
        grid = (MapGrid)target;
    }

    private void OnSceneGUI()
    {
        Event e = Event.current;

        // L'ID du control actuel
        int controlId = GUIUtility.GetControlID(FocusType.Passive);

        if (e.type == EventType.MouseDown && e.control)
        {
            // Empeche de selectionner un autre objet en cliquant dans la scene
            GUIUtility.hotControl = controlId;

            // Position cliquée dans le world
            Vector2 t_ClickPos = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
            MapGrid.GridPoint t_ClickedGridPoint = grid.WorldPointToGridPoint(t_ClickPos);

            if (t_ClickedGridPoint == null)
            {
                Debug.Log("Clicked outside the grid");
                //Stop l'exécution
                return;
            }

            if (grid.selectedTile < 0 || 
                grid.selectedTile >= grid.availableTiles.Length)
            {
                Debug.Log("Invalid tile selected.");
                //Stop l'exécution
                return;
            }

            Vector3 t_tilePos = grid.GridPointToWorldPoint(t_ClickedGridPoint);
            GameObject t_newTile = Instantiate(grid.availableTiles[grid.selectedTile], t_tilePos, Quaternion.identity);
            t_newTile.transform.parent = grid.transform;

            Debug.Log("Control + Clicked GridPoint (" + t_ClickedGridPoint.x + ", " + t_ClickedGridPoint.y + ")");
        }
    }

    
}

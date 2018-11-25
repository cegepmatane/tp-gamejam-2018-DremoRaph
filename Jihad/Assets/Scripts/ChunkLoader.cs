using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour {

    public GameObject[] UsableChunkList;
    public GameObject Container;
    //public GameObject MapGridContainer;
    public GameObject MapGridPrefab;
    public GameObject CameraPrefab;
    public GameObject SpawnerPrefab;
    private MapGrid ChunkMapGrid;
    public GameObject Player;
    
    private float[,] ChunkPositionList;

    private void Awake()
    {
        //ChunkMapGrid = MapGridContainer.GetComponent<MapGrid>();
    }

    // Use this for initialization
    void Start () {

        ChunkPositionList = new float[8, 8];
        Debug.Log(ChunkPositionList[0, 0]);
        for (int i = 0; i < 8; i++)
        {
            for (int k = 0; k < 8; k++)
            {
                int t_RandomChunkNumber = Random.Range(0, UsableChunkList.Length);
                GameObject t_NewChunk;
                if (ChunkPositionList[i, k] != 0)
                {
                    continue;
                }
                if (i == 0 && k == 0)
                {
                    t_NewChunk = Instantiate(UsableChunkList[0], new Vector3((i * 10) - 35, (k * 10) - 35, 0), Quaternion.identity) as GameObject;
                    t_NewChunk.transform.parent = Container.transform;
                    ChunkPositionList[i, k] = 1;
                    Debug.Log(ChunkPositionList[0, 0]);
                    continue;
                }
                if (k > 0)
                {
                    //Si le chunk en dessous est une intersection, on génère une route normale
                    if (ChunkPositionList[i, k - 1] == 1)
                    {
                        t_NewChunk = Instantiate(UsableChunkList[1], new Vector3((i * 10) - 35, (k * 10) - 35, 0), Quaternion.identity) as GameObject;
                        t_NewChunk.transform.parent = Container.transform;
                        continue;

                    }
                    //Si le chunk en dessous est une route horizontale, on ne peut pas générer ni d'intersection, ni de route horizontale
                    if (ChunkPositionList[i, k - 1] == 2.5)
                    {
                        while (t_RandomChunkNumber == 0)
                        {
                            t_RandomChunkNumber = Random.Range(0, UsableChunkList.Length);
                        }
                        if (t_RandomChunkNumber == 2)
                        {
                            t_NewChunk = Instantiate(UsableChunkList[1], new Vector3((i * 10) - 35, (k * 10) - 35, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
                            t_NewChunk.transform.parent = Container.transform;
                            ChunkPositionList[i, k] = 2.5f;
                            continue;
                        }

                    }
                }
                if (i > 0)
                {
                    //Si le chunk à gauche est une intersection, les chunks de route seront tourné de 90 degrés pour y être connecter
                    if (ChunkPositionList[i - 1, k] == 1)
                    {                       
                        if (t_RandomChunkNumber == 1)
                        {
                            t_NewChunk = Instantiate(UsableChunkList[1], new Vector3((i * 10) - 35, (k * 10) - 35, 0), Quaternion.Euler(0, 0, 90)) as GameObject;                         
                            t_NewChunk.transform.parent = Container.transform;
                            ChunkPositionList[i, k] = 2.5f;
                            continue;
                        }
                    
                    }
                    //Si le chunk à gauche est une route allant vers le haut, on génère un chunk de batiment sur son long
                    if (ChunkPositionList[i - 1, k] == 2)
                    {
                        t_NewChunk = Instantiate(UsableChunkList[2], new Vector3((i * 10) - 35, (k * 10) - 35, 0), Quaternion.identity) as GameObject;
                        t_NewChunk.transform.parent = Container.transform;
                        ChunkPositionList[i, k] = 3;
                        continue;
                    }
                    //Si le chunk à gauche est une route à l'horizontale, on ne peut pas avoir de route verticale
                    if (ChunkPositionList[i - 1, k] == 2.5)
                    {
                        if (t_RandomChunkNumber == 2)
                        {
                            t_NewChunk = Instantiate(UsableChunkList[1], new Vector3((i * 10) - 35, (k * 10) - 35, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
                            t_NewChunk.transform.parent = Container.transform;
                            ChunkPositionList[i, k] = 2.5f;
                            continue;
                        }
                        
                    }
                    

                }
                
                t_NewChunk = Instantiate(UsableChunkList[t_RandomChunkNumber], new Vector3((i * 10) - 35, (k * 10) - 35, 0), Quaternion.identity) as GameObject;
                t_NewChunk.transform.parent = Container.transform;
                ChunkPositionList[i, k] = t_RandomChunkNumber + 1;
            }
        }



        GenerateMapGrid();


    }

    public void GenerateMapGrid()
    {
        GameObject t_MapGrid = Instantiate(MapGridPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        ChunkMapGrid = t_MapGrid.GetComponent<MapGrid>();
        ChunkMapGrid.Container = this.Container;
        ChunkMapGrid.Activate();
        GeneratePlayer();
    }

    public void GeneratePlayer()
    {
        GameObject t_NewPlayer = Instantiate(Player, new Vector3(5, 5, 0), Quaternion.identity) as GameObject;

        GameObject t_NewCam = Instantiate(CameraPrefab, new Vector3(5, 5, -10), Quaternion.identity) as GameObject;
        CompleteCameraController CameraController = t_NewCam.GetComponent<CompleteCameraController>();
        CameraController.player = t_NewPlayer;
        
        ClickToMove t_ClickToMove = t_NewPlayer.GetComponent<ClickToMove>();
        Pathfinder t_PathFinder = t_NewPlayer.GetComponentInChildren<Pathfinder>();
        t_PathFinder.grid = ChunkMapGrid;
        t_ClickToMove.destinationPoint = t_PathFinder.GetTransformObj();
        
        t_ClickToMove.grid = ChunkMapGrid;

        GenerateEnemySpawner();
    }

    public void GenerateEnemySpawner()
    {
        GameObject t_NewSpawner = Instantiate(SpawnerPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
    }

    
}

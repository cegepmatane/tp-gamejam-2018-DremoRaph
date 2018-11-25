using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour {

    public GameObject[] UsableChunkList;
    public GameObject Container;
    private float[,] ChunkPositionList;
    
    
    
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
                        t_NewChunk = Instantiate(UsableChunkList[2], new Vector3((i * 10) - 35, (k * 10) - 35, 0), Quaternion.Euler(0, 0, 90)) as GameObject;
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




    }

    // Update is called once per frame
    void Update () {
		
	}
}

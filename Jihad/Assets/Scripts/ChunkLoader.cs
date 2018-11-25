using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour {

    public GameObject[] UsableChunkList;
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
                if (ChunkPositionList[i, k] != 0)
                {
                    continue;
                }
                if (i == 0 && k == 0)
                {
                    Instantiate(UsableChunkList[0], new Vector3((i * 10) - 35, (k * 10) - 35, 0), Quaternion.identity);
                    ChunkPositionList[i, k] = 1;
                    Debug.Log(ChunkPositionList[0, 0]);
                    continue;
                }
                if (i > 0)
                {
                    if (ChunkPositionList[i - 1, k] == 1)
                    {                       
                        if (t_RandomChunkNumber == 1)
                        {
                            Instantiate(UsableChunkList[1], new Vector3((i * 10) - 35, (k * 10) - 35, 0), Quaternion.Euler(0, 0, 90));
                            ChunkPositionList[i, k] = 2.5f;
                            continue;
                        }
                    
                    }
                }
                

                    
                

                Instantiate(UsableChunkList[t_RandomChunkNumber], new Vector3((i * 10) - 35, (k * 10) - 35, 0), Quaternion.identity);
                ChunkPositionList[i, k] = 1;
            }
        }




    }

    // Update is called once per frame
    void Update () {
		
	}
}

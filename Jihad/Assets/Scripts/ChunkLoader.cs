using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour {

    public GameObject[] ChunkList;
    
    
    
    // Use this for initialization
	void Start () {

        
        for (int i = 0; i < 8; i++)
        {
            for (int k = 0; k < 8; k++)
            {
               Instantiate(ChunkList[Random.Range(0, 2)], new Vector3((i*10) - 35 , (k * 10) - 35, 0),  Quaternion.identity);
                
            }
        }




    }

    // Update is called once per frame
    void Update () {
		
	}
}

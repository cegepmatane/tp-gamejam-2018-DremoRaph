using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public int count, delay, interval;       // Delay on delivery.
    
    private Transform m_SpawnPoint;
    [SerializeField]
    private GameObject m_Enemy;
    private GameObject m_player;

    private List<Tile> spawnTiles;

    void Start()
    {
        spawnTiles = FindObjectOfType<MapGrid>().GetSpawnTiles();
        StartCoroutine(SpawnEnemy(count, interval, delay));
        m_player = GameObject.FindGameObjectWithTag("Player");
    }


    public IEnumerator SpawnEnemy(int a_count, int a_interval, int a_delay)
    {
        // Wait for the delivery delay.
        yield return new WaitForSeconds(a_delay);

        for (; a_count > 0; a_count--)
        {
            bool foundTile = false;
            int spawnPos = 0;
            while (!foundTile)
            {
                spawnPos = Random.RandomRange(0, spawnTiles.Count - 1);
                if ((m_player.transform.position - spawnTiles[spawnPos].transform.position).magnitude > 20) continue;
                if ((m_player.transform.position - spawnTiles[spawnPos].transform.position).magnitude < 2) continue;
                foundTile = true;
            }
            
            Instantiate(m_Enemy, spawnTiles[spawnPos].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(a_interval);
        }
    }
}

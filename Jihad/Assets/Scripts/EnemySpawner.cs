using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

   public int count, delay, interval;       // Delay on delivery.

    [SerializeField]
    private Transform m_SpawnPoint;
    [SerializeField]
    private GameObject m_Enemy;

    private List<Tile> spawnTiles;

    void Start()
    {
        //spawnTiles = FindObjectOfType<MapGrid>().GetSpawnTiles();
        StartCoroutine(SpawnEnemy(count, interval, delay));
    }


    public IEnumerator SpawnEnemy(int a_count, int a_interval, int a_delay)
    {
        // Wait for the delivery delay.
        yield return new WaitForSeconds(a_delay);

        for (; a_count > 0; a_count--)
        {
            int spawnPos = Random.RandomRange(0, spawnTiles.Count - 1);
            Instantiate(m_Enemy, spawnTiles[spawnPos].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(a_interval);
        }
    }
}

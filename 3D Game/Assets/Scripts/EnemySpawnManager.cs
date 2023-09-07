using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    private void Start()
    {
        //foreach (GameObject enemyPrefab in enemyPrefabs)
        //{
        //    Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        //}
        StartCoroutine(Spawn());
    }

    private Vector3 RandomSpawnPositionAroundPlayer(float distanceFromPlayer)
    {
        float randomX = 0;
        float randomZ = 0;

        while (randomX == 0 && randomZ ==0)
        {
            randomX = Random.Range(-1f, 1f);
            randomZ = Random.Range(-1f, 1f);
        }

        Vector3 randomDirection = new Vector3(randomX, 0, randomZ).normalized;
        
        return PlayerControl.instance.transform.position + randomDirection * distanceFromPlayer;
    }

    public IEnumerator Spawn()
    {
        for (int i = 0; i < 100; i++)
        {
            Vector3 spawnPosition = RandomSpawnPositionAroundPlayer(15);
            Instantiate(enemyPrefabs[0], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }
}

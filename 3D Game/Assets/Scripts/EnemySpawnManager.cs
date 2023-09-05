using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    private void Start()
    {
        foreach (GameObject enemyPrefab in enemyPrefabs)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}

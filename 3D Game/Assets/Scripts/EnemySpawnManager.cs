using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    private void Start()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}

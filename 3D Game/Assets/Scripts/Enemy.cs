using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public GameObject lootPrefab;

    public void SpawnLoot()
    {
        Instantiate(lootPrefab, transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        SpawnLoot();
    }
}

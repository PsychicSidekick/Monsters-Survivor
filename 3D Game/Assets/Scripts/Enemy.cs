using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public GameObject lootPrefab;

    public List<Item> lootPool = new List<Item>();

    public void SpawnLoot()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(0, 1.5f), 0, Random.Range(0, 1.5f));
            Loot loot = Instantiate(lootPrefab, transform.position + randomOffset, Quaternion.identity).GetComponent<Loot>();
            loot.item = lootPool[Random.Range(0, lootPool.Count)];
        }
    }

    public override void OnDeath()
    {
        SpawnLoot();
    }
}

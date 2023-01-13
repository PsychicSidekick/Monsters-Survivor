using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public List<ItemPrefab> lootPool = new List<ItemPrefab>();

    public void SpawnLoot()
    {
        for (int i = 0; i < 2; i++)
        {
            // Choose random item from loot pool
            ItemPrefab itemPrefab = lootPool[Random.Range(0, lootPool.Count)];
            Item item = new Item(itemPrefab);
            // Spread loot position
            Vector3 randomOffset = new Vector3(Random.Range(0, 1.5f), 0, Random.Range(0, 1.5f));
            Loot loot = Instantiate(itemPrefab.lootPrefab, transform.position + randomOffset, Quaternion.identity).GetComponent<Loot>();
            loot.item = item;
            item.lootObj = loot.gameObject;
        }
    }

    public override void OnDeath()
    {
        SpawnLoot();
    }
}

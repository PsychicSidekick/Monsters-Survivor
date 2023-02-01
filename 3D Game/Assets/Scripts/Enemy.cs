using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public List<ItemPrefab> lootPool = new List<ItemPrefab>();

    public override void Update()
    {
        base.Update();

        float distanceFromPlayer = Vector3.Distance(PlayerControl.instance.transform.position, transform.position);

        if (distanceFromPlayer < 5)
        {
            Move(PlayerControl.instance.transform.position);
            Vector3 lookDir = Vector3.RotateTowards(transform.forward, PlayerControl.instance.transform.position - transform.position, 3 * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
        else
        {
            StopMoving();
        }
    }

    public void SpawnLoot()
    {
        for (int i = 0; i < 6; i++)
        {
            // Choose random item from loot pool
            ItemPrefab itemPrefab = lootPool[Random.Range(0, lootPool.Count)];
            ItemObj itemObj = Instantiate(itemPrefab.itemObjPrefab, transform.position, Quaternion.identity).GetComponent<ItemObj>();
            itemObj.itemPrefab = itemPrefab;
            // Spread loot position
            Vector3 randomOffset = new Vector3(Random.Range(0, 1.5f), 0, Random.Range(0, 1.5f));
            itemObj.transform.position += randomOffset;
        }
    }

    public override void OnDeath()
    {
        SpawnLoot();
    }
}

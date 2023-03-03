using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float detectionRange;
    public float attackRange;
    public List<ItemPrefab> lootPool = new List<ItemPrefab>();

    public int xpYield;

    public override void Update()
    {
        base.Update();

        float distanceFromPlayer = Vector3.Distance(PlayerControl.instance.transform.position, transform.position);

        if (distanceFromPlayer < detectionRange)
        {
            if (distanceFromPlayer < attackRange)
            {
                animator.SetBool("isAttacking", true);
            }
            else
            {
                Move(PlayerControl.instance.transform.position);
                animator.SetBool("isAttacking", false);
            }
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
        for (int i = 0; i < 10; i++)
        {
            // Choose random item from loot pool
            ItemPrefab itemPrefab = lootPool[Random.Range(0, lootPool.Count)];
            ItemObj itemObj = Instantiate(itemPrefab.itemObjPrefab, transform.position, Quaternion.identity).GetComponent<ItemObj>();
            itemObj.itemPrefab = itemPrefab;
            itemObj.item = new Item(itemPrefab);

            // Spread loot position
            Vector3 randomOffset = new Vector3(Random.Range(0, 1.5f), 0, Random.Range(0, 1.5f));
            itemObj.transform.position += randomOffset;
        }
    }

    public override void OnDeath()
    {
        PlayerControl.instance.ReceiveXp(xpYield);
        SpawnLoot();
    }
}

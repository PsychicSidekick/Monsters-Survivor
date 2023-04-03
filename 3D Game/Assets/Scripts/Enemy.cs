using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public GameObject healthBarCanvas;
    public GameObject healthBarPrefab;
    private GameObject healthBar;

    public float detectionRange;
    public float attackRange;
    public List<ItemPrefab> lootPool = new List<ItemPrefab>();

    public int xpYield;

    protected override void Start()
    {
        base.Start();
        healthBar = Instantiate(healthBarPrefab, healthBarCanvas.transform);
    }

    protected override void Update()
    {
        base.Update();

        healthBar.GetComponent<RectTransform>().anchoredPosition = GameManager.instance.WorldToCanvasPos(healthBarCanvas, transform.position);
        healthBar.GetComponent<Slider>().value = life/stats.maxLife.value;

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
        for (int i = 0; i < 1; i++)
        {
            // Choose random item from loot pool
            ItemPrefab itemPrefab = lootPool[Random.Range(0, lootPool.Count)];
            ItemObj itemObj = Instantiate(itemPrefab.itemObjPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity).GetComponent<ItemObj>();
            itemObj.itemPrefab = itemPrefab;
            itemObj.item = new Item(itemPrefab);

            // Spread loot position
            Vector3 randomOffset = new Vector3(Random.Range(0, 1.5f), 0, Random.Range(0, 1.5f));
            itemObj.transform.position += randomOffset;
        }
    }

    public override void OnDeath()
    {
        Destroy(healthBar);
        PlayerControl.instance.ReceiveXp(xpYield);
        SpawnLoot();
    }
}

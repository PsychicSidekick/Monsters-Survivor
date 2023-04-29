using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public GameObject healthBarCanvas;
    public GameObject healthBarPrefab;
    private GameObject healthBar;
    
    public List<ItemPrefab> lootPool = new List<ItemPrefab>();

    public int xpYield;

    public PlayerControl player;

    protected override void Start()
    {
        base.Start();
        player = PlayerControl.instance;
        healthBar = Instantiate(healthBarPrefab, healthBarCanvas.transform);
    }

    protected override void Update()
    {
        base.Update();

        healthBar.GetComponent<RectTransform>().anchoredPosition = GameManager.instance.WorldToCanvasPos(healthBarCanvas, transform.position);
        healthBar.GetComponent<Slider>().value = life/stats.maxLife.value;
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
        player.ReceiveXp(xpYield);
        SpawnLoot();
        base.OnDeath();
    }

    public override void FindGroundTarget()
    {
        if (player == null)
        {
            return;
        }
        GetComponent<SkillHandler>().groundTarget = GameManager.instance.RefinedPos(player.transform.position);
    }

    public override Character FindCharacterTarget()
    {
        return player;
    }
}

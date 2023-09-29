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

    public float detectionRange;
    public float attackRange;

    protected override void Start()
    {
        base.Start();
        healthBarCanvas = GameObject.Find("EnemyHealthCanvas");
        player = PlayerControl.instance;
        healthBar = Instantiate(healthBarPrefab, healthBarCanvas.transform);
        healthBar.GetComponent<RectTransform>().anchoredPosition = GameManager.instance.WorldToCanvasPos(healthBarCanvas, transform.position);

    }

    protected override void Update()
    {
        base.Update();
        healthBar.GetComponent<RectTransform>().anchoredPosition = GameManager.instance.WorldToCanvasPos(healthBarCanvas, transform.position);
        healthBar.GetComponent<Slider>().value = life/stats.maximumLife.value;
    }

    public void SpawnLoot()
    {
        for (int i = 0; i < 0; i++)
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

    public void FacePlayer()
    {
        FacePosition(player.transform.position);
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
        return PlayerControl.instance;
    }
}
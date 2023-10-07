using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public GameObject healthBarCanvas;
    public GameObject healthBarPrefab;
    private GameObject healthBar;
    
    public List<ItemBase> lootPool = new List<ItemBase>();

    public int xpYield;

    public Player player;

    public float detectionRange;
    public float attackRange;

    protected override void Start()
    {
        base.Start();
        healthBarCanvas = GameObject.Find("EnemyHealthCanvas");
        player = Player.instance;
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
        for (int i = 0; i < 1; i++)
        {
            // Choose random item from loot pool
            ItemBase itemBase = lootPool[Random.Range(0, lootPool.Count)];
            LootGameObject lootGameObject = Instantiate(itemBase.lootGameObjectPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity).GetComponent<LootGameObject>();
            lootGameObject.item = new Item(itemBase);
            lootGameObject.item.lootGameObject = lootGameObject;

            // Spread loot position
            Vector3 randomOffset = new Vector3(Random.Range(0, 1.5f), 0, Random.Range(0, 1.5f));
            lootGameObject.transform.position += randomOffset;
        }
    }

    public override void OnDeath()
    {
        Destroy(healthBar);
        player.ReceiveXp(xpYield);
        SpawnLoot();
        Destroy(gameObject);
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
        return Player.instance;
    }
}

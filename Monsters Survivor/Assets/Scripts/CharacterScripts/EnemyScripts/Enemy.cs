using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public GameObject healthBarCanvas;
    public GameObject healthBarPrefab;
    private GameObject healthBar;

    public int lootRank;
    private List<ItemBase> lootPool = new List<ItemBase>();

    public int xpYield;
    public float chanceToDropLoot;
    public AudioClip lootDropSFX;

    public Player player;

    public float detectionRange;
    public float attackRange;

    protected override void Start()
    {
        base.Start();
        healthBarCanvas = GameObject.Find("EnemyHealthCanvas");
        player = Player.instance;
        if (lootRank == 1 && GameManager.instance.GetCurrentGameTime() > 180)
        {
            lootRank = 2;
            chanceToDropLoot = 1;
        }
        lootPool = Resources.LoadAll<ItemBase>("ItemBases/Rank" + lootRank).ToList();
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
        if (Random.Range(1f, 101f) <= chanceToDropLoot)
        {
            player.audioSource.PlayOneShot(lootDropSFX);
            // Choose random item from loot pool
            ItemBase itemBase = lootPool[Random.Range(0, lootPool.Count)];
            LootGameObject lootGameObject = Instantiate(itemBase.lootGameObjectPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity).GetComponent<LootGameObject>();
            lootGameObject.item = new Item(itemBase);
            lootGameObject.item.lootGameObject = lootGameObject;
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

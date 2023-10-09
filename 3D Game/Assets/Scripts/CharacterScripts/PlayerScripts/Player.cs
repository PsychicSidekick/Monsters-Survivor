using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;


public class Player : Character
{
    public static Player instance;

    [HideInInspector] private Vector3 moveTarget = new Vector3(0, 0, 0);

    [HideInInspector] public bool isAttacking;

    public LootGameObject targetLoot;

    public LayerMask moveRayLayer;

    public int availableSkillPoints;
    public TMP_Text availableSkillPointsText;
    public GameObject deathScreen;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
        PlayerStorage.instance.player = this;
    }

    protected override void Start()
    {
        base.Start();
        foreach (ItemSlot itemSlot in Resources.FindObjectsOfTypeAll(typeof(ItemSlot)))
        {
            if (itemSlot.equippedItem != null)
            {
                itemSlot.EquipItem(itemSlot.equippedItem);
            }
        }
        
    }

    protected override void Update()
    {
        base.Update();

        if (GameManager.IsMouseOverUI())
        {
            return;
        }

        if (PlayerStorage.instance.lockCursor)
        {
            return;
        }

        if (((!isAttacking && Input.GetMouseButton(0)) || (isAttacking && Input.GetMouseButtonDown(0))) && !GetComponent<SkillHandler>().isChannelling)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, moveRayLayer))
            {
                GetComponent<SkillHandler>().currentSkillHolder = null;
                FindMoveTarget(hit);
                PlayerStorage.instance.pickingUpLoot = false;
                Move(moveTarget);
            }
        }
    }

    public override void OnDeath()
    {
        GameManager.instance.PauseGame();
        deathScreen.SetActive(true);
        base.OnDeath();
    }

    public override void OnLevelUp()
    {
        base.OnLevelUp();
        AddToAvailableSkillPoints(1);
    }

    public void AddToAvailableSkillPoints(int value)
    {
        availableSkillPoints += value;
        availableSkillPointsText.text = availableSkillPoints.ToString();
    }

    public void FindMoveTarget(RaycastHit hit)
    {
        GameObject hitObj = hit.transform.gameObject;

        if (hitObj.tag == "Ground")
        {
            moveTarget = hit.point;
        }
    }

    public override void FindGroundTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, targettable))
        {
            GameObject hitObj = hit.transform.gameObject;

            if (hitObj.tag == "Ground")
            {
                GetComponent<SkillHandler>().groundTarget = GameManager.instance.RefinedPos(hit.point);
            }
            else if (hitObj.tag == "Enemy")
            {
                GetComponent<SkillHandler>().groundTarget = GameManager.instance.RefinedPos(hitObj.transform.position);
            }
        }
    }

    public override Character FindCharacterTarget()
    {
        Character characterTarget = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, targettable))
        {
            GameObject hitObj = hit.transform.gameObject;

            if (hitObj.tag == "Enemy")
            {
                characterTarget = hitObj.GetComponent<Character>();
            }
        }

        GetComponent<SkillHandler>().characterTarget = characterTarget;
        return characterTarget;
    }
}

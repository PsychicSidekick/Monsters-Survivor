using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerControl : Character
{
    public static PlayerControl instance;

    private Vector3 moveTarget = new Vector3(0, 0, 0);

    public bool isAttacking;

    public GameObject targetItem;

    public LayerMask moveRayLayer;

    private void Awake()
    {
        instance = this;
    }

    protected override void Update()
    {
        base.Update();


        if (IsMouseOverUI())
        {
            return;
        }

        if (Inventory.instance.lockCursor)
        {
            return;
        }

        if (((!isAttacking && Input.GetMouseButton(0)) || (isAttacking && Input.GetMouseButtonDown(0))) && !GetComponent<SkillHandler>().isChannelling)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, moveRayLayer))
            {
                GetComponent<SkillHandler>().currentSkill = null;
                FindMoveTarget(hit);
                Inventory.instance.pickingUpLoot = false;
                Move(moveTarget);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AttackHB")
        {
            GetComponent<StatusEffectManager>().ApplyStatusEffect(new FreezeBuff(1, 50));
        }
    }

    public void FindMoveTarget(RaycastHit hit)
    {
        GameObject hitObj = hit.transform.gameObject;

        if (hitObj.tag == "Ground")
        {
            moveTarget = hit.point;
        }
    }

    public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}

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

    public override void Update()
    {
        base.Update();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, moveRayLayer))
        {
            FindMoveTarget(hit);

            if (!isAttacking && Input.GetMouseButton(0) && !Inventory.instance.lockCursor && !IsMouseOverUI())
            {
                Inventory.instance.pickingUpLoot = false;
                Move(moveTarget);
            } 

            if (isAttacking && Input.GetMouseButtonDown(0) && !Inventory.instance.lockCursor && !IsMouseOverUI())
            {
                Inventory.instance.pickingUpLoot = false;
                Move(moveTarget);
            }
        }
    }

    public void FindMoveTarget(RaycastHit hit)
    {
        GameObject hitObj = hit.transform.gameObject;

        if (hitObj.tag == "Ground")
        {
            moveTarget = RefinedPos(hit.point);
        }
    }

    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}

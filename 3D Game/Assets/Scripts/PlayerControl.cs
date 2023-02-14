using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerControl : Character
{
    public static PlayerControl instance;

    public Vector3 skillTarget = new Vector3(0, 1, 0);
    private Vector3 moveTarget = new Vector3(0, 0, 0);

    public bool isAttacking;

    public GameObject targetItem;

    private void Awake()
    {
        instance = this;
    }

    public override void Update()
    {
        base.Update();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            FindSkillTarget(hit);
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

    public void FindSkillTarget(RaycastHit hit)
    {
        GameObject hitObj = hit.transform.gameObject;

        if (hitObj.tag == "Ground")
        {
            skillTarget = RefinedPos(hit.point);
        }

        if (hitObj.tag == "Enemy")
        {
            skillTarget = RefinedPos(hitObj.transform.position);
        }
    }

    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;


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

        if (GameManager.IsMouseOverUI())
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

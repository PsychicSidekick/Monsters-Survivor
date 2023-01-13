using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerControl : Character
{
    public static PlayerControl instance;

    private Vector3 skillTarget = new Vector3(0, 1, 0);
    private Vector3 moveTarget = new Vector3(0, 1, 0);

    public float moveSpeed;
    public float projSpeed;

    public Skill skill;

    public float attackSpeed = 1;
    private float lastAttack = 0;

    public bool cursorHoldsItem = false;
    public GameObject targetLoot;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            FindSkillTarget(hit);
            FindMoveTarget(hit);

            if (Input.GetMouseButton(0) && !Inventory.instance.lockCursor)
            {
                Inventory.instance.pickingUpLoot = false;
                Move(moveTarget);
            } 
        }
        
        GetSkillKey("q");
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

    private void GetSkillKey(string key)
    {
        if (Input.GetKey(key))
        {
            StopMoving();

            if (Time.time - 1 / attackSpeed > lastAttack)
            {
                UseSkill();
                lastAttack = Time.time;
            }
        }
    }

    private void UseSkill()
    {
        skill.UseSkill(transform.position, skillTarget, projSpeed);
    }

    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}

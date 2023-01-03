using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerControl : Character
{
    public static PlayerControl instance;

    private Vector3 skillTarget = new Vector3(0, 1, 0);

    public float moveSpeed;
    public float projSpeed;

    public Skill skill;

    public float attackSpeed = 1;
    private float lastAttack = 0;

    public bool pickingUpLoot = false;
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
            GameObject hitObj = hit.transform.gameObject;

            if (hitObj.tag == "Ground")
            {
                if (Input.GetMouseButton(0) && !IsMouseOverUI())
                {
                    pickingUpLoot = false;
                    Move(hit.point);
                }

                skillTarget = RefinedPos(hit.point);
            }
            else if (hit.transform.gameObject.tag == "Enemy")
            {
                skillTarget = RefinedPos(hitObj.transform.position);
            }
        }

        if(pickingUpLoot)
        {
            if (Vector3.Distance(transform.position, RefinedPos(targetLoot.transform.position)) < 0.1f)
            {
                targetLoot.GetComponent<Loot>().OnPickUp();
                Inventory.instance.AddItem(targetLoot.GetComponent<Loot>().item);
                pickingUpLoot = false;
            }
        }
        
        GetSkillKey("q");
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

    public void StartPickUpLoot(GameObject loot)
    {
        pickingUpLoot = true;
        targetLoot = loot;
        Move(RefinedPos(loot.transform.position));
    }

    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}

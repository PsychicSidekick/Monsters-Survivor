using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControl : Character
{
    private Vector3 skillTarget = new Vector3(0, 1, 0);

    public float moveSpeed;
    public float projSpeed;

    public Skill skill;

    public float attackSpeed = 1;
    private float lastAttack = 0;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.gameObject.tag == "Ground")
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<NavMeshAgent>().destination = new Vector3(hit.point.x, 1, hit.point.z);
                }

                skillTarget = new Vector3(hit.point.x, 1, hit.point.z);
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

    private void StopMoving()
    {
        GetComponent<NavMeshAgent>().destination = transform.position;
    }
}

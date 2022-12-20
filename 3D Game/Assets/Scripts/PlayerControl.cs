using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Vector3 moveTarget = new Vector3(0, 1, 0);
    private Vector3 skillTarget = new Vector3(0, 1, 0);

    public float moveSpeed;
    public float projSpeed;

    public Skill skill;

    public float attackSpeed = 1;
    private float lastAttack = 0;

    private bool stopMoving = false;

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
                    moveTarget = new Vector3(hit.point.x, 1, hit.point.z);
                }

                skillTarget = new Vector3(hit.point.x, 1, hit.point.z);
            }
        }
        
        GetSkillKey("q");

        if(!stopMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveSpeed / 100);
        }
    }

    private void GetSkillKey(string key)
    {
        if (Input.GetKey(key))
        {
            stopMoving = true;
            moveTarget = transform.position;

            if (Time.time - 1 / attackSpeed > lastAttack)
            {
                UseSkill();
                lastAttack = Time.time;
            }
        }
        else
        {
            stopMoving = false;
        }
    }

    private void UseSkill()
    {
        skill.UseSkill(transform.position, skillTarget, projSpeed);
    }
}

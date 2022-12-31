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
            GameObject hitObj = hit.transform.gameObject;

            if (hitObj.tag == "Ground")
            {
                if (Input.GetMouseButton(0))
                {
                    Move(hit.point);
                }

                skillTarget = RefinedPos(hit.point);
            }
            else if (hit.transform.gameObject.tag == "Enemy")
            {
                skillTarget = RefinedPos(hitObj.transform.position);
            }
        }
        
        GetSkillKey("q");
    }

    public Vector3 RefinedPos(Vector3 position)
    {
        return new Vector3(position.x, 1, position.z);
    }

    public void Move(Vector3 targetPosition)
    {
        GetComponent<NavMeshAgent>().destination = RefinedPos(targetPosition);
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

    public void PickUpItem(GameObject gameObject)
    {

    }

    private void StopMoving()
    {
        GetComponent<NavMeshAgent>().destination = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Loot")
        {
            PickUpItem(other.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillHandler : MonoBehaviour
{
    KeyCode skillKey1 = KeyCode.Q;
    KeyCode skillKey2 = KeyCode.W;
    KeyCode skillKey3 = KeyCode.E;
    KeyCode skillKey4 = KeyCode.R;

    public LayerMask layerMask;

    public Vector3 skillTarget = new Vector3(0, 1, 0);

    public List<KeyCode> keys;
    public List<string> skills = new List<string>();

    public Dictionary<KeyCode, string> keyBinds = new Dictionary<KeyCode, string>();

    public float attackSpeed = 1;
    private float lastAttack = 0;

    private void Start()
    {
        keys = new List<KeyCode> { skillKey1, skillKey2, skillKey3, skillKey4 };

        keyBinds = keys.Select((k, index) => new { k, v = skills[index] })
                       .ToDictionary(x => x.k, x => x.v);
    }

    private void Update()
    {
        foreach (KeyCode key in keys)
        {
            GetSkillKey(key);
        }
    }

    public void FindSkillTarget(RaycastHit hit)
    {
        GameObject hitObj = hit.transform.gameObject;

        if (hitObj.tag == "Ground")
        {
            skillTarget = PlayerControl.instance.RefinedPos(hit.point);
        }

        if (hitObj.tag == "Enemy")
        {
            skillTarget = PlayerControl.instance.RefinedPos(hitObj.transform.position);
        }
    }

    private void GetSkillKey(KeyCode key)
    {
        if (Input.GetKey(key))
        {
            if (Time.time - 1 / attackSpeed > lastAttack)
            {
                UseSkill(keyBinds[key]);
                lastAttack = Time.time;
            }
        }
    }

    private void UseSkill(string skill)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            FindSkillTarget(hit);
        }
        PlayerControl.instance.animator.Play(skill, -1, 0f);
    }

    public void FaceSkillTarget()
    {
        PlayerControl.instance.StopMoving();
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, skillTarget - transform.position, 10, 0.0f);
        transform.rotation = Quaternion.LookRotation(lookDir);
    }
}

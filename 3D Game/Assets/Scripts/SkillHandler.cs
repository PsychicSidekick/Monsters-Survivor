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
        PlayerControl.instance.animator.Play("ArcaneBlast", -1, 0f);
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, PlayerControl.instance.skillTarget - transform.position, 10, 0.0f);
        transform.rotation = Quaternion.LookRotation(lookDir);
        GetComponent<Skills>().Invoke(skill, 0);
    }
}

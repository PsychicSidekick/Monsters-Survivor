using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class SkillHolder
{
    public Skill skill;
    [HideInInspector]public float cooldownTime;
    [HideInInspector]public float activeTime;
    [HideInInspector]public SkillState state;
    public KeyCode key;
    public bool triggerSkill;
}

public enum SkillState
{
    ready,
    outOfRange,
    active,
    cooldown
}

public class SkillHandler : MonoBehaviour
{
    public List<SkillHolder> skills = new List<SkillHolder>();

    public Skill currentSkill;
    public Vector3 groundTarget;
    public Character characterTarget;
    public LayerMask mask;

    public bool isChannelling;

    public float lastSkillUse;

    private void Update()
    {
        bool readyToUseSkill = Time.time - 1 / GetComponent<StatsManager>().attackSpeed.value > lastSkillUse;

        foreach (SkillHolder skillHolder in skills)
        {
            switch (skillHolder.state)
            {
                case SkillState.ready:
                    if (skillHolder.triggerSkill && readyToUseSkill && GetComponent<Animator>().GetFloat("ActionSpeed") != 0 && !isChannelling)
                    {
                        if (skillHolder.skill.targetsCharacters)
                        {
                            if (FindCharacterTarget() == null)
                            {
                                break;
                            }
                        }
                        else
                        {
                            FindGroundTarget();
                        }
                        currentSkill = skillHolder.skill;
                        if (Vector3.Distance(groundTarget, transform.position) > skillHolder.skill.useRange)
                        {
                            skillHolder.state = SkillState.outOfRange;
                            GetComponent<Character>().Move(groundTarget);
                            break;
                        }
                        lastSkillUse = Time.time;

                        skillHolder.skill.OnUse(GetComponent<Character>());
                        skillHolder.state = SkillState.active;
                        skillHolder.activeTime = skillHolder.skill.activeTime;
                    }
                    break;
                case SkillState.outOfRange:
                    if (currentSkill == skillHolder.skill)
                    {
                        if (Vector3.Distance(groundTarget, transform.position) > skillHolder.skill.useRange)
                        {
                            break;
                        }
                        lastSkillUse = Time.time;

                        skillHolder.skill.OnUse(GetComponent<Character>());
                        skillHolder.state = SkillState.active;
                        skillHolder.activeTime = skillHolder.skill.activeTime;
                    }
                    else
                    {
                        skillHolder.state = SkillState.ready;
                    }
                    break;
                case SkillState.active:
                    if (skillHolder.skill.isChannellingSkill)
                    {
                        if (skillHolder.triggerSkill)
                        {
                            isChannelling = true;
                            skillHolder.skill.WhileChannelling();
                            break;
                        }
                        else
                        {
                            isChannelling = false;
                        }
                    }
                    else
                    {
                        if (skillHolder.activeTime > 0)
                        {
                            skillHolder.activeTime -= Time.deltaTime;
                            break;
                        }
                    }

                    skillHolder.skill.OnCoolDown();
                    skillHolder.state = SkillState.cooldown;
                    skillHolder.cooldownTime = skillHolder.skill.coolDownTime;
                    break;
                case SkillState.cooldown:
                    if (skillHolder.cooldownTime > 0)
                    {
                        skillHolder.cooldownTime -= Time.deltaTime;
                    }
                    else
                    {
                        skillHolder.state = SkillState.ready;
                    }
                    break;
            }
        }
    }

    public void FindGroundTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, mask))
        {
            GameObject hitObj = hit.transform.gameObject;

            if (hitObj.tag == "Ground" || hitObj.tag == "Enemy")
            {
                groundTarget = GameManager.instance.RefinedPos(hit.point);
            }
            //else if (hitObj.tag == "Enemy")
            //{
            //    groundTarget = GameManager.instance.RefinedPos(hitObj.transform.position);
            //}
        }
    }

    public Character FindCharacterTarget()
    {
        characterTarget = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, mask))
        {
            GameObject hitObj = hit.transform.gameObject;

            if (hitObj.tag == "Enemy")
            {
                characterTarget = hitObj.GetComponent<Character>();
            }
        }

        return characterTarget;
    }

    public void FaceGroundTarget()
    {
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, new Vector3(groundTarget.x, transform.position.y, groundTarget.z) - transform.position, 10, 0.0f);
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

    public void FaceCharacterTarget()
    {
        if (characterTarget == null)
        {
            return;
        }

        Vector3 lookDir = Vector3.RotateTowards(transform.forward, characterTarget.transform.position - transform.position, 10, 0.0f);
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

    public void UseCurrentSkill()
    {
        if (currentSkill != null)
        {
            currentSkill.UseSkill();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class SkillHolder
{
    public Skill skill;
    public KeyCode key;
    [HideInInspector] public float cooldownTime;
    [HideInInspector] public float activeTime;
    [HideInInspector] public SkillState state;
    [HideInInspector] public bool triggerSkill;
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

    [HideInInspector] public Skill currentSkill;
    [HideInInspector] public Vector3 groundTarget;
    [HideInInspector] public Character characterTarget;
    public LayerMask mask;

    [HideInInspector] public bool isChannelling;
    [HideInInspector] public float lastSkillUse;

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
                            if (GetComponent<Character>().FindCharacterTarget() == null)
                            {
                                break;
                            }
                        }
                        else
                        {
                            GetComponent<Character>().FindGroundTarget();
                        }
                        currentSkill = skillHolder.skill;
                        lastSkillUse = Time.time;

                        skillHolder.skill.OnUse(GetComponent<Character>());
                        skillHolder.state = SkillState.active;
                        skillHolder.activeTime = skillHolder.skill.activeTime;
                    }
                    break;
                case SkillState.active:
                    if (skillHolder.skill.isChannellingSkill)
                    {
                        if (skillHolder.triggerSkill)
                        {
                            isChannelling = true;
                            if (skillHolder.skill.WhileChannelling(GetComponent<Character>()))
                            {
                                break;
                            }
                            isChannelling = false;
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
                            skillHolder.skill.WhileActive(GetComponent<Character>());
                            skillHolder.activeTime -= Time.deltaTime;
                            break;
                        }
                    }

                    skillHolder.skill.OnCoolDown(GetComponent<Character>());
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
            currentSkill.UseSkill(GetComponent<Character>());
        }
    }
}

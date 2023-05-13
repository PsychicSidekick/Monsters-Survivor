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
    public Character skillUser;
    public List<SkillHolder> skills = new List<SkillHolder>();

    [HideInInspector] public Skill currentSkill;
    [HideInInspector] public Vector3 groundTarget;
    [HideInInspector] public Character characterTarget;
    public LayerMask mask;

    [HideInInspector] public bool isChannelling;
    [HideInInspector] public GameObject currentChannelingGameObject;
    [HideInInspector] public float lastSkillUse;

    private void Start()
    {
        skillUser = GetComponent<Character>();
    }

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
                            if (skillUser.FindCharacterTarget() == null)
                            {
                                break;
                            }
                        }
                        else
                        {
                            skillUser.FindGroundTarget();
                        }
                        currentSkill = skillHolder.skill;
                        lastSkillUse = Time.time;

                        skillHolder.skill.OnUse(skillUser);
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
                            if (skillHolder.skill.WhileChannelling(skillUser))
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
                            skillHolder.skill.WhileActive(skillUser);
                            skillHolder.activeTime -= Time.deltaTime;
                            break;
                        }
                    }

                    skillHolder.skill.OnCoolDown(skillUser);
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
            if (currentSkill.TryUseSkill(skillUser, currentSkill.GetManaCost(skillUser)))
            {
                currentSkill.UseSkill(skillUser);
            }
        }
    }
}

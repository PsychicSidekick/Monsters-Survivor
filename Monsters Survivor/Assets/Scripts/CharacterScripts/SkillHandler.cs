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
    [HideInInspector] public float channelledTime;
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

    [HideInInspector] public SkillHolder currentSkillHolder;
    // Bonus increased attack speed from the current skill
    [HideInInspector] public StatModifier currentSkillAttackSpeedMod;
    [HideInInspector] public Vector3 groundTarget;
    [HideInInspector] public Character characterTarget;

    [HideInInspector] public bool isChannelling;
    [HideInInspector] public GameObject currentChannelingGameObject;
    [HideInInspector] public float lastSkillUseTime;

    private void Start()
    {
        skillUser = GetComponent<Character>();
        currentSkillAttackSpeedMod = new StatModifier(StatModifierType.inc_AttackSpeed, 0);
        skillUser.stats.ApplyStatModifier(currentSkillAttackSpeedMod);
    }

    private void Update()
    {
        // Global cooldown
        bool readyToUseSkill = Time.time - 1 / skillUser.stats.attackSpeed.value > lastSkillUseTime;
        
        foreach (SkillHolder skillHolder in skills)
        {
            switch (skillHolder.state)
            {
                case SkillState.ready:
                    if (skillHolder.triggerSkill && readyToUseSkill && GetComponent<Animator>().GetFloat("ActionSpeed") != 0 && !isChannelling)
                    {
                        if (!skillHolder.skill)
                        {
                            return;
                        }
                        if (skillHolder.skill.targetsCharacters)
                        {
                            // Ignore skill trigger if no target was found
                            if (skillUser.FindCharacterTarget() == null)
                            {
                                break;
                            }
                        }
                        else
                        {
                            skillUser.FindGroundTarget();
                        }
                        currentSkillHolder = skillHolder;
                        lastSkillUseTime = Time.time;

                        skillHolder.skill.OnUse(skillUser);
                        return;
                    }
                    break;
                case SkillState.active:
                    if (skillHolder.skill.isChannellingSkill)
                    {
                        skillHolder.channelledTime += Time.deltaTime;
                        if (skillHolder.triggerSkill)
                        {
                            isChannelling = true;
                            if (skillHolder.skill.WhileChannelling(skillUser, this, skillHolder.channelledTime))
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

                    skillHolder.channelledTime = 0;
                    skillHolder.cooldownTime = skillHolder.skill.OnCoolDown(skillUser);
                    skillHolder.state = SkillState.cooldown;
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
        skillUser.FacePosition(new Vector3(groundTarget.x, transform.position.y, groundTarget.z));
    }

    public void FaceCharacterTarget()
    {
        if (characterTarget == null)
        {
            return;
        }

        skillUser.FacePosition(characterTarget.transform.position);
    }

    // Called in animation
    public void UseCurrentSkill()
    {
        if (currentSkillHolder != null)
        {
            // Use skill only when skill user has enough mana
            if (currentSkillHolder.skill.TryUseSkill(skillUser, currentSkillHolder.skill.GetManaCost(skillUser)))
            {
                currentSkillHolder.skill.UseSkill(skillUser);
                currentSkillHolder.state = SkillState.active;
                currentSkillHolder.activeTime = currentSkillHolder.skill.activeTime;
            }
        }
    }

    // Apply bonus increased attack speed from the current skill
    public void SetCurrentAttackSpeedMod(float value)
    {
        skillUser.stats.RemoveStatModifier(currentSkillAttackSpeedMod);
        currentSkillAttackSpeedMod = new StatModifier(StatModifierType.inc_AttackSpeed, value);
        skillUser.stats.ApplyStatModifier(currentSkillAttackSpeedMod);
    }
}

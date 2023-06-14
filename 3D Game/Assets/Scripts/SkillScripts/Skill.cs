using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : ScriptableObject
{
    public string skillName;
    public Sprite skillIcon;

    public float coolDownTime;
    public float activeTime;
    public float baseManaCost;
    public bool targetsCharacters;
    public bool isChannellingSkill;

    public virtual float GetManaCost(Character skillUser)
    {
        return baseManaCost;
    }

    public virtual void OnUse(Character skillUser)
    {

    }

    public virtual void WhileActive(Character skillUser)
    {

    }

    public virtual bool WhileChannelling(Character skillUser, SkillHandler skillHandler, float channelledTime)
    {
        return false;
    }

    public virtual float OnCoolDown(Character skillUser)
    {
        return coolDownTime;
    }

    public bool TryUseSkill(Character skillUser, float manaCost)
    {
        if (!skillUser.CheckSkillCost(manaCost))
        {
            return false;
        }

        skillUser.ReduceMana(manaCost);
        return true;
    }

    public virtual void UseSkill(Character skillUser)
    {
        skillUser.GetComponent<SkillHandler>().currentSkill = null;
    }
}

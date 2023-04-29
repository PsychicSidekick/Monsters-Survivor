using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public string skillName;
    public float coolDownTime;
    public float activeTime;
    public float baseManaCost;
    public bool targetsCharacters;
    public bool isChannellingSkill;

    public virtual void OnUse(Character skillUser)
    {

    }

    public virtual void WhileActive(Character skillUser)
    {

    }

    public virtual bool WhileChannelling(Character skillUser)
    {
        return false;
    }

    public virtual void OnCoolDown(Character skillUser)
    {

    }

    public bool TryUseSkill(Character skillUser)
    {
        if (!skillUser.CheckSkillCost(baseManaCost))
        {
            return false;
        }

        skillUser.ReduceMana(baseManaCost);
        return true;
    }

    public virtual void UseSkill(Character skillUser)
    {
        skillUser.GetComponent<SkillHandler>().currentSkill = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : ScriptableObject
{
    public string skillName;
    public float coolDownTime;
    public float activeTime;
    public float useRange;
    public bool targetsCharacters;
    public bool isChannellingSkill;
    protected Character skillUser;

    public virtual void OnUse(Character _skillUser)
    {
        skillUser = _skillUser;
    }

    public virtual void WhileChannelling()
    {

    }

    public virtual void OnCoolDown()
    {

    }

    public virtual void UseSkill()
    {
        skillUser.GetComponent<SkillHandler>().currentSkill = null;
    }
}

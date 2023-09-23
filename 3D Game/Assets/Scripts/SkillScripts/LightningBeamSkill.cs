using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightningBeamSkill : Skill
{
    public GameObject beamPrefab;

    public float baseRange;
    public float baseWidth;
    public float baseDamagePerSecond;
    public float baseDamageRampPerSecond;

    public float baseTimeUntilOvercharged;
    public float baseOCDamageIncrease;
    public float baseOCWidthIncrease;
    public float baseOCRangeIncrease;
    public float baseOCManaIncrease;

    public override void OnUse(Character skillUser)
    {
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        skillHandler.SetCurrentAttackSpeedMod(1000);
        skillUser.animator.SetBool("isChannelling", true);
        skillUser.animator.Play("Channel");
        skillHandler.FaceGroundTarget();
        skillUser.StopMoving();

        LightningBeamSkillTree skillTree = skillUser.GetComponent<LightningBeamSkillTree>();
        float width = baseWidth * (1 + skillTree.increasedWidth);
        float range = baseRange * (1 + skillTree.increasedRange);
        float damagePerSecond = baseDamagePerSecond * (1 + skillTree.increasedDamagePerSecond);

        GameObject beamObject = Instantiate(beamPrefab, skillUser.transform);
        skillHandler.currentChannelingGameObject = beamObject;
        beamObject.transform.localScale = new Vector3(0.1f, 0.1f, range);
        beamObject.transform.localPosition = new Vector3(0, 1, range / 2f + 0.5f);

        EffectCollider beamArea = beamObject.GetComponent<EffectCollider>();
        beamArea.SetHostileEffects(damagePerSecond, DamageType.Lightning, true, skillUser, null);
    }

    public override bool WhileChannelling(Character skillUser, SkillHandler skillHandler, float channelledTime)
    {
        LightningBeamSkillTree skillTree = skillUser.GetComponent<LightningBeamSkillTree>();
        bool overcharged = false;
        if ((channelledTime >= baseTimeUntilOvercharged + skillTree.additionalTimeUntilOvercharged && !skillTree.doesNotOvercharge) || (skillTree.startsOvercharged && !skillTree.doesNotOvercharge))
        {
            overcharged = true;
        }

        float manaCost = (baseManaCost + skillTree.additionalManaCostPerSecond) * Time.deltaTime;
        if (overcharged)
        {
            manaCost *= 1 + skillTree.ocIncreasedManaCostPerSecond;
        }

        if (!skillUser.CheckSkillCost(manaCost))
        {
            return false;
        }

        skillUser.ReduceMana(manaCost);
        
        float damageRamp = baseDamageRampPerSecond + skillTree.increasedDamageRampPerSecond;

        if (!overcharged)
        {
            GameObject beamObject = skillHandler.currentChannelingGameObject;

            EffectCollider beamArea = beamObject.GetComponent<EffectCollider>();
            float damagePerSecond = baseDamagePerSecond * (1 + skillTree.increasedDamagePerSecond + damageRamp * channelledTime);
            beamArea.SetHostileEffects(damagePerSecond, DamageType.Lightning, true, skillUser, null);
        } 
        else if ((channelledTime - baseTimeUntilOvercharged - skillTree.additionalTimeUntilOvercharged) < 0.1f)
        {
            GameObject beamObject = skillHandler.currentChannelingGameObject;
            float width = baseWidth * (1 + skillTree.increasedWidth + baseOCWidthIncrease + skillTree.ocIncreasedWidth);
            float range = baseRange * (1 + skillTree.increasedRange + baseOCRangeIncrease + skillTree.ocIncreasedRange);
            beamObject.transform.localScale = new Vector3(width, width, range);
            beamObject.transform.localPosition = new Vector3(0, 1, range / 2f + 0.5f);

            EffectCollider beamArea = beamObject.GetComponent<EffectCollider>();
            float timeUntilOvercharged = baseTimeUntilOvercharged + skillTree.additionalTimeUntilOvercharged;
            float ocDamageIncrease = baseOCDamageIncrease + skillTree.ocIncreasedDamagePerSecond;
            float damagePerSecond = baseDamagePerSecond * (1 + skillTree.increasedDamagePerSecond + damageRamp * timeUntilOvercharged + ocDamageIncrease);
            beamArea.SetHostileEffects(damagePerSecond, DamageType.Lightning, true, skillUser, null);
        }

        skillUser.FindGroundTarget();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.StopMoving();

        return true;
    }

    public override float OnCoolDown(Character skillUser)
    {
        skillUser.animator.SetBool("isChannelling", false);
        Destroy(skillUser.GetComponent<SkillHandler>().currentChannelingGameObject);
        return coolDownTime;
    }
}

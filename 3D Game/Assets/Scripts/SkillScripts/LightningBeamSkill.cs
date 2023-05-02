using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightningBeamSkill : Skill
{
    public GameObject beamPrefab;
    public float beamRange;
    public float beamBaseDamagePerSecond;

    public override void OnUse(Character skillUser)
    {
        skillUser.animator.SetBool("isChannelling", true);
        skillUser.animator.Play("Channel");
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.StopMoving();

        GameObject beamObject = Instantiate(beamPrefab, skillUser.transform);
        skillUser.GetComponent<SkillHandler>().currentChannelingGameObject = beamObject;
        beamObject.transform.localScale = new Vector3(0.1f, 0.1f, beamRange);
        beamObject.transform.localPosition = new Vector3(0, 1, beamRange / 2f + 0.5f);

        EffectCollider beamArea = beamObject.GetComponent<EffectCollider>();
        beamArea.SetEffects(beamBaseDamagePerSecond, DamageType.Lightning, true, skillUser, null);
    }

    public override bool WhileChannelling(Character skillUser)
    {
        if (!skillUser.CheckSkillCost(baseManaCost * Time.deltaTime))
        {
            return false;
        }

        skillUser.ReduceMana(baseManaCost * Time.deltaTime);

        skillUser.FindGroundTarget();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.StopMoving();
        return true;
    }

    public override void OnCoolDown(Character skillUser)
    {
        skillUser.animator.SetBool("isChannelling", false);
        Destroy(skillUser.GetComponent<SkillHandler>().currentChannelingGameObject);
    }
}

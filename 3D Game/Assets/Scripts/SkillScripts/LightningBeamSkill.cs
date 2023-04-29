using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightningBeamSkill : Skill
{
    public GameObject beamPrefab;
    public GameObject beamObject;
    public float beamRange;
    public float manaCostPerSecond;
    public float damageTickRate;
    public float beamBaseDamagePerSecond;

    public override void OnUse(Character skillUser)
    {
        skillUser.animator.SetBool("isChannelling", true);
        skillUser.animator.Play("Channel");
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.StopMoving();

        beamObject = Instantiate(beamPrefab, skillUser.transform);

        beamObject.transform.localScale = new Vector3(0.1f, 0.1f, beamRange);
        beamObject.transform.localPosition = new Vector3(0, 1, beamRange / 2f + 0.5f);

        AreaDamageOverTime beamArea = beamObject.GetComponent<AreaDamageOverTime>();
        beamArea.damagePerSecond = beamBaseDamagePerSecond;
        beamArea.damageType = DamageType.Lightning;
        beamArea.owner = skillUser;
    }

    public override bool WhileChannelling(Character skillUser)
    {
        if (!skillUser.CheckSkillCost(manaCostPerSecond * Time.deltaTime))
        {
            return false;
        }

        skillUser.ReduceMana(manaCostPerSecond * Time.deltaTime);
        skillUser.FindGroundTarget();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.StopMoving();
        return true;
    }

    public override void OnCoolDown(Character skillUser)
    {
        skillUser.animator.SetBool("isChannelling", false);
        Destroy(beamObject);
    }
}

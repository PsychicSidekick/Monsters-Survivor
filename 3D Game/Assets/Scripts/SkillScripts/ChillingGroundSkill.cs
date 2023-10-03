using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChillingGroundSkill : Skill
{
    public GameObject chillingAreaPrefab;

    public float baseChillingGroundDamagePerSecond;
    public float baseChillingGroundHealingPerSecond;
    public float baseChillingGroundRadius;
    public float baseChillingGroundDuration;

    public float baseSlowEffect;

    public override void OnUse(Character skillUser)
    {
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        skillHandler.SetCurrentAttackSpeedMod(0);
        skillUser.StopMoving();
        skillHandler.FaceGroundTarget();
        skillUser.animator.Play("AreaCast");
    }

    public override void UseSkill(Character skillUser)
    {
        ChillingGroundSkillTree skillTree = skillUser.GetComponent<ChillingGroundSkillTree>();

        float chillingGroundDamagePerSecond = (baseChillingGroundDamagePerSecond * (1 + skillTree.increasedBaseChillingGroundDamage) + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedChillingGroundDamage + skillUser.stats.increasedColdDamage.value + skillUser.stats.increasedAreaDamage.value);
        float increasedChillingGroundHealingPerSecond = 1 + skillTree.increasedChillingGroundHealing;

        if (skillTree.damageModifiersAffectHealing)
        {
            increasedChillingGroundHealingPerSecond += skillTree.increasedChillingGroundDamage + skillUser.stats.increasedColdDamage.value + skillUser.stats.increasedAreaDamage.value;
        }

        float chillingGroundHealingPerSecond = baseChillingGroundHealingPerSecond * increasedChillingGroundHealingPerSecond;
        float chillingGroundRadius = baseChillingGroundRadius * (1 + skillTree.increasedChillingGroundRadius + skillUser.stats.increasedAreaEffect.value);
        float chillingGroundDuration = baseChillingGroundDuration * (1 + skillTree.increasedChillingGroundDuration);

        float slowEffect = baseSlowEffect + skillTree.increasedSlowEffect + skillUser.stats.increasedSlowEffect.value;
        SlowEffect slow = new SlowEffect(slowEffect, 1000, 100);
        StatusEffect[] inAreaStatusEffects = { slow };

        EffectCollider chillingArea = Instantiate(chillingAreaPrefab, skillUser.transform.position, Quaternion.identity).GetComponent<EffectCollider>();
        if (skillTree.doesNotSlow)
        {
            chillingArea.SetHostileEffects(chillingGroundDamagePerSecond, DamageType.Cold, true, skillUser, null);
        }
        else
        {
            chillingArea.SetHostileEffects(chillingGroundDamagePerSecond, DamageType.Cold, true, skillUser, inAreaStatusEffects);
        }

        Debug.Log(chillingGroundHealingPerSecond);
        chillingArea.SetFriendlyEffects(chillingGroundHealingPerSecond, true, skillUser, null);

        skillUser.StartCoroutine(DestroyChillingArea(chillingArea.gameObject, chillingGroundDuration));
        chillingArea.transform.localScale = new Vector3(chillingGroundRadius, 1, chillingGroundRadius);
    }

    private IEnumerator DestroyChillingArea(GameObject chillingArea, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(chillingArea);
    }
}

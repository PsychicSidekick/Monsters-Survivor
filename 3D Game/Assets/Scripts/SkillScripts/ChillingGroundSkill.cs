using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChillingGroundSkill : Skill
{
    public GameObject chillingAreaPrefab;
    public float baseRadius;
    public float baseDamagePerSecond;
    public float baseDuration;

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

        float damagePerSecond = (baseDamagePerSecond + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedDamage + skillUser.stats.increasedColdDamage.value + skillUser.stats.increasedAreaDamage.value);
        float radius = baseRadius * (1 + skillTree.increasedRadius + skillUser.stats.increasedAreaEffect.value);
        float duration = baseDuration * (1 + skillTree.increasedDuration);

        float slowEffect = baseSlowEffect + skillTree.increasedSlowEffect + skillUser.stats.increasedSlowEffect.value;
        SlowEffect slow = new SlowEffect(slowEffect, 1000, 100);
        StatusEffect[] inAreaStatusEffects = { slow };

        EffectCollider chillingArea = Instantiate(chillingAreaPrefab, skillUser.transform.position, Quaternion.identity).GetComponent<EffectCollider>();
        chillingArea.SetHostileEffects(damagePerSecond, DamageType.Cold, true, skillUser, inAreaStatusEffects);
        skillUser.StartCoroutine(DisableChillingArea(chillingArea.gameObject, duration));
        chillingArea.transform.localScale = new Vector3(radius, 1, radius);
    }

    private IEnumerator DisableChillingArea(GameObject chillingArea, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(chillingArea);
    }
}

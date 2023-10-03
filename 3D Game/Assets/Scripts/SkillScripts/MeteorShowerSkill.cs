using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeteorShowerSkill : Skill
{
    public GameObject meteorShowerColliderPrefab;
    public GameObject meteorShowerParticlesPrefab;

    public float baseMeteorShowerDamagePerSecond;
    public float baseMeteorShowerRadius;
    public float baseMeteorShowerDuration;

    public float baseIgniteChance;
    public float baseIgniteDuration;

    public override void OnUse(Character skillUser)
    {
        MeteorShowerSkillTree skillTree = skillUser.GetComponent<MeteorShowerSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();

        skillHandler.SetCurrentAttackSpeedMod(skillTree.increasedAttackSpeed);
        skillUser.StopMoving();
        skillHandler.FaceGroundTarget();
        skillUser.animator.Play("AreaCast");
    }

    public override void UseSkill(Character skillUser)
    {
        MeteorShowerSkillTree skillTree = skillUser.GetComponent<MeteorShowerSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();

        float meteorShowerDamagePerSecond = (baseMeteorShowerDamagePerSecond * (1 + skillTree.increasedBaseMeteorShowerDamage) + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedMeteorShowerDamage + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedAreaDamage.value);
        float meteorShowerRadius = baseMeteorShowerRadius * (1 + skillTree.increasedBaseMeteorShowerRadius) * (1 + skillTree.increasedMeteorShowerRadius + skillUser.stats.increasedAreaEffect.value);
        float meteorShowerDuration = baseMeteorShowerDuration * (1 + skillTree.increasedMeteorShowerDuration);

        float igniteDamage = meteorShowerDamagePerSecond * 0.5f * (1 + skillTree.increasedIgniteDamage + skillTree.increasedIgniteDuration + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedIgniteDamage.value + skillUser.stats.increasedIgniteDuration.value);
        float igniteChance = baseIgniteChance + skillTree.increasedIgniteChance + skillUser.stats.additionalIgniteChance.value;
        float igniteDuration = baseIgniteDuration * (1 + skillTree.increasedIgniteDuration + skillUser.stats.increasedIgniteDuration.value);

        IgniteEffect ignite = new IgniteEffect(skillUser, igniteDamage, igniteDuration, igniteChance);

        EffectCollider meteorShowerArea = Instantiate(meteorShowerColliderPrefab, skillHandler.groundTarget, Quaternion.identity).GetComponent<EffectCollider>();
        meteorShowerArea.SetHostileEffects(meteorShowerDamagePerSecond, DamageType.Fire, true, skillUser, null, ignite);
        meteorShowerArea.transform.localScale = new Vector3(meteorShowerRadius, meteorShowerRadius, meteorShowerRadius);

        GameObject meteorShowerParticles = Instantiate(meteorShowerParticlesPrefab, skillHandler.groundTarget - new Vector3(0, 1, 0), Quaternion.identity);
        meteorShowerParticles.transform.localScale = new Vector3(meteorShowerRadius * 0.11f, 1, meteorShowerRadius * 0.11f);

        skillUser.StartCoroutine(DestroyMeteorShowerArea(meteorShowerArea.gameObject, meteorShowerParticles, meteorShowerDuration));
    }

    IEnumerator DestroyMeteorShowerArea(GameObject meteorShowerArea, GameObject meteorShowerParticles, float meteorShowerDuration)
    {
        yield return new WaitForSeconds(meteorShowerDuration);
        Destroy(meteorShowerArea);
        Destroy(meteorShowerParticles);
    }

    public override float OnCoolDown(Character skillUser)
    {
        return coolDownTime + skillUser.GetComponent<MeteorShowerSkillTree>().additionalCooldownTime;
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<MeteorShowerSkillTree>().additionalManaCost;
    }
}

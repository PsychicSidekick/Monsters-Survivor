using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeteorShowerSkill : Skill
{
    public GameObject meteorShowerAreaPrefab;
    public float baseRadius;
    public float baseDamagePerSecond;
    public float baseDuration;

    public float baseIgniteChance;
    public float baseIgniteDuration;

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
        MeteorShowerSkillTree skillTree = skillUser.GetComponent<MeteorShowerSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();

        float damagePerSecond = (baseDamagePerSecond + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedDamage + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedAreaDamage.value);
        float radius = baseRadius * (1 + skillTree.increasedRadius + skillUser.stats.increasedAreaEffect.value);
        float duration = baseDuration * (1 + skillTree.increasedDuration);

        float igniteDamage = damagePerSecond * 0.5f * (1 + skillTree.increasedIgniteDamage + skillTree.increasedIgniteDuration + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedIgniteDamage.value + skillUser.stats.increasedIgniteDuration.value);
        float igniteChance = baseIgniteChance + skillTree.increasedIgniteChance + skillUser.stats.additionalIgniteChance.value;
        float igniteDuration = baseIgniteDuration * (1 + skillTree.increasedIgniteDuration + skillUser.stats.increasedIgniteDuration.value);
        IgniteEffect ignite = new IgniteEffect(skillUser, igniteDamage, igniteDuration, igniteChance);

        EffectCollider meteorShowerArea = Instantiate(meteorShowerAreaPrefab, skillHandler.groundTarget, Quaternion.identity).GetComponent<EffectCollider>();
        meteorShowerArea.SetHostileEffects(damagePerSecond, DamageType.Fire, true, skillUser, null, ignite);
        skillUser.StartCoroutine(DisableMeteorShowerArea(meteorShowerArea.gameObject, duration));
        meteorShowerArea.transform.localScale = new Vector3(radius, 1, radius);
        var meteorShowerAreaShape = meteorShowerArea.transform.GetChild(0).GetComponent<ParticleSystem>().shape;
        meteorShowerAreaShape.radius = radius * 4;
    }

    private IEnumerator DisableMeteorShowerArea(GameObject meteorShowerArea, float lifeTime)
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(lifeTime);
        Debug.Log(Time.time);
        Destroy(meteorShowerArea);
    }
}

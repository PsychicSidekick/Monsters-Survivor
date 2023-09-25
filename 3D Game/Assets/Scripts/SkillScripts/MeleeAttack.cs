using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeAttack : Skill
{
    public float baseRange;
    public float baseDamage;

    public float baseIgniteDuration;
    public float baseIgniteChance;

    public float baseSlowEffect;
    public float baseSlowDuration;
    public float baseSlowChance;

    public float baseShockEffect;
    public float baseShockDuration;
    public float baseShockChance;

    public override void OnUse(Character skillUser)
    {
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        skillHandler.SetCurrentAttackSpeedMod(0);
        skillUser.StopMoving();
        skillUser.animator.Play("MeleeAttack");
    }

    public override void UseSkill(Character skillUser)
    {
        Character targetCharacter = skillUser.GetComponent<SkillHandler>().characterTarget;
        MeleeSkillTree skillTree = skillUser.GetComponent<MeleeSkillTree>();

        skillTree.meleeVFX.GetComponent<ParticleSystem>().Play();

        if (Vector3.Distance(skillUser.transform.position, targetCharacter.transform.position) <= baseRange + skillTree.increasedRange + 1)
        {
            Instantiate(skillTree.onHitVFX, GameManager.instance.RefinedPos(targetCharacter.transform.position), Quaternion.identity);

            float damage = (baseDamage + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedDamage);
            targetCharacter.ReceiveDamage(new Damage(baseDamage + skillUser.stats.attackDamage.value, skillUser, skillTree.damageType));

            StatusEffect statusEffect = new StatusEffect();
            switch (skillTree.damageType)
            {
                case DamageType.Fire:
                    float igniteDamage = damage * 0.5f * (1 + skillTree.increasedIgniteDamage + skillTree.increasedIgniteDuration);
                    float igniteDuration = baseIgniteDuration * (1 + skillTree.increasedIgniteDuration);
                    float igniteChance = baseIgniteChance + skillTree.increasedIgniteChance;
                    statusEffect = new IgniteEffect(skillUser, igniteDamage, igniteDuration, igniteChance);
                    break;
                case DamageType.Cold:
                    float slowEffect = baseSlowEffect + skillTree.increasedSlowEffect;
                    float slowDuration = baseSlowDuration * (1 + skillTree.increasedSlowDuration);
                    float slowChance = baseSlowChance + skillTree.increasedSlowChance;
                    statusEffect = new SlowEffect(slowEffect, slowDuration, slowChance);
                    break;
                case DamageType.Lightning:
                    float shockEffect = baseShockEffect + skillTree.increasedShockEffect;
                    float shockDuration = baseShockDuration * (1 + skillTree.increasedShockDuration);
                    float shockChance = baseShockChance + skillTree.increasedShockChance;
                    statusEffect = new ShockEffect(shockEffect, shockDuration, shockChance);
                    break;
            }
            targetCharacter.GetComponent<StatusEffectManager>().ApplyStatusEffect(statusEffect);
        }
    }
}

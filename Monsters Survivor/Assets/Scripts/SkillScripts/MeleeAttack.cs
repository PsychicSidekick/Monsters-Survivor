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

        if (Vector3.Distance(skillUser.transform.position, targetCharacter.transform.position) <= baseRange + skillTree.increasedRange + 1 && targetCharacter.isActiveAndEnabled)
        {
            skillUser.audioSource.PlayOneShot(skillSFX);

            Instantiate(skillTree.onHitVFX, GameManager.instance.RefinedPos(targetCharacter.transform.position), Quaternion.identity);

            float damage = baseDamage + skillUser.stats.attackDamage.value;

            StatusEffect statusEffect = new StatusEffect();
            switch (skillTree.damageType)
            {
                case DamageType.Fire:
                    damage *= 1 + skillTree.increasedDamage + skillUser.stats.increasedFireDamage.value;

                    float igniteDamage = damage * 0.5f * (1 + skillTree.increasedIgniteDamage + skillTree.increasedIgniteDuration + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedIgniteDamage.value + skillUser.stats.increasedIgniteDuration.value);
                    float igniteDuration = baseIgniteDuration * (1 + skillTree.increasedIgniteDuration + skillUser.stats.increasedIgniteDuration.value);
                    float igniteChance = baseIgniteChance + skillTree.increasedIgniteChance + skillUser.stats.additionalIgniteChance.value;
                    statusEffect = new IgniteEffect(skillUser, igniteDamage, igniteDuration, igniteChance);
                    break;
                case DamageType.Cold:
                    damage *= 1 + skillTree.increasedDamage + skillUser.stats.increasedColdDamage.value;

                    float slowEffect = baseSlowEffect + skillTree.increasedSlowEffect + skillUser.stats.increasedSlowEffect.value;
                    float slowDuration = baseSlowDuration * (1 + skillTree.increasedSlowDuration + skillUser.stats.increasedSlowDuration.value);
                    float slowChance = baseSlowChance + skillTree.increasedSlowChance + skillUser.stats.additionalSlowChance.value;
                    statusEffect = new SlowEffect(slowEffect, slowDuration, slowChance);
                    break;
                case DamageType.Lightning:
                    damage *= 1 + skillTree.increasedDamage + skillUser.stats.increasedLightningDamage.value;

                    float shockEffect = baseShockEffect + skillTree.increasedShockEffect + skillUser.stats.increasedShockEffect.value;
                    float shockDuration = baseShockDuration * (1 + skillTree.increasedShockDuration + skillUser.stats.increasedShockDuration.value);
                    float shockChance = baseShockChance + skillTree.increasedShockChance + skillUser.stats.additionalShockChance.value;
                    statusEffect = new ShockEffect(shockEffect, shockDuration, shockChance);
                    break;
            }

            targetCharacter.ReceiveDamage(new Damage(damage, skillUser, skillTree.damageType));
            targetCharacter.GetComponent<StatusEffectManager>().ApplyStatusEffect(statusEffect);
        }
    }
}

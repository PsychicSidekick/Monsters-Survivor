using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IncinerateSkill : Skill
{
    public GameObject explosionPrefab;
    public float baseExplosionRadius;
    public float baseExplosionDamage;

    public float baseIgniteDamageMultiplier;

    public float baseNewIgniteDamageMultiplier;
    public float baseNewIgniteDuration;

    public override void OnUse(Character skillUser)
    {
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        skillHandler.SetCurrentAttackSpeedMod(0);
        skillUser.StopMoving();
        skillHandler.FaceCharacterTarget();
        skillUser.animator.Play("AreaCast");
    }

    public override void UseSkill(Character skillUser)
    {
        IncinerateSkillTree skillTree = skillUser.GetComponent<IncinerateSkillTree>();

        Character targetCharacter = skillUser.GetComponent<SkillHandler>().characterTarget;
        EffectCollider explosion = Instantiate(explosionPrefab, targetCharacter.transform.position, Quaternion.identity).GetComponent<EffectCollider>();
        float explosionDamage = baseExplosionDamage * (1 + skillTree.increasedDamage);
        explosion.SetEffects(explosionDamage, DamageType.Fire, false, skillUser, null);
        float explosionRadius = baseExplosionRadius * (1 + skillTree.increasedRadius);
        explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
        skillUser.StartCoroutine(DestroyExplosion(skillUser, explosion.gameObject, skillTree.spreadsExplosions));
    }

    private IEnumerator DestroyExplosion(Character skillUser, GameObject explosion, bool spreadsExplosions)
    {
        yield return new WaitForSeconds(0.05f);

        IncinerateSkillTree skillTree = skillUser.GetComponent<IncinerateSkillTree>();

        List<Character> hitTargets = explosion.GetComponent<EffectCollider>().charactersInArea;
        foreach (Character character in hitTargets)
        {
            StatusEffectManager statusEffectManager = character.GetComponent<StatusEffectManager>();
            IgniteEffect ignite = (IgniteEffect)statusEffectManager.FindStatusEffectWithName("ignite");
            if (ignite != null)
            {
                if (!skillTree.doesNotDealExtraIgniteDamage)
                {
                    float extraIgniteDamage = ignite.remainingDamage * baseIgniteDamageMultiplier * (1 + skillTree.increasedExtraIgniteMulti);
                    character.ReceiveDamage(new Damage(extraIgniteDamage, skillUser, DamageType.Fire));
                }
                else if (skillTree.receivesHealingFromRemovedIgnites)
                {
                    float extraIgniteDamage = ignite.remainingDamage * baseIgniteDamageMultiplier * (1 + skillTree.increasedExtraIgniteMulti);
                    skillUser.ReceiveHealing(extraIgniteDamage);
                }
                if (!skillTree.doesNotRemoveIgnites)
                {
                    statusEffectManager.RemoveStatusEffect(statusEffectManager.FindStatusEffectWithName("ignite"));
                }
            }
            
            if (skillTree.appliesNewIgnite)
            {
                float newIgniteDamage = baseExplosionDamage * (1 + baseNewIgniteDamageMultiplier + skillTree.increasedDamage + skillTree.increasedIgniteDamage);
                IgniteEffect newIgnite = new IgniteEffect(skillUser, newIgniteDamage, baseNewIgniteDuration, 100);
                statusEffectManager.ApplyStatusEffect(newIgnite);
            }
        }

        if (spreadsExplosions)
        {
            Character targetCharacter = skillUser.GetComponent<SkillHandler>().characterTarget;
            List<Character> spreadTargets = explosion.GetComponent<EffectCollider>().charactersInArea;
            spreadTargets.Remove(targetCharacter);

            foreach (Character character in spreadTargets)
            {
                EffectCollider newExplosion = Instantiate(explosionPrefab, character.transform.position, Quaternion.identity).GetComponent<EffectCollider>();
                float explosionDamage = baseExplosionDamage * (1 + skillTree.increasedDamage);
                newExplosion.SetEffects(explosionDamage, DamageType.Fire, false, skillUser, null);
                float explosionRadius = baseExplosionRadius * (1 + skillTree.increasedRadius);
                newExplosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
                skillUser.StartCoroutine(DestroyExplosion(skillUser, newExplosion.gameObject, false));
            }
        }
        
        Destroy(explosion);
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<IncinerateSkillTree>().additionalManaCost;
    }
}

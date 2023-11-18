using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightningStrikeSkill : Skill
{
    public GameObject lightningStrikeAreaPrefab;

    public int baseNumberOfLightningStrikes;
    public float baseLightningStrikeDamage;
    public float baseLightningStrikeRadius;
    public float baseMinimumLightningStrikeRange;
    public float baseMaximumLightningStrikeRange;

    public float baseShockEffect;
    public float baseShockChance;
    public float baseShockDuration;

    public override void OnUse(Character skillUser)
    {
        LightningStrikeSkillTree skillTree = skillUser.GetComponent<LightningStrikeSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        if (skillTree.maximumNumberOfLightningStrikesIsOne)
        {
            skillHandler.SetCurrentAttackSpeedMod(skillTree.increasedAttackSpeed + (skillTree.additionalNumberOfLightningStrikes + (int)skillUser.stats.additionalNumberOfProjectiles.value) * 10);
        }
        else
        {
            skillHandler.SetCurrentAttackSpeedMod(skillTree.increasedAttackSpeed);
        }

        if (!skillTree.doesNotStopMoving)
        {
            skillUser.StopMoving();
            skillUser.animator.Play("AreaCast");
        }
        else
        {
            if (TryUseSkill(skillUser, GetManaCost(skillUser)))
            {
                UseSkill(skillUser);
                skillHandler.currentSkillHolder.state = SkillState.active;
                skillHandler.currentSkillHolder.activeTime = activeTime;
            }
        }
    }

    public override void UseSkill(Character skillUser)
    {
        skillUser.audioSource.PlayOneShot(skillSFX);

        LightningStrikeSkillTree skillTree = skillUser.GetComponent<LightningStrikeSkillTree>();

        int numberOfLightningStrikes = baseNumberOfLightningStrikes + skillTree.additionalNumberOfLightningStrikes + (int)skillUser.stats.additionalNumberOfProjectiles.value;

        float increasedBaseLightningStrikeDamage = 1 + skillTree.increasedBaseLightningStrikeDamage;
        if (skillTree.maximumNumberOfLightningStrikesIsOne)
        {
            increasedBaseLightningStrikeDamage += (numberOfLightningStrikes - baseNumberOfLightningStrikes) * 0.2f;
            numberOfLightningStrikes = 1;
        }
        float lightningStrikeDamage = (baseLightningStrikeDamage * increasedBaseLightningStrikeDamage + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedLightningStrikeDamage + skillUser.stats.increasedLightningDamage.value + skillUser.stats.increasedAreaDamage.value);
        float lightningStrikeRadius = baseLightningStrikeRadius * (1 + skillTree.increasedBaseLightningStrikeRadius) * (1 + skillTree.increasedLightningStrikeRadius + skillUser.stats.increasedAreaEffect.value);
        float minimumLightningStrikeRange = baseMinimumLightningStrikeRange * (1 + skillTree.increasedLighningStrikeRange);
        float maximumLightningStrikeRange = baseMaximumLightningStrikeRange * (1 + skillTree.increasedLighningStrikeRange);

        float shockEffect = baseShockEffect + skillTree.increasedShockEffect + skillUser.stats.increasedShockEffect.value;
        float shockChance = baseShockChance + skillTree.increasedShockChance + skillUser.stats.additionalShockChance.value;
        float shockDuration = baseShockDuration * (1 + skillTree.increasedShockDuration + skillUser.stats.increasedShockDuration.value);

        for (int i = 0; i < numberOfLightningStrikes; i++)
        {
            float randomRange = Random.Range(minimumLightningStrikeRange, maximumLightningStrikeRange);
            Vector2 randomVector2 = Random.insideUnitCircle;
            Vector3 randomDirection = new Vector3(randomVector2.x, 0, randomVector2.y).normalized;
            Vector3 spawnPosition = skillUser.transform.position + randomDirection * randomRange;

            EffectCollider collider = Instantiate(lightningStrikeAreaPrefab, spawnPosition, Quaternion.identity).GetComponent<EffectCollider>();
            ShockEffect shock = new ShockEffect(shockEffect, shockDuration, shockChance);

            collider.SetHostileEffects(lightningStrikeDamage, DamageType.Lightning, false, skillUser, null, shock);
            collider.transform.localScale = new Vector3(lightningStrikeRadius, 1, lightningStrikeRadius);
            collider.transform.GetChild(0).GetComponent<DigitalRuby.LightningBolt.LightningBoltScript>().StartPosition = GameManager.instance.RefinedPos(skillUser.transform.position);

            skillUser.StartCoroutine(DestroyLightningStrike(collider.gameObject));
        }
    }

    IEnumerator DestroyLightningStrike(GameObject lightningStrike)
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(lightningStrike);
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<LightningStrikeSkillTree>().additionalManaCost;
    }
}

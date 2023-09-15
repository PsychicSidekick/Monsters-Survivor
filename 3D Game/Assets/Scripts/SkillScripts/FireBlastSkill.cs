using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FireBlastSkill : Skill
{
    public GameObject blastColliderPrefab;

    public float baseExpansionTime;
    public float baseBlastRadius;
    public float baseBlastDamage;
    public float baseIgniteChance;
    public float baseIgniteDuration;

    public override void OnUse(Character skillUser)
    {
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        FireBlastSkillTree skillTree = skillUser.GetComponent<FireBlastSkillTree>();
        skillHandler.SetCurrentAttackSpeedMod(0);
        if (!skillTree.doesNotStopToUseSkill)
        {
            skillUser.StopMoving();
        }
        skillUser.FindGroundTarget();
        skillHandler.FaceGroundTarget();
        skillUser.animator.Play("AreaCast");
    }

    public override void UseSkill(Character skillUser)
    {
        skillUser.StartCoroutine(ExpandSphereCollider(skillUser));
    }

    public override float OnCoolDown(Character skillUser)
    {
        FireBlastSkillTree skillTree = skillUser.GetComponent<FireBlastSkillTree>();
        return coolDownTime + skillTree.additionalCooldownTime;
    }

    IEnumerator ExpandSphereCollider(Character skillUser)
    {
        FireBlastSkillTree skillTree = skillUser.GetComponent<FireBlastSkillTree>();

        float blastDamage = (baseBlastDamage + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedDamage + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedAreaDamage.value);
        float igniteDamage = blastDamage * 0.5f * (1 + skillTree.increasedIgniteDuration + skillTree.increasedIgniteDamage + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedIgniteDamage.value + skillUser.stats.increasedIgniteDuration.value);
        float igniteDuration = baseIgniteDuration * (1 + skillTree.increasedIgniteDuration + skillUser.stats.increasedIgniteDuration.value);
        float igniteChance = baseIgniteChance + skillTree.increasedIgniteChance + skillUser.stats.additionalIgniteChance.value;
        float blastRadius = baseBlastRadius * (1 + skillTree.increasedRadius + skillUser.stats.increasedAreaEffect.value);
        float expansionTime = baseExpansionTime + skillTree.increasedExpansionTime;
        IgniteEffect ignite = new IgniteEffect(skillUser, igniteDamage, igniteDuration, igniteChance);

        if (skillTree.doesNotDealDirectDamage)
        {
            blastDamage = 0;
        }

        EffectCollider blastCollider = Instantiate(blastColliderPrefab, GameManager.instance.RefinedPos(skillUser.transform.position), Quaternion.identity).GetComponent<EffectCollider>();
        blastCollider.SetHostileEffects(blastDamage, DamageType.Fire, false, skillUser, null, ignite);
        if (!skillTree.doesNotDestroyProjectiles)
        {
            blastCollider.gameObject.AddComponent<DestroyProjectiles>();
        }

        for (float i = 0; i <= expansionTime + 0.1; i += Time.deltaTime)
        {
            float size = Mathf.Lerp(0.5f, blastRadius, i / expansionTime);
            blastCollider.transform.localScale = new Vector3(size, size, size);
            if (size >= blastRadius)
            {
                Destroy(blastCollider.gameObject);
                break;
            }
            yield return null;
        }
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<FireBlastSkillTree>().additionalManaCost;
    }
}

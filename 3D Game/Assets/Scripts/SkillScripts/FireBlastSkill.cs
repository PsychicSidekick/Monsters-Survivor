using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FireBlastSkill : Skill
{
    public GameObject fireBlastPrefab;

    public float baseFireBlastDamage;
    public float baseFireBlastRadius;
    public float baseFireBlastExpansionTime;

    public float baseIgniteChance;
    public float baseIgniteDuration;

    public override void OnUse(Character skillUser)
    {
        FireBlastSkillTree skillTree = skillUser.GetComponent<FireBlastSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();

        skillHandler.SetCurrentAttackSpeedMod(skillTree.increasedAttackSpeed);

        if (!skillTree.doesNotStopToUseSkill)
        {
            skillUser.FindGroundTarget();
            skillHandler.FaceGroundTarget();
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
        FireBlastSkillTree skillTree = skillUser.GetComponent<FireBlastSkillTree>();

        float fireBlastDamage = (baseFireBlastDamage + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedFireBlastDamage + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedAreaDamage.value);
        float fireBlastRadius = baseFireBlastRadius * (1 + skillTree.increasedFireBlastRadius + skillUser.stats.increasedAreaEffect.value);
        float expansionTime = baseFireBlastExpansionTime * (1 + skillTree.increasedFireBlastExpansionTime);

        float igniteDamage = fireBlastDamage * 0.5f * (1 + skillTree.increasedIgniteDuration + skillTree.increasedIgniteDamage + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedIgniteDamage.value + skillUser.stats.increasedIgniteDuration.value);
        float igniteChance = baseIgniteChance + skillTree.increasedIgniteChance + skillUser.stats.additionalIgniteChance.value;
        float igniteDuration = baseIgniteDuration * (1 + skillTree.increasedIgniteDuration + skillUser.stats.increasedIgniteDuration.value);        

        if (skillTree.doesNotDealDamageOnHit)
        {
            fireBlastDamage = 0;
        }

        IgniteEffect ignite = new IgniteEffect(skillUser, igniteDamage, igniteDuration, igniteChance);

        EffectCollider collider = Instantiate(fireBlastPrefab, GameManager.instance.RefinedPos(skillUser.transform.position), Quaternion.identity).GetComponent<EffectCollider>();
        collider.SetHostileEffects(fireBlastDamage, DamageType.Fire, false, skillUser, null, ignite);

        if (skillTree.destroysProjectiles)
        {
            collider.gameObject.AddComponent<DestroyProjectiles>();
        }

        skillUser.StartCoroutine(ExpandFireBlastCollider(collider, expansionTime, fireBlastRadius));
    }

    IEnumerator ExpandFireBlastCollider(EffectCollider collider, float expansionTime, float blastRadius)
    {
        for (float i = 0; i <= expansionTime + 0.1; i += Time.deltaTime)
        {
            float size = Mathf.Lerp(0.5f, blastRadius, i / expansionTime);
            collider.transform.localScale = new Vector3(size, size, size);
            if (size >= blastRadius)
            {
                Destroy(collider.gameObject);
                break;
            }
            yield return null;
        }
    }

    public override float OnCoolDown(Character skillUser)
    {
        return coolDownTime + skillUser.GetComponent<FireBlastSkillTree>().additionalCooldownTime;
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<FireBlastSkillTree>().additionalManaCost;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FrostNovaSkill : Skill
{
    public GameObject novaColliderPrefab;

    public float baseRadius;
    public float expandTime;
    public float freezeChance;
    public float baseFreezeDuration;
    public float slowChance;
    public float baseSlowDuration;
    public float baseSlowEffect;
    public float chillChance;
    public float baseChillDuration;
    public float baseChillEffect;

    public override void OnUse(Character skillUser)
    {
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        FrostNovaSkillTree skillTree = skillUser.GetComponent<FrostNovaSkillTree>();
        skillHandler.SetCurrentAttackSpeedMod(0);
        if (!skillTree.doesNotStopMoving)
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

    IEnumerator ExpandSphereCollider(Character skillUser)
    {
        FrostNovaSkillTree skillTree = skillUser.GetComponent<FrostNovaSkillTree>();

        EffectCollider novaCollider = Instantiate(novaColliderPrefab, GameManager.instance.RefinedPos(skillUser.transform.position), Quaternion.identity).GetComponent<EffectCollider>();

        if (!skillTree.slowsAndChillInstead)
        {
            float freezeDuration = baseFreezeDuration * (1 + skillTree.increasedFreezeDuration);

            FreezeEffect freeze = new FreezeEffect(skillUser, freezeDuration, freezeChance);

            novaCollider.SetHostileEffects(0, DamageType.Cold, false, skillUser, null, freeze);
        }
        else
        {
            float slowDuration = baseSlowDuration * (1 + skillTree.increasedSlowDuration);
            float slowEffect = baseSlowEffect + skillTree.increasedSlowEffect;
            float chillDuration = baseChillDuration * (1 + skillTree.increasedChillDuration);
            float chillEffect = baseChillEffect + skillTree.increasedChillEffect;

            SlowEffect slow = new SlowEffect(slowEffect, slowDuration, slowChance);
            ChillEffect chill = new ChillEffect(chillEffect, chillDuration, chillChance);

            novaCollider.SetHostileEffects(0, DamageType.Cold, false, skillUser, null, slow, chill);
        }
        
        for (float i = 0; i <= expandTime + 0.1; i += Time.deltaTime)
        {
            float radius = baseRadius * (1 + skillTree.increasedRadius);
            float size = Mathf.Lerp(0.5f, radius, i / expandTime);
            novaCollider.transform.localScale = new Vector3(size, size, size);
            if (size >= baseRadius)
            {
                Destroy(novaCollider.gameObject);
                break;
            }
            yield return null;
        }
    }

    public override float OnCoolDown(Character skillUser)
    {
        FrostNovaSkillTree skillTree = skillUser.GetComponent<FrostNovaSkillTree>();
        return coolDownTime + skillTree.additionalCooldownTime;
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<FrostNovaSkillTree>().additionalManaCost;
    }
}

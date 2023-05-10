using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChainLightningSkill : Skill
{
    public GameObject lightningBoltPrefab;
    public float lightningBoltSpeed;
    public int numberOfChains;
    public float chainingRange;
    public float chainDamageMultiplier;
    public float baseDamage;

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        skillUser.GetComponent<SkillHandler>().FaceCharacterTarget();
        skillUser.animator.Play("ShootBall");
    }

    public override void UseSkill(Character skillUser)
    {
        Character targetCharacter = skillUser.GetComponent<SkillHandler>().characterTarget;

        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
        EffectCollider collider = Instantiate(lightningBoltPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
        collider.SetEffects(baseDamage, DamageType.Lightning, false, skillUser, targetCharacter);
        Projectile proj = collider.GetComponent<Projectile>();
        proj.projSpeed = lightningBoltSpeed;
        proj.chain = numberOfChains;
        proj.chainingRange = chainingRange;
        proj.chainDamageMultiplier = chainDamageMultiplier;
    }
}

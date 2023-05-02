using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightningOrbSkill : Skill
{
    public GameObject lightningOrbPrefab;
    public GameObject rotationCenterPrefab;
    public float orbRotationSpeed;
    public float orbRotationRadius;
    public int orbPierce;
    public float baseDamage;
    public int numberOfOrbs;
    public float orbDuration;

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        skillUser.animator.Play("ShootBall");

        RotationCenter rotationCenter = Instantiate(rotationCenterPrefab, skillUser.transform.position, Quaternion.identity).GetComponent<RotationCenter>();
        rotationCenter.lifeTime = orbDuration;
        rotationCenter.movingTarget = skillUser.transform;
        rotationCenter.rotationSpeed = orbRotationSpeed;
        for (int i = 0; i < numberOfOrbs; i++)
        {
            Vector3 spawnOffset = Quaternion.AngleAxis(i * 360 / numberOfOrbs, rotationCenter.transform.up) * new Vector3(orbRotationRadius, 0, 0);
            EffectCollider collider = Instantiate(lightningOrbPrefab, rotationCenter.transform).GetComponent<EffectCollider>();
            collider.SetEffects(baseDamage, DamageType.Lightning, false, skillUser, null);
            collider.transform.position = rotationCenter.transform.position + spawnOffset;
            Projectile proj = collider.GetComponent<Projectile>();
            proj.pierce = orbPierce;
        }
    }
}

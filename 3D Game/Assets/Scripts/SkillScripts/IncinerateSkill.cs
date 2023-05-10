using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IncinerateSkill : Skill
{
    public GameObject explosionPrefab;
    public float explosionRadius;
    public float explosionDamage;

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        skillUser.GetComponent<SkillHandler>().FaceCharacterTarget();
        skillUser.animator.Play("Blast");
    }

    public override void UseSkill(Character skillUser)
    {
        Character targetCharacter = skillUser.GetComponent<SkillHandler>().characterTarget;
        EffectCollider explosion = Instantiate(explosionPrefab, targetCharacter.transform.position, Quaternion.identity).GetComponent<EffectCollider>();
        explosion.SetEffects(explosionDamage, DamageType.Fire, false, skillUser, null);
        explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
        skillUser.StartCoroutine(DestroyExplosion(explosion.gameObject));
    }

    private IEnumerator DestroyExplosion(GameObject explosion)
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(explosion);
    }
}

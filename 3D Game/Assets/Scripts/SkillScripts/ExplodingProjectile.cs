using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjectile : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float explosionRadius;
    public float explosionDamage;
    public DamageType explosionDamageType;
    public StatusEffect[] explosionStatusEffects;

    private void OnDestroy()
    {
        EffectCollider explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity).GetComponent<EffectCollider>();
        explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
        explosion.SetEffects(explosionDamage, explosionDamageType, false, GetComponent<EffectCollider>().owner, null, explosionStatusEffects);
        GetComponent<EffectCollider>().owner.StartCoroutine(DestroyExplosion(explosion.gameObject));
    }

    public IEnumerator DestroyExplosion(GameObject explosion)
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(explosion);
    }
}

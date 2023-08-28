using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjectile : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float explosionRadius;
    public float explosionDamage;
    public DamageType explosionDamageType;
    public List<StatusEffect> explosionStatusEffects = new List<StatusEffect>();

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        EffectCollider explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity).GetComponent<EffectCollider>();
        explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
        explosion.SetHostileEffects(explosionDamage, explosionDamageType, false, GetComponent<EffectCollider>().owner, null, explosionStatusEffects.ToArray());
        if (GetComponent<EffectCollider>().owner.gameObject.activeInHierarchy)
        {
            GetComponent<EffectCollider>().owner.StartCoroutine(DestroyExplosion(explosion.gameObject));
        }
        else
        {
            Destroy(explosion.gameObject);
        }
    }

    public IEnumerator DestroyExplosion(GameObject explosion)
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(explosion);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingProjectile : MonoBehaviour
{
    public static float lastExplosion = 0;

    public GameObject explosionPrefab;
    public AudioClip explosionSFX;

    public float explosionRadius;
    public float explosionDamage;
    public DamageType explosionDamageType;
    public List<StatusEffect> explosionStatusEffects = new List<StatusEffect>();

    private void OnDestroy()
    {
        // For Editor, does not create explosion on play mode exit
        if (!gameObject.scene.isLoaded)
        {
            return;
        }

        EffectCollider explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity).GetComponent<EffectCollider>();
        explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);
        explosion.SetHostileEffects(explosionDamage, explosionDamageType, false, GetComponent<EffectCollider>().owner, null, explosionStatusEffects.ToArray());

        Character owner = GetComponent<EffectCollider>().owner;
        if (owner && owner.gameObject.activeInHierarchy)
        {
            if (Time.time - lastExplosion > 0.1f)
            {
                owner.audioSource.PlayOneShot(explosionSFX, 2);
                lastExplosion = Time.time;
            }
            //owner.audioSource.clip = explosionSFX;
            //owner.audioSource.Play();
            explosion.StartCoroutine(DestroyExplosion(explosion.gameObject));
        }
        else
        {
            Destroy(explosion.gameObject);
        }

        owner.audioSource.volume = 0.15f;
    }

    public IEnumerator DestroyExplosion(GameObject explosion)
    {
        yield return new WaitForSeconds(0.12f);
        Destroy(explosion);
    }
}

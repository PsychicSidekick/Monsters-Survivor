using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    public float damage;
    public DamageType dmgType;
    [HideInInspector] public List<StatusEffect> statusEffects = new List<StatusEffect>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            if (damage > 0)
            {
                other.GetComponent<Enemy>().ReceiveDamage(new Damage(damage, other.GetComponent<Character>(), dmgType));
            }

            foreach (StatusEffect statusEffect in statusEffects)
            {
                other.GetComponent<StatusEffectManager>().ApplyStatusEffect(statusEffect);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public float damage;
    public DamageType dmgType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            other.GetComponent<Enemy>().ReceiveDamage(new Damage(damage, other.GetComponent<Character>(), dmgType));
        }
    }
}

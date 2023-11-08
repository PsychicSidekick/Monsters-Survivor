using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class StatusVFX : MonoBehaviour
{
    public StatusEffectManager statusEffectManager;

    public GameObject igniteVFX;
    public GameObject slowVFX;
    public GameObject shockVFX;

    private void Start()
    {
        statusEffectManager = transform.parent.GetComponent<StatusEffectManager>();
    }

    void Update()
    {
        transform.position = transform.parent.position;
        transform.rotation = Quaternion.identity;

        // Set VFX active if its corresponding status effect is present
        igniteVFX.SetActive(SearchForTypeOfStatusEffect(typeof(IgniteEffect)));
        slowVFX.SetActive(SearchForTypeOfStatusEffect(typeof(SlowEffect)));
        shockVFX.SetActive(SearchForTypeOfStatusEffect(typeof(ShockEffect)));
    }

    public bool SearchForTypeOfStatusEffect(Type type)
    {
        return statusEffectManager.statusEffectList.Any(statusEffect => statusEffect.GetType() == type);
    }
}

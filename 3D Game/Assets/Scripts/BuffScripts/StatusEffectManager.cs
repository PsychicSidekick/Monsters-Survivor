using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatusEffectManager : MonoBehaviour
{
    [HideInInspector] public List<StatusEffect> statusEffectList = new List<StatusEffect>();
    private List<StatusEffect> expiredStatusEffects = new List<StatusEffect>();

    private void Update()
    {
        foreach (StatusEffect statusEffect in statusEffectList)
        {
            statusEffect.remainingDuration -= Time.deltaTime;
            if (statusEffect.remainingDuration <= 0)
            {
                RemoveStatusEffect(statusEffect);
            }
            else
            {
                statusEffect.EffectOverTime(GetComponent<Character>(), Time.deltaTime);
            }
        }

        foreach (StatusEffect statusEffect in expiredStatusEffects)
        {
            statusEffectList.Remove(statusEffect);
        }

        expiredStatusEffects.Clear();
    }

    public void ApplyStatusEffect(StatusEffect statusEffect)
    {
        if (Random.Range(1, 101) <= statusEffect.chance)
        {
            StatusEffect dup = FindStatusEffectWithName(statusEffect.name);
            if (dup != null && !expiredStatusEffects.Contains(dup))
            {
                dup.AddStack(statusEffect);
                return;
            }
            statusEffectList.Add(statusEffect);
            statusEffect.OnApply(GetComponent<Character>());
        }
    }

    public void RemoveStatusEffect(StatusEffect statusEffect)
    {
        expiredStatusEffects.Add(statusEffect);
        statusEffect.OnRemove(GetComponent<Character>());
    }

    public StatusEffect FindStatusEffectWithName(string name)
    {
        foreach (StatusEffect statusEffect in statusEffectList)
        {
            if (statusEffect.name == name)
            {
                return statusEffect;
            }
        }

        return null;
    }
}

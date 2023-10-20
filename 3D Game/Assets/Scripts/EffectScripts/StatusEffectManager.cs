using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StatusEffectManager : MonoBehaviour
{
    public List<StatusEffect> statusEffectList = new List<StatusEffect>();
    public List<StatusEffect> expiredStatusEffects = new List<StatusEffect>();
    private Character character;

    private void Start()
    {
        character = GetComponent<Character>();
    }

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
            List<StatusEffect> dups = FindStatusEffectsWithName(statusEffect.name);
            foreach (StatusEffect dup in dups)
            {
                if (dup != null && !expiredStatusEffects.Contains(dup))
                {
                    dup.AddStack(character, statusEffect);

                    if (dup.GetType() == typeof(IgniteEffect))
                    {
                        return;
                    }
                }
            }
            
            if (dups.Count == 0)
            {
                statusEffectList.Add(statusEffect);
                statusEffect.OnApply(character);
            }
        }
    }

    public void RemoveStatusEffect(StatusEffect statusEffect)
    {
        expiredStatusEffects.Add(statusEffect);
        statusEffect.OnRemove(character);
    }

    public List<StatusEffect> FindStatusEffectsWithName(string name)
    {
        List<StatusEffect> statusEffects = new List<StatusEffect>();

        foreach (StatusEffect statusEffect in statusEffectList)
        {
            if (statusEffect.name == name)
            {
                statusEffects.Add(statusEffect);
            }
        }

        return statusEffects;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPassiveButton : MonoBehaviour
{
    public static int pointsAvailable;
    public List<StatModifierTypeValuePair> passiveModifiers = new List<StatModifierTypeValuePair>();
    private List<StatModifier> statModifiers = new List<StatModifier>();
    private Button button;
    public bool allocated;
    public bool allocatable;
    public List<PlayerPassiveButton> connectedPassives;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(delegate { OnClick(); });
        foreach (StatModifierTypeValuePair statMod in passiveModifiers)
        {
            statModifiers.Add(new StatModifier(statMod.statModifierType, statMod.value));
        }
    }

    private void Update()
    {
        button.interactable = allocatable;
    }

    public void OnAllocate()
    {
        allocated = true;
        foreach (PlayerPassiveButton passive in connectedPassives)
        {
            if (passive.allocated == false)
            {
                passive.allocatable = true;
            }
        }
    }

    public void OnClick()
    {
        foreach (StatModifier mod in statModifiers)
        {
            Player.instance.stats.ApplyStatModifier(mod);
        }
        
        button.interactable = false;
        OnAllocate();
    }
}

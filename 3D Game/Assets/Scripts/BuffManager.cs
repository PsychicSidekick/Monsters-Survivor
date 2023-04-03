using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BuffManager : MonoBehaviour
{
    public List<Buff> buffList = new List<Buff>();
    private List<Buff> expiredBuffs = new List<Buff>();

    private void Update()
    {
        foreach (Buff buff in buffList)
        {
            Debug.Log(buff.remainingDuration);
            buff.remainingDuration -= Time.deltaTime;
            if (buff.remainingDuration <= 0)
            {
                RemoveBuff(buff);
            }
            else
            {
                buff.EffectOverTime(GetComponent<Character>(), Time.deltaTime);
            }
        }

        foreach (Buff buff in expiredBuffs)
        {
            buffList.Remove(buff);
        }

        expiredBuffs.Clear();
    }

    public void ApplyBuff(Buff buff)
    {
        Buff dup = FindBuffWithName(buff.buffName);
        if (dup != null)
        {
            dup.AddStack(buff);
            return;
        }
        buffList.Add(buff);
        buff.OnApply(GetComponent<Character>());
    }

    public void RemoveBuff(Buff buff)
    {
        expiredBuffs.Add(buff);
        buff.OnRemove(GetComponent<Character>());
    }

    public Buff FindBuffWithName(string buffName)
    {
        foreach (Buff buff in buffList)
        {
            if (buff.buffName == buffName)
            {
                return buff;
            }
        }

        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamageOverTime : MonoBehaviour
{
    public float damagePerSecond;
    public DamageType damageType;
    public Character owner;
    public List<Character> charactersInArea;

    void Update()
    {
        foreach (Character character in charactersInArea)
        {
            character.ReceiveDamage(new Damage(damagePerSecond * Time.deltaTime, owner, damageType));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            if (character == owner)
            {
                return;
            }
            charactersInArea.Add(other.GetComponent<Character>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            if (character == owner)
            {
                return;
            }
            charactersInArea.Remove(other.GetComponent<Character>());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public StatsManager stats;

    public float life;
    public float mana;
    public int xp;
    public int level;
    
    public bool xpIsDirty = true;

    public static LayerMask targettable = 1 << 7;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<StatsManager>();

        life = stats.maxLife.value;
        mana = stats.maxMana.value;
    }

    protected virtual void Update()
    {
        if (agent.remainingDistance < 0.1)
        {
            animator.SetBool("isWalking", false);
        }

        ReceiveHealing(stats.lifeRegen.value * Time.deltaTime);
        AddMana(stats.manaRegen.value * Time.deltaTime);
    }

    public int GetCurrentLevel()
    {
        int level = 0;
        int requiredXp = 0;

        while (xp >= requiredXp)
        {
            requiredXp += (level + 1) * 1000;
            level++;

            if (level >= 100)
            {
                return level;
            }
        }

        return level;
    }

    public int GetRequiredXp(int level)
    {
        int requiredXp = 0;

        for (int i = 1; i < level; i++)
        {
            requiredXp += i * 1000;
        }

        return requiredXp;
    }

    public void ReceiveXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        this.xp += xp;
        for (int i = 0; i < GetCurrentLevel()-currLevel; i++)
        {
            OnLevelUp();
        }
        xpIsDirty = true;
    }

    public virtual void OnLevelUp()
    {
        Debug.Log("Leveled Up!");
        stats.ApplyStatModifier(new StatModifier(StatType.MaxLife, 5, ModType.flat));
        life = stats.maxLife.value;
    }

    public void ReceiveDamage(Damage dmg)
    {
        if (dmg.value <= 0)
        {
            return;
        }

        dmg.value *= 1 + stats.increasedDamageTaken.value/100;

        float armor = stats.armor.value;
        float evasion = stats.evasion.value;
        float fireRes = stats.fireRes.value;
        float coldRes = stats.coldRes.value;
        float lightningRes = stats.lightningRes.value;

        if (life > 0)
        {
            float evasionChance = (float)(1 - 500 * 1.25 / (500 + Mathf.Pow(evasion / 5, 0.9f)));
            if (Random.Range(1f, 100f) < evasionChance * 100)
            {
                Debug.Log("Evaded");
                return;
            }

            float finalDmg = dmg.value;
            float reduction = 100;

            switch(dmg.type)
            {
                case DamageType.Physical:
                    reduction = (1 - armor / (armor + 5 * finalDmg)) * 100;
                    break;
                case DamageType.Fire:
                    reduction = Mathf.Clamp(fireRes, float.MinValue, 75);
                    break;
                case DamageType.Cold:
                    reduction = Mathf.Clamp(coldRes, float.MinValue, 75);
                    break;
                case DamageType.Lightning:
                    reduction = Mathf.Clamp(lightningRes, float.MinValue, 75);
                    break;
            }

            reduction = (Mathf.Abs(reduction) + (reduction > 0 ? 0 : 100)) / 100;

            life -= finalDmg * reduction;
            CheckDeath();
        }
    }

    public void ReceiveHealing(float healing)
    {
        if (life < stats.maxLife.value)
        {
            life += healing;
        }

        if (life > stats.maxLife.value)
        {
            life = stats.maxLife.value;
        }
    }

    public void ReduceMana(float value)
    {
        if (mana > 0)
        {
            mana -= value;
            if (mana < 0)
            {
                mana = 0;
            }
        }    
    }

    public void AddMana(float value)
    {
        if (mana < stats.maxMana.value)
        {
            mana += value;
        }

        if (mana > stats.maxMana.value)
        {
            mana = stats.maxMana.value;
        }
    }

    public bool CheckSkillCost(float value)
    {
        return mana >= value;
    }

    public void CheckDeath()
    {
        if(life <= 0)
        {
            life = 0;
            OnDeath();
        }
    }

    public void Move(Vector3 targetPosition)
    {
        animator.SetBool("isWalking", true);
        agent.SetDestination(targetPosition);
    }

    public void StopMoving()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        agent.SetDestination(transform.position);
    }

    public void FacePosition(Vector3 position)
    {
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, position - transform.position, 10, 0.0f);
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

    public virtual void OnDeath()
    {
        //Debug.Log(gameObject + " has died");
        gameObject.SetActive(false);
    }

    public virtual void FindGroundTarget()
    {

    }

    public virtual Character FindCharacterTarget()
    {
        return null;
    }
}

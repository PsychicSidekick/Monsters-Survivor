using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public StatsManager stats;

    public float life;
    public float mana;
    public int xp;
    public int level;

    [HideInInspector] public bool xpIsDirty = true;

    public static LayerMask targettable = 1 << 7;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<StatsManager>();
    }

    protected virtual void Start()
    {
        life = stats.maxLife.value;
        mana = stats.maxMana.value;
    }

    protected virtual void Update()
    {
        if (agent.remainingDistance < 0.1)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
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

        float fireRes = stats.fireRes.value;
        float coldRes = stats.coldRes.value;
        float lightningRes = stats.lightningRes.value;

        if (life > 0)
        {
            float reduction = 0;

            switch(dmg.type)
            {
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

            life -= dmg.value * (1 - reduction / 100);
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
        if (Vector3.Distance(transform.position, targetPosition) < 0.2)
        {
            return;
        }
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
        if (animator.GetFloat("ActionSpeed") != 0)
        {
            Vector3 lookDir = Vector3.RotateTowards(transform.forward, position - transform.position, 10, 0.0f);
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
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

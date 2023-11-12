using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public StatsManager stats;
    [HideInInspector] public StatusEffectManager status;
    [HideInInspector] public AudioSource audioSource;

    public float life;
    public float mana;
    public int xp;
    public int level;
    public int increasedRequiredXpPerLevel;
    public int maxLevel;

    [HideInInspector] public bool xpIsDirty = true;

    public static LayerMask targettable = 1 << 7;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<StatsManager>();
        status = GetComponent<StatusEffectManager>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        life = stats.maximumLife.value;
        mana = stats.maximumMana.value;
    }

    protected virtual void Update()
    {
        // Stop walking when arrived at destination
        if (agent.remainingDistance < 0.1)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        // Natural regeneration
        ReceiveHealing(stats.lifeRegeneration.value * Time.deltaTime);
        AddMana(stats.manaRegeneration.value * Time.deltaTime);
    }

    public int GetCurrentLevel()
    {
        int level = 0;
        int requiredXp = 0;

        while (xp >= requiredXp)
        {
            requiredXp += (level + 1) * increasedRequiredXpPerLevel;
            level++;
        }

        return level;
    }

    public int GetRequiredXp(int level)
    {
        int requiredXp = 0;

        // Increase required xp every level
        for (int i = 1; i < level; i++)
        {
            requiredXp += i * increasedRequiredXpPerLevel;
        }

        return requiredXp;
    }

    public void ReceiveXp(int xp)
    {
        int currentLevel = GetCurrentLevel();
        this.xp += xp;
        // Call OnLevelUp for every level increased in this instance of xp gain
        for (int i = 0; i < GetCurrentLevel()-currentLevel; i++)
        {
            OnLevelUp();
        }
        xpIsDirty = true;
    }

    public virtual void OnLevelUp()
    {
        Debug.Log("Leveled Up!");
    }

    public void ReceiveDamage(Damage dmg)
    {
        if (dmg.value <= 0)
        {
            return;
        }

        float fireRes = stats.fireResistance.value;
        float coldRes = stats.coldResistance.value;
        float lightningRes = stats.lightningResistance.value;

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
        life += healing;

        // Prevents going over maximum life
        if (life > stats.maximumLife.value)
        {
            life = stats.maximumLife.value;
        }
    }

    public void ReduceMana(float value)
    {
        mana -= value;

        // Prevents going below 0 mana
        if (mana < 0)
        {
            mana = 0;
        }  
    }

    public void AddMana(float value)
    {
        mana += value;

        // Prevents going over maximum mana
        if (mana > stats.maximumMana.value)
        {
            mana = stats.maximumMana.value;
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
        // Ignore if target position is too close
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

    // Rotate character to face a vector3 position
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

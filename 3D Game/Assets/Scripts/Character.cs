using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    protected StatsManager stats;

    public float life;
    public float mana;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<StatsManager>();

        life = stats.maxLife.value;
        mana = stats.maxMana.value;
    }

    public virtual void Update()
    {
        if (agent.remainingDistance < 0.1)
        {
            animator.SetBool("isWalking", false);
        }

        ReceiveHealing(stats.lifeRegen.value * Time.deltaTime);
        AddMana(stats.manaRegen.value * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GetComponent<Enemy>() && other.CompareTag("AttackHB"))
        {
            ReceiveDamage(new Damage(50, this, DamageType.Fire));
        }
    }

    public void ReceiveDamage(Damage dmg)
    {
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

            reduction = (Mathf.Abs(reduction) + (reduction >= 0 ? 0 : 100)) / 100;

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
            if (mana > stats.maxMana.value)
            {
                mana = stats.maxMana.value;
            }
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
            Destroy(gameObject);
        }
    }

    public void Move(Vector3 targetPosition)
    {
        animator.SetBool("isWalking", true);
        agent.SetDestination(RefinedPos(targetPosition));
    }

    public void StopMoving()
    {
        agent.SetDestination(transform.position);
    }

    public virtual void OnDeath()
    {
        Debug.Log("OnDeath() from Character");
    }

    public Vector3 RefinedPos(Vector3 position)
    {
        return new Vector3(position.x, 1, position.z);
    }
}

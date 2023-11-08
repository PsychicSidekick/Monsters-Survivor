using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;

    // All possible enemies to spawn.
    public GameObject[] enemyPrefabs;

    public TextAsset spawnDataJson;
    public SpawnData spawnData;

    // Enemy stat increases per minute.
    private float flatMaximumLifePerMinute = 50;
    private float incMaximumLifePerMinute = 10;
    private float flatAttackDamagePerMinute = 5;
    private float flatFireResistancePerMinute = 5;
    private float flatColdResistancePerMinute = 5;
    private float flatLightningResistancePerMinute = 5;
    private float incFireDamagePerMinute = 10;
    private float incColdDamagePerMinute = 10;
    private float incLightningDamagePerMinute = 10;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Reads from spawn data json.
        spawnData = JsonUtility.FromJson<SpawnData>(spawnDataJson.text);
        StartCoroutine(SpawnSequence());
    }

    private Vector3 RandomSpawnPositionAroundPlayer(float distanceFromPlayer)
    {
        float randomX = 0;
        float randomZ = 0;

        while (randomX == 0 && randomZ ==0)
        {
            randomX = Random.Range(-1f, 1f);
            randomZ = Random.Range(-1f, 1f);
        }

        Vector3 randomDirection = new Vector3(randomX, 0, randomZ).normalized;
        
        return Player.instance.transform.position + randomDirection * distanceFromPlayer;
    }

    public IEnumerator SpawnSequence()
    {
        // Repeats whole spawn sequence indefinitely.
        while(true)
        {
            // Repeats for every wave.
            for (int i = 0; i < spawnData.waves.Length; i++)
            {
                // Starts every special spawn job.
                for (int j = 0; j < spawnData.waves[i].specialJobs.Length; j++)
                {
                    StartCoroutine(SpecialSpawnJob(spawnData.waves[i].specialJobs[j]));
                }

                // Starts for every spawn job.
                for (int k = 0; k < spawnData.waves[i].jobs.Length; k++)
                {
                    StartCoroutine(SpawnJob(spawnData.waves[i].jobs[k], spawnData.waves[i].duration));
                }

                // Waits until current wave ends.
                yield return new WaitForSeconds(spawnData.waves[i].duration);
            }
        }
    }

    public IEnumerator SpawnJob(Job job, float duration)
    {
        // Repeats for every enemy in job.
        for (int i = 0; i < job.amount; i++)
        {
            Vector3 spawnPosition = RandomSpawnPositionAroundPlayer(12);
            Enemy enemy = Instantiate(enemyPrefabs[job.enemyTypeID], spawnPosition, Quaternion.identity).GetComponent<Enemy>();
            ApplyModifiersOverTime(enemy);
            // Waits until next enemy spawn.
            yield return new WaitForSeconds(duration/job.amount);
        }
    }

    public IEnumerator SpecialSpawnJob(SpecialJob specialJob)
    {
        // Waits until start time of special job.
        yield return new WaitForSeconds(specialJob.startTime);
        // Repeats for every enemy in special job.
        for (int i = 0; i < specialJob.amount; i++)
        {
            Vector3 spawnPosition = RandomSpawnPositionAroundPlayer(12);
            Enemy enemy = Instantiate(enemyPrefabs[specialJob.enemyTypeID], spawnPosition, Quaternion.identity).GetComponent<Enemy>();
            ApplyModifiersOverTime(enemy);
        }
    }

    // Apply stat and xp yield modifiers to enemy depending on the amount of time passed.
    public void ApplyModifiersOverTime(Enemy enemy)
    {
        // Minutes passed since this run started.
        float minutesPassed = Mathf.Floor(GameManager.instance.GetCurrentRunTime() / 60);

        // Multiplier to stat modifiers, increased by 0.5 every 10 minutes passed.
        float statMultiplier = minutesPassed * (1 + Mathf.Floor(minutesPassed / 10) * 0.5f);

        // Additional xp yields to enemies, increased by 150 every 10 minutes passed.
        int additionalXpYield = (int)Mathf.Floor(minutesPassed / 10) * 150;

        enemy.xpYield += additionalXpYield;

        enemy.stats.ApplyStatModifier(new StatModifier(StatModifierType.flat_MaximumLife, flatMaximumLifePerMinute * statMultiplier));
        enemy.stats.ApplyStatModifier(new StatModifier(StatModifierType.inc_MaximumLife, incMaximumLifePerMinute * statMultiplier));
        enemy.stats.ApplyStatModifier(new StatModifier(StatModifierType.flat_AttackDamage, flatAttackDamagePerMinute * statMultiplier));
        enemy.stats.ApplyStatModifier(new StatModifier(StatModifierType.flat_FireResistance, flatFireResistancePerMinute * statMultiplier));
        enemy.stats.ApplyStatModifier(new StatModifier(StatModifierType.flat_ColdResistance, flatColdResistancePerMinute * statMultiplier));
        enemy.stats.ApplyStatModifier(new StatModifier(StatModifierType.flat_LightningResistance, flatLightningResistancePerMinute * statMultiplier));
        enemy.stats.ApplyStatModifier(new StatModifier(StatModifierType.flat_IncreasedFireDamage, incFireDamagePerMinute * statMultiplier));
        enemy.stats.ApplyStatModifier(new StatModifier(StatModifierType.flat_IncreasedColdDamage, incColdDamagePerMinute * statMultiplier));
        enemy.stats.ApplyStatModifier(new StatModifier(StatModifierType.flat_IncreasedLightningDamage, incLightningDamagePerMinute * statMultiplier));
    }
}

[System.Serializable]
public class SpawnData
{
    public Wave[] waves;
}

[System.Serializable]
public class Wave
{
    public float duration;
    public Job[] jobs;
    public SpecialJob[] specialJobs; 
}

[System.Serializable]
public class Job
{
    public int enemyTypeID;
    public int amount;
}

[System.Serializable]
public class SpecialJob : Job
{
    public float startTime;
}

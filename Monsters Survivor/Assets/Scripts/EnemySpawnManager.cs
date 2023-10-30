using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public TextAsset spawnDataJson;
    public SpawnData spawnData;

    public static EnemySpawnManager instance;

    public float startTime;

    private float flatMaximumLifePerMinute = 50;
    private float incMaximumLifePerMinute = 10;
    private float incAttackSpeedPerMinute = 5;
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
        spawnData = JsonUtility.FromJson<SpawnData>(spawnDataJson.text);
        startTime = Time.time;
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
        // repeat for every wave
        for (int i = 0; i < spawnData.waves.Length; i++)
        {
            // repeat for every specialJob
            for (int j = 0; j < spawnData.waves[i].specialJobs.Length; j++)
            {
                StartCoroutine(SpecialSpawnJob(spawnData.waves[i].specialJobs[j]));
            }

            // repeat for every job
            for (int k = 0; k < spawnData.waves[i].jobs.Length; k++)
            {
                StartCoroutine(SpawnJob(spawnData.waves[i].jobs[k], spawnData.waves[i].duration));
            }

            yield return new WaitForSeconds(spawnData.waves[i].duration);
        }
    }

    public IEnumerator SpawnJob(Job job, float duration)
    {
        for (int i = 0; i < job.amount; i++)
        {
            Vector3 spawnPosition = RandomSpawnPositionAroundPlayer(12);
            StatsManager stats = Instantiate(enemyPrefabs[job.enemyTypeID], spawnPosition, Quaternion.identity).GetComponent<Character>().stats;
            ApplyStatModifiers(stats);
            yield return new WaitForSeconds(duration/job.amount);
        }
    }

    public IEnumerator SpecialSpawnJob(SpecialJob specialJob)
    {
        yield return new WaitForSeconds(specialJob.startTime);
        for (int i = 0; i < specialJob.amount; i++)
        {
            Vector3 spawnPosition = RandomSpawnPositionAroundPlayer(12);
            StatsManager stats = Instantiate(enemyPrefabs[specialJob.enemyTypeID], spawnPosition, Quaternion.identity).GetComponent<Character>().stats;
            ApplyStatModifiers(stats);
        }
    }

    public void ApplyStatModifiers(StatsManager statsManager)
    {
        float minutePassed = Mathf.Floor((Time.time - startTime) / 60);

        statsManager.ApplyStatModifier(new StatModifier(StatModType.flat_MaximumLife, flatMaximumLifePerMinute * minutePassed));
        statsManager.ApplyStatModifier(new StatModifier(StatModType.inc_MaximumLife, incMaximumLifePerMinute * minutePassed));
        statsManager.ApplyStatModifier(new StatModifier(StatModType.inc_AttackSpeed, incAttackSpeedPerMinute * minutePassed));
        statsManager.ApplyStatModifier(new StatModifier(StatModType.flat_AttackDamage, flatAttackDamagePerMinute * minutePassed));
        statsManager.ApplyStatModifier(new StatModifier(StatModType.flat_FireResistance, flatFireResistancePerMinute * minutePassed));
        statsManager.ApplyStatModifier(new StatModifier(StatModType.flat_ColdResistance, flatColdResistancePerMinute * minutePassed));
        statsManager.ApplyStatModifier(new StatModifier(StatModType.flat_LightningResistance, flatLightningResistancePerMinute * minutePassed));
        statsManager.ApplyStatModifier(new StatModifier(StatModType.flat_IncreasedFireDamage, incFireDamagePerMinute * minutePassed));
        statsManager.ApplyStatModifier(new StatModifier(StatModType.flat_IncreasedColdDamage, incColdDamagePerMinute * minutePassed));
        statsManager.ApplyStatModifier(new StatModifier(StatModType.flat_IncreasedLightningDamage, incLightningDamagePerMinute * minutePassed));
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
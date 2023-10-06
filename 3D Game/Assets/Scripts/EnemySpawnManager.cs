using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public TextAsset spawnDataJson;
    public SpawnData spawnData;

    private void Start()
    {
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
            Instantiate(enemyPrefabs[job.enemyTypeID], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(duration/job.amount);
        }
    }

    public IEnumerator SpecialSpawnJob(SpecialJob specialJob)
    {
        yield return new WaitForSeconds(specialJob.startTime);
        for (int i = 0; i < specialJob.amount; i++)
        {
            Vector3 spawnPosition = RandomSpawnPositionAroundPlayer(12);
            Instantiate(enemyPrefabs[specialJob.enemyTypeID], spawnPosition, Quaternion.identity);
        }
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

using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class GameLoopManager : MonoBehaviour
{
    public static Vector3[] NodePositions;
    private static Queue<Enemy> enemiesToRemoveQueue;
    private static Queue<int> EnemyIDToSpawn;

    public Transform NodeParent;

    public Wave[] waves;
    private int currentWaveIndex = 0;
    private bool isSpawningWave = false;

    // Waitiing time at the start of round
    public float initialWaitTime = 10f;

    // Waiting time between next waves
    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private bool isInitialWait = true;

    public bool LoopShouldEnd;

    // Start is called before the first frame update
    void Start()
    {
        EnemyIDToSpawn = new Queue<int>();
        enemiesToRemoveQueue = new Queue<Enemy>();
        EntitySpawner.Init();

        NodePositions = new Vector3[NodeParent.childCount];

        for (int i = 0; i < NodePositions.Length; i++)
        {
            NodePositions[i] = NodeParent.GetChild(i).position;
        }

        waveCountdown = initialWaitTime;
        StartCoroutine(GameLoop());
        //InvokeRepeating("SpawnTest", 0f, 1f);
    }

    IEnumerator GameLoop()
    {
        while (!LoopShouldEnd)
        {
            if (!isSpawningWave)
            {
                if (waveCountdown <= 0)
                {
                    if (currentWaveIndex < waves.Length)
                    {
                        StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                        currentWaveIndex++;
                        waveCountdown = timeBetweenWaves;
                        isInitialWait = false;
                    }
                    else
                    {
                        // No more waves, handle end of game logic here
                        LoopShouldEnd = true;
                        Debug.Log("Waves Ended!");
                    }
                }
                else
                {
                    waveCountdown -= Time.deltaTime;
                }
            }

            // Move Enemies
            NativeArray<Vector3> NodesToUse = new NativeArray<Vector3>(NodePositions, Allocator.TempJob);
            NativeArray<int> NodeIndices = new NativeArray<int>(EntitySpawner.EnemiesInGame.Count, Allocator.TempJob);
            NativeArray<float> EnemySpeeds = new NativeArray<float>(EntitySpawner.EnemiesInGame.Count, Allocator.TempJob);
            TransformAccessArray EnemyAccess = new TransformAccessArray(EntitySpawner.EnemiesInGameTransform.ToArray(), 2);

            for (int i = 0; i < EntitySpawner.EnemiesInGame.Count; i++)
            {
                EnemySpeeds[i] = EntitySpawner.EnemiesInGame[i].Speed;
                NodeIndices[i] = EntitySpawner.EnemiesInGame[i].NodeIndex;
            }

            MoveEnemiesJob MoveJob = new MoveEnemiesJob
            {
                NodePositions = NodesToUse,
                EnemySpeeds = EnemySpeeds,
                NodeIndex = NodeIndices,
                deltaTime = Time.deltaTime
            };

            JobHandle MoveEnemyJobHandle = MoveJob.Schedule(EnemyAccess);
            MoveEnemyJobHandle.Complete();

            for (int i = 0; i < EntitySpawner.EnemiesInGame.Count; i++)
            {
                EntitySpawner.EnemiesInGame[i].NodeIndex = NodeIndices[i];

                if (EntitySpawner.EnemiesInGame[i].NodeIndex == NodePositions.Length)
                {
                    EnqueueEnemyToRemove(EntitySpawner.EnemiesInGame[i]);
                }
            }

            NodesToUse.Dispose();
            NodeIndices.Dispose();
            EnemySpeeds.Dispose();
            EnemyAccess.Dispose();

            // Remove Enemies
            if (enemiesToRemoveQueue.Count > 0)
            {
                for (int i = 0; i < enemiesToRemoveQueue.Count; i++)
                {
                    EntitySpawner.RemoveEnemy(enemiesToRemoveQueue.Dequeue());
                }
            }

            yield return null;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        isSpawningWave = true;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            EntitySpawner.SpawnEnemy(1);// For now all enemies have ID of 1
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }

        isSpawningWave = false;
    }

    public static void EnqueueIDToSpawn(int ID)
    {
        EnemyIDToSpawn.Enqueue(ID);

    }

    public static void EnqueueEnemyToRemove(Enemy EnemyToRemove)
    {
        enemiesToRemoveQueue.Enqueue(EnemyToRemove);
    }
}

[System.Serializable]
public class Wave
{
    public string name;
    public int enemyCount;
    public float spawnRate; // Time between spawns within the wave
}

public struct MoveEnemiesJob : IJobParallelForTransform
{
    [NativeDisableParallelForRestriction]
    public NativeArray<Vector3> NodePositions;

    [NativeDisableParallelForRestriction]
    public NativeArray<float> EnemySpeeds;

    [NativeDisableParallelForRestriction]
    public NativeArray<int> NodeIndex;

    public float deltaTime;

    public void Execute(int index, TransformAccess transform)
    {
        if (NodeIndex[index] < NodePositions.Length)
        {
            Vector3 PositionToMoveTo = NodePositions[NodeIndex[index]];
            transform.position = Vector3.MoveTowards(transform.position, PositionToMoveTo, EnemySpeeds[index] * deltaTime);

            if (transform.position == PositionToMoveTo)
            {
                NodeIndex[index]++;
            }
        }
        
    }
}

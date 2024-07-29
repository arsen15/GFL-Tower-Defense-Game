using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    // Use the size of this array to display the number of waves left on screen.
    public Wave[] waves;
    private int currentWaveIndex = 0;
    private bool isSpawningWave = false;

    // Waitiing time at the start of round
    public float initialWaitTime = 10f;

    // Waiting time between next waves
    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    //private bool isInitialWait = true;

    public bool GameIsOver;

    public TextMeshProUGUI waveNumber;
    public TextMeshProUGUI timerText;

    public GameObject VictoryUI;
    public GameObject DefeatUI;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        EnemyIDToSpawn = new Queue<int>();
        enemiesToRemoveQueue = new Queue<Enemy>();
        EntitySpawner.Init();

        NodePositions = new Vector3[NodeParent.childCount];

        for (int i = 0; i < NodePositions.Length; i++)
        {
            NodePositions[i] = NodeParent.GetChild(i).position;
        }

        waveCountdown = initialWaitTime;
        UpdateWaveNumberText();
        UpdateTimerText();
        StartCoroutine(GameLoop());
        StartCoroutine(MoveEnemies());

        VictoryUI.SetActive(false);
        DefeatUI.SetActive(false);
        GameIsOver = false;
    }

    private void Update()
    {
        UpdateTimerText();
    }


    private void UpdateWaveNumberText()
    {
        waveNumber.text = $"{currentWaveIndex + 0} / {waves.Length}";
    }

    private void UpdateTimerText()
    {
        timerText.text = $"{(int)waveCountdown + 0}";
    }

    IEnumerator GameLoop()
    {
        while (!GameIsOver)
        {
            if (!isSpawningWave)
            {
                if (waveCountdown <= 0)
                {
                    if (currentWaveIndex < waves.Length)
                    {
                        audioManager.PlaySFX(audioManager.waveStart);

                        StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                        PlayerStats.Rounds++;
                        currentWaveIndex++;
                        waveCountdown = timeBetweenWaves;
                        //isInitialWait = false;
                        UpdateWaveNumberText();
                        UpdateTimerText() ;
                    }
                    else
                    {
                        // No more waves, handle end of game logic here
                        CheckForVictory();
                        Debug.Log("Waves Ended!");

                    }
                }
                else
                {
                    waveCountdown -= Time.deltaTime;
                }
            }

            yield return null;
        }
    }

    void Defeat()
    {
        GameIsOver = true;
        DefeatUI.SetActive(true);
        Debug.Log("Game Over: Defeat!");

        audioManager.StopMusic();
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.levelFail);
        }

        if (DefeatUI.activeSelf)
        {
            Debug.Log("DefeatUI is now active.");
        }
        else
        {
            Debug.LogError("DefeatUI is not active!");
        }

        Time.timeScale = 0f;
        
    }

    void Victory()
    {
        GameIsOver = true;
        Debug.Log("Game Over: Victory!");
        VictoryUI.SetActive(true);

        audioManager.StopMusic();
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.levelComplete);
        }

        if (VictoryUI.activeSelf)
        {
            Debug.Log("VictoryUI is now active.");
        }
        else
        {
            Debug.LogError("VictoryUI is not active!");
        }
        Time.timeScale = 0f;
    }

    public void CheckForVictory()
        // When all waves are over AND all enemies are DEAD
    {// currentWaveIndex == waves.Length && EntitySpawner.EnemiesInGame.Count == 0
        if (currentWaveIndex == waves.Length && EntitySpawner.EnemiesInGame.Count == 0)
        {
            Debug.Log("Conditions for victory are met!");
            Victory();
        }
    }

    // Move enemies Coroutine
    IEnumerator MoveEnemies()
    {
        while (true)
        {
            if (GameIsOver) yield break;

            if (EntitySpawner.EnemiesInGame.Count > 0)
            {
                
                // Move Enemies
                NativeArray<Vector3> NodesToUse = new NativeArray<Vector3>(NodePositions, Allocator.TempJob);
                NativeArray<int> NodeIndices = new NativeArray<int>(EntitySpawner.EnemiesInGame.Count, Allocator.TempJob);
                NativeArray<float> EnemySpeeds = new NativeArray<float>(EntitySpawner.EnemiesInGame.Count, Allocator.TempJob);
                TransformAccessArray EnemyAccess = new TransformAccessArray(EntitySpawner.EnemiesInGameTransform.ToArray(), 2);

                try
                {
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
                            //Damaging the player when enemy reaches end of path
                            PlayerStats.Lives--;
                            EnqueueEnemyToRemove(EntitySpawner.EnemiesInGame[i]);

                            if (PlayerStats.Lives <= 0)
                            {
                                Defeat();
                                yield break;
                            }
                        }
                    }
                }
                finally
                {

                    NodesToUse.Dispose();
                    NodeIndices.Dispose();
                    EnemySpeeds.Dispose();
                    EnemyAccess.Dispose();
                }

                // Remove Enemies
                if (enemiesToRemoveQueue.Count > 0)
                {
                    //for (int i = 0; i < enemiesToRemoveQueue.Count; i++)
                    while(enemiesToRemoveQueue.Count > 0)
                    {
                        EntitySpawner.RemoveEnemy(enemiesToRemoveQueue.Dequeue());
                        
                    }

                    CheckForVictory();
                }
            }
            else
            {
                CheckForVictory() ;
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

        // Immediately check for victory after spawning the last wave
        CheckForVictory();
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

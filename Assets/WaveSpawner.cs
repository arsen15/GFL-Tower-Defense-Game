using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is for testing an idea of wave spawning.
 * DO NOT USE!
 */
public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState{SPAWNING, WAITING, COUNTING};   
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int enemyCount;
        public float spawnRate;
    }

    public Wave[] waves;

    private int nextWave = 0;
    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    private SpawnState spawnState = SpawnState.COUNTING;

    // Start is called before the first frame update
    void Start()
    {
        waveCountDown = timeBetweenWaves;

    }

    // Update is called once per frame
    void Update()
    {
        if(waveCountDown <= 0)
        {
            // If we are not spawning, then we want to start spawning
            if (spawnState != SpawnState.SPAWNING)
            {
                //Start spawning
            } else
            {
                waveCountDown -= Time.deltaTime;
            }
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        spawnState = SpawnState.SPAWNING;

        for (int i = 0; i < wave.enemyCount; i++)
        {

        }

        spawnState = SpawnState.WAITING;
        yield break;
    }
}

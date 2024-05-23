using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    private static Queue<int> EnemyIDToSpawn;
    
    public bool LoopShouldEnd;
    // Start is called before the first frame update
    void Start()
    {
        EnemyIDToSpawn = new Queue<int>();
        EntitySpawner.Init();

        StartCoroutine(GameLoop());
        InvokeRepeating("SpawnTest", 0f, 1f);
        InvokeRepeating("RemoveTest", 0f, 1.5f);

    }

    void SpawnTest()
    {
        EnemyIDToSpawn.Enqueue(1);
    }

    void RemoveTest()
    {
        if (EntitySpawner.EnemiesInGame.Count > 0)
        {
            EntitySpawner.RemoveEnemy(EntitySpawner.EnemiesInGame[Random.Range(0, EntitySpawner.EnemiesInGame.Count)]);
        }
    }

    IEnumerator GameLoop()
    {
        while (LoopShouldEnd == false )
        {
            //Spawn Enemies
            if (EnemyIDToSpawn.Count > 0)
            {
                for (int i = 0; i < EnemyIDToSpawn.Count; ++i)
                {
                    EntitySpawner.SpawnEnemy(EnemyIDToSpawn.Dequeue());
                }
            }

            //Spawn Towers

            // Move Enemies

            //Damage Enemies

            //Remove Enemies

            yield return null;
        }
    }

    public static void EnqueueIDToSpawn(int ID)
    {
        EnemyIDToSpawn.Enqueue(ID);

    }
}

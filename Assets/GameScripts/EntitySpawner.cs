using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    //List to keep track of all enemies
    public static List<Enemy> EnemiesInGame;

    public static List<Transform> EnemiesInGameTransform;

    //Used for Object Pooling
    public static Dictionary<int, GameObject> EnemyPrefabs;
    public static Dictionary<int, Queue<Enemy>> EnemyObjectPools;

    private static bool IsInitialized;
    public static void Init()
    {
        if (!IsInitialized)
        {  
            
            EnemyPrefabs = new Dictionary<int, GameObject>();
            EnemyObjectPools = new Dictionary<int, Queue<Enemy>>();
            EnemiesInGame = new List<Enemy>();
            EnemiesInGameTransform = new List<Transform>();

            EnemySpawnData[] Enemies = Resources.LoadAll<EnemySpawnData>("Enemies");

            foreach (EnemySpawnData enemy in Enemies)
            {
                EnemyPrefabs.Add(enemy.EnemyID, enemy.EnemyPrefab);
                EnemyObjectPools.Add(enemy.EnemyID, new Queue<Enemy>());
            }

            IsInitialized = true;
        } else
        {
            Debug.Log("This class is already initialized!");
        }
    }

    public static Enemy SpawnEnemy(int EnemyID)
    {
        Enemy SpawnedEnemy = null;

        if (EnemyPrefabs.ContainsKey(EnemyID))
        {
            Queue<Enemy> ReferencedQueue = EnemyObjectPools[EnemyID];

            if (ReferencedQueue.Count > 0)
            {
                //Dequeue Enemy and initialize
                SpawnedEnemy = ReferencedQueue.Dequeue();
                SpawnedEnemy.Init();

                SpawnedEnemy.gameObject.SetActive(true);
            } else
            {
                //Instantiate new instance of enemy and initialize
                GameObject NewEnemy = Instantiate(EnemyPrefabs[EnemyID], GameLoopManager.NodePositions[0], Quaternion.identity);
                SpawnedEnemy = NewEnemy.GetComponent<Enemy>();
                SpawnedEnemy.Init();
            }
        } else
        {
            Debug.Log($"Enemy ID of {EnemyID} does not exist");
        }

        EnemiesInGameTransform.Add(SpawnedEnemy.transform);
        EnemiesInGame.Add( SpawnedEnemy );
        SpawnedEnemy.ID = EnemyID;

        return SpawnedEnemy;
    }

    public static void RemoveEnemy(Enemy EnemyToRemove)
    {
        EnemyObjectPools[EnemyToRemove.ID].Enqueue(EnemyToRemove);
        EnemyToRemove.gameObject.SetActive(false);
        EnemiesInGame.Remove(EnemyToRemove);
        EnemiesInGameTransform.Remove(EnemyToRemove.transform);

    }
}

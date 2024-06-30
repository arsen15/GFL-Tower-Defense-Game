using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildManager : MonoBehaviour
{
    // Singleton pattern
    public static TowerBuildManager instance;

    private GameObject turretToBuild;

    public GameObject melonTower;
    public GameObject appleTower;


    private Dictionary<GameObject, int> turretCosts = new Dictionary<GameObject, int>();

    // Singleton pattern cont.
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one TowerBuildManager in Scene!");
            return;
        }
        instance = this;

        turretCosts.Add(melonTower, 3);
    }

    

    public GameObject GetTowerToBuild()
    {
        return turretToBuild;
    }

    public void SetTowerToBuild(GameObject tower)
    {
        turretToBuild = tower;
    }

    public int GetTurretCost(GameObject turretPrefab)
    {
        if (turretCosts.ContainsKey(turretPrefab))
        {
            return turretCosts[turretPrefab];
        }
        else 
        {
            Debug.LogWarning("Turret cost not found!");
            return 0;
        }
    }
}

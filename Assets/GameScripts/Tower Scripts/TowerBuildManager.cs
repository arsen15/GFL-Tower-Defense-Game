using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildManager : MonoBehaviour
{
    // Singleton pattern
    public static TowerBuildManager instance;

    private GameObject turretToBuild;
    public GameObject standardTurretPrefab;

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

        turretCosts.Add(standardTurretPrefab, 3);
    }

    

    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
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

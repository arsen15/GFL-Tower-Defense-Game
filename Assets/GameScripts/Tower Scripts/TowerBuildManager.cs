using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildManager : MonoBehaviour
{
    // Singleton pattern
    public static TowerBuildManager instance;

    private GameObject turretToBuild;
    public GameObject standardTurretPrefab;

    // Singleton pattern cont.
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one TowerBuildManager in Scene!");
            return;
        }
        instance = this;
    }

    

    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}

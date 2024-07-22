using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildManager : MonoBehaviour
{
    // Singleton pattern
    public static TowerBuildManager instance;

    private TowerBlueprint turretToBuild;
    private TowerPlacementTile selectedNode;

    public GameObject melonTower;
    public GameObject appleTower;


    private Dictionary<GameObject, int> turretCosts = new Dictionary<GameObject, int>();


    public TowerTileUI towerTileUI;

    public GameObject sellEffect;

    public bool CanBuild { get { return turretToBuild != null; } }
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


    public void SelectNode(TowerPlacementTile node)
    {

        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        towerTileUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        towerTileUI.Hide();
    }

    public void SetTowerToBuild(TowerBlueprint tower)
    {
        turretToBuild = tower;
        
        DeselectNode();
    }

    public TowerBlueprint GetTowerToBuild()
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

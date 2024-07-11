using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerPlacementTile : MonoBehaviour
{
    public Color hoverColor;

    private Color startColor;

    private Renderer rend;

    [HideInInspector]
    private GameObject tower;

    [HideInInspector]
    public TowerBlueprint towerBlueprint;

    [HideInInspector]
    public bool isUpgraded = false;

    TowerBuildManager buildManager;

    

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = TowerBuildManager.instance;
    }

    private void OnMouseDown()
    {
        // If there is tower to build, build it
        if (buildManager.CanBuild && tower == null)
        {
            
            BuildTower(buildManager.GetTowerToBuild());
            return;
            
        }

        // If there is no tower to build, select this node if it already has a tower
        if (tower != null)
        {
            buildManager.SelectNode(this);
        }

    }

    void BuildTower(TowerBlueprint blueprint)
    {
        
        //int towerCost = TowerBuildManager.instance.GetTurretCost(turretToBuild);

        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Need more money!");
            return;
        }
        PlayerStats.Money -= blueprint.cost;
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position, transform.rotation);
        tower = _turret;
        towerBlueprint = blueprint;

    }

    public void UpgradeTower()
    {
        if (PlayerStats.Money < towerBlueprint.upgradeCost)
        {
            Debug.Log("Need more money for the upgrade!");
            return;
        }
        PlayerStats.Money -= towerBlueprint.upgradeCost;

        // Remove old tower
        Destroy(tower);

        // Build the upgraded tower
        GameObject _turret = (GameObject)Instantiate(towerBlueprint.upgradedPrefab, transform.position, transform.rotation);
        tower = _turret;

        isUpgraded = true;

        Debug.Log("Tower Upgraded");
    }
    private void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
        {
            return;
        }

        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}

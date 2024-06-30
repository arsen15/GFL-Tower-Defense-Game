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

    private GameObject turret;

    TowerBuildManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = TowerBuildManager.instance;
    }

    private void OnMouseDown()
    {
        if (buildManager.GetTowerToBuild() == null)
        {
            return;
        }

        if (turret != null)
        {
            Debug.Log("Can't Build there!");
            return;
        }

        //Build turret
        GameObject turretToBuild = buildManager.GetTowerToBuild();
 
        int towerCost = TowerBuildManager.instance.GetTurretCost(turretToBuild);

        if (PlayerStats.Money < towerCost)
        {
            Debug.Log("Need more money!");
            return;
        }
        PlayerStats.Money -= towerCost;
        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);

    }
    private void OnMouseEnter()
    {
        if (buildManager.GetTowerToBuild() == null)
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

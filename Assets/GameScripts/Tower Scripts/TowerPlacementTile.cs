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

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't Build there!");
            return;
        }

        //Build turret
        
        
        GameObject turretToBuild = TowerBuildManager.instance.GetTurretToBuild();
 
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
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

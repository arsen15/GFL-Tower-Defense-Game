using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    TowerBuildManager buildManager;

    private void Start()
    {
        buildManager = TowerBuildManager.instance;
    }
    public void PurchaseMelonTower()
    {
        Debug.Log("Bought Melong tower");
        buildManager.SetTowerToBuild(buildManager.melonTower);
    }

    public void PurchaseAppleTower()
    {
        Debug.Log("Bought Apple tower");
        buildManager.SetTowerToBuild(buildManager.appleTower);
    }
}

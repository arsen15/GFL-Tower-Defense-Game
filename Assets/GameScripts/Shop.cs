using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBlueprint appleTower;
    public TowerBlueprint melonTower;

    TowerBuildManager buildManager;

    private void Start()
    {
        buildManager = TowerBuildManager.instance;
    }
    public void PurchaseMelonTower()
    {
        Debug.Log("Bought Melon tower");
        buildManager.SetTowerToBuild(melonTower);
    }

    public void PurchaseAppleTower()
    {
        Debug.Log("Bought Apple tower");
        buildManager.SetTowerToBuild(appleTower);
    }
}

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
        
        buildManager.SetTowerToBuild(melonTower);
    }

    public void PurchaseAppleTower()
    {
        
        buildManager.SetTowerToBuild(appleTower);
    }
}

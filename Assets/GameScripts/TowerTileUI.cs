using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerTileUI : MonoBehaviour
{

    public GameObject ui;
    private TowerPlacementTile target;

    public TextMeshProUGUI upgradeCost;

    public Button upgradeButton;

    public void SetTarget(TowerPlacementTile _target)
    {
        target = _target;

        transform.position = target.transform.position;

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.towerBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        } else
        {
            upgradeCost.text = "MAX LEVEL";
            upgradeButton.interactable = false;
        }

        

        ui.SetActive(true);
        
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTower();
        TowerBuildManager.instance.DeselectNode();
    }
}

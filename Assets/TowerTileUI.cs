using UnityEngine;

public class TowerTileUI : MonoBehaviour
{

    public GameObject ui;
    private TowerPlacementTile target;

    public void SetTarget(TowerPlacementTile _target)
    {
        target = _target;

        transform.position = target.transform.position;

        ui.SetActive(true);
        
    }

    public void Hide()
    {
        ui.SetActive(false);
    }
}

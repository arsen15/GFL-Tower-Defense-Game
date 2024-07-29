using UnityEngine;
using UnityEngine.UI;

public class TowerPlacementUI : MonoBehaviour
{
    public Button towerButton;
    public Image towerImage;
    public Color selectedColor = Color.gray;  
    private Color originalColor;  

    private bool isTowerSelected = false;  

    private void Start()
    {

        if (towerImage != null)
        {
            originalColor = towerImage.color;
        }


        if (towerButton != null)
        {
            towerButton.onClick.AddListener(OnTowerButtonClick);
        }
    }

    private void OnTowerButtonClick()
    {
        if (isTowerSelected)
        {
  
            return;
        }


        if (towerImage != null)
        {
            towerImage.color = selectedColor;
        }

        isTowerSelected = true;

    
        PlaceTower();
    }

    private void PlaceTower()
    {

        Invoke("OnTowerPlaced", 2.0f); 
    }

    private void OnTowerPlaced()
    {

        if (towerImage != null)
        {
            towerImage.color = originalColor;
        }

        isTowerSelected = false;
    }
}

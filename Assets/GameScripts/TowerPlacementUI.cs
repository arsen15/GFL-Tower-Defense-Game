using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPlacementUI : MonoBehaviour
{
    public Button towerButton;
    public Image towerImage;
    public Color selectedColor = Color.gray;
    private Color originalColor;
    private bool isPlacingTower = false;

    private void Start()
    {
        originalColor = towerImage.color;
        towerButton.onClick.AddListener(OnTowerButtonClick);
    }

    private void Update()
    {
        // 如果正在放置塔，且塔放置完成
        if (isPlacingTower && !TowerBuildManager.instance.CanBuild)
        {
            towerImage.color = originalColor;
            isPlacingTower = false;
        }
    }

    private void OnTowerButtonClick()
    {
        towerImage.color = selectedColor;
        isPlacingTower = true;

        // 假设点击按钮后设置一个要建造的塔
        TowerBuildManager.instance.SetTowerToBuild(TowerBuildManager.instance.GetTowerToBuild());

        // 启动一个协程来检查塔是否已放置
        StartCoroutine(CheckTowerPlaced());
    }

    private IEnumerator CheckTowerPlaced()
    {
        // 等待一小段时间来模拟塔放置过程
        yield return new WaitForSeconds(0.1f);

        // 持续检查塔是否放置完成
        while (isPlacingTower)
        {
            if (!TowerBuildManager.instance.CanBuild)
            {
                towerImage.color = originalColor;
                isPlacingTower = false;
            }
            yield return null;
        }
    }
}

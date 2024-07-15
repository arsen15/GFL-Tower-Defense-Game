using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreenController : MonoBehaviour
{
    public GameObject victoryPanel;

    void Start()
    {
        victoryPanel.SetActive(false);
    }

    public void ShowVictoryScreen()
    {
        victoryPanel.SetActive(true);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

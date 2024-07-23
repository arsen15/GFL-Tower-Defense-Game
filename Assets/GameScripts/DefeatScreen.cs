using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatScreen : MonoBehaviour
{
    public GameObject defeatScreen;
    public void Retry()
    {
        Time.timeScale = 1f;

        EntitySpawner.ResetInitialization();
        // Reload the currently active scene based on index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

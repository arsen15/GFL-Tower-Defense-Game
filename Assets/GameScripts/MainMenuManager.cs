using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    public void Play()
    {
        Time.timeScale = 1f;

        EntitySpawner.ResetInitialization();

        Debug.Log("Pressed Play");
        SceneManager.LoadScene("Scenes/Level 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

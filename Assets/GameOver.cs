using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over_Script : MonoBehaviour
{

    public GameObject defeatPanel;
    // Start is called before the first frame update
    void Start()
    {
        defeatPanel.SetActive(false);   
    }

    public void ShowDefeatScreen()
    {
        defeatPanel.SetActive(true);
    }

    public void LoadPlayAgain()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Update is called once per frame
    public void Play()
    {
        //SceneManager.LoadScene("Tower Placement Scene (With UI)");
        Debug.Log("Pressed Play");
        SceneManager.LoadScene("Scenes/Level 1");
    }
}

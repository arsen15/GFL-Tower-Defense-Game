using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
	public GameObject pauseMenu;

	public static bool isPaused = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (isPaused)
			{
				Resume();
			} 
			else 
			{
				Pause();
			}
		}
	}
	void Pause()
	{
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
		isPaused = true;
	}

	public void Menu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("Main Menu");
	}

	public void Resume()
	{
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
		isPaused = false;
    }

	public void Quit()
	{
		Application.Quit();
    }
}

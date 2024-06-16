using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    private bool gameEnded = false;
    
    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
        
    }

    private void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game Over!");
        // TODO: Show menu that prompts player to restart level or return to main menu
    }
}

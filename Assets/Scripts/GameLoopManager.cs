using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    public bool LoopShouldEnd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator GameLoop()
    {
        while (LoopShouldEnd == false )
        {
            //Spawn Enemies

            //Spawn Towers

            // Move Enemies

            //Damage Enemies

            //Remove Enemies

            yield return null;
        }
    }
}

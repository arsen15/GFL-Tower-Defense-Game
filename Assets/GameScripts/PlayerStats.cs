using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Lives;
    public int startLives = 20;

    // Start is called before the first frame update
    void Start()
    {
        Lives = startLives;
    }

    
}

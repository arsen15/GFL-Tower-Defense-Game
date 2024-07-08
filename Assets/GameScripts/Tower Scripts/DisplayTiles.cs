using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTiles : MonoBehaviour
{
    private GameObject tiles;
    void Start()
    {
        tiles = GameObject.FindGameObjectWithTag("Tile");
    }

    private void OnMouseDown()
    {
        tiles.SetActive(true);
    }

}

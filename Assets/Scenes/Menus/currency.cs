using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class currency : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    void Start()
    {
        textMesh.text = "$" + PlayerStats.Money.ToString();
    }
    private float timer = 0f;
    public float seconds = 2f;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= seconds)
        {
            timer = 0f;
            PlayerStats.Money ++;
            textMesh.text = "$" + PlayerStats.Money.ToString() ;
        }
        if (timer >= 0f)
        {
            textMesh.text = "$" + PlayerStats.Money.ToString() ;
        }
    }

}

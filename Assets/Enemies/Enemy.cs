using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float Health;
    public float Speed;
    public int ID;
    public void Init()
    {
        Health = maxHealth;
    }
}

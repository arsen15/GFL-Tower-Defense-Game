using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int NodeIndex;
    public float maxHealth;
    public float Health;
    public float Speed;
    public int ID;
    public void Init()
    {
        Health = maxHealth;
        transform.position = GameLoopManager.NodePositions[0];
        NodeIndex = 0;
    }
    public int coins = 2;
    void Die()
    {
        PlayerStats.Money += coins;
        Destroy(gameObject);
    }

    public void TakeDamage (float amount)
	{
		Health -= amount;

		if (Health <= 0)
		{
			Die();
		}
	}
}

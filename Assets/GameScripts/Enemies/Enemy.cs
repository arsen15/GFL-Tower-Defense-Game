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

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void Init()
    {
        Health = maxHealth;
        transform.position = GameLoopManager.NodePositions[0];
        NodeIndex = 0;
    }
    public int coins = 2;
    void Die()
    {
        Debug.Log("Enemy died");
        PlayerStats.Money += coins;

        audioManager.PlaySFX(audioManager.bugExplosion);
        //Destroy(gameObject);
        Debug.Log("Enemy removed from list");
        EntitySpawner.RemoveEnemy(this); // Remove enemy from the game
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

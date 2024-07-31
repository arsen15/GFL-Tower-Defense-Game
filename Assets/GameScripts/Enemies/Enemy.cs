using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int NodeIndex;
    public float maxHealth;
    public float Health;
    public float Speed;
    public int ID;

    private AudioManager audioManager;

    [Header("Unity Required")]
    public Image healthBar;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void Init()
    {
        Health = maxHealth;
        transform.position = GameLoopManager.NodePositions[0];
        NodeIndex = 0;

        // Reset the health bar to full
        if (healthBar != null)
        {
            healthBar.fillAmount = 1f;
        }
    }
    public int coins = 2;
    void Die()
    {
        PlayerStats.Money += coins;

        audioManager.PlaySFX(audioManager.bugExplosion);
        EntitySpawner.RemoveEnemy(this); // Remove enemy from the game
    }

    public void TakeDamage (float amount)
	{
		Health -= amount;

        healthBar.fillAmount = Health / maxHealth;

		if (Health <= 0)
		{
			Die();
		}
	}
}

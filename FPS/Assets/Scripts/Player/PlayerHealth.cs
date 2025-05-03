using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<int> OnPlayerHealthChanged;

    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        NotifyHealthChanged();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        NotifyHealthChanged();
    }

    private void NotifyHealthChanged()
    {
        OnPlayerHealthChanged?.Invoke(currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        // Add Death Behavior Here
    }
}

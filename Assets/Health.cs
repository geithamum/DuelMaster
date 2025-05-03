using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public NetworkVariable<int> health = new NetworkVariable<int>();

    private void Start()
    {
        currentHealth = maxHealth;
        health.Value = currentHealth;
    }

    // Function to take damage
    public void TakeDamage(int damage)
    {
        if (IsOwner)
        {
            currentHealth -= damage;
            health.Value = currentHealth;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    // Function to handle player death
    private void Die()
    {
        // Handle player death (e.g., respawn, game over, etc.)
        Debug.Log("Player died!");
    }
}

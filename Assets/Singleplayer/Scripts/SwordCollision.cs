using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    public int damage = 10;  // Damage value for the sword

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))  // Check if the sword hits an enemy
        {
            // Get the AISwordFighter component (assuming it's attached to the enemy)
            AISwordFighter enemy = other.GetComponent<AISwordFighter>();
            if (enemy != null)
            {
                enemy.OnHitBySword();  // Call the method to stop movement and start despawn
            }
        }
    }
}

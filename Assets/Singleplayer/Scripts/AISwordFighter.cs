using System.Collections;
using System.Diagnostics;
using UnityEngine;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class AISwordFighter : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float moveSpeed = 3f;  // Movement speed
    public float attackRange = 1.5f;  // Distance to attack
    public float despawnDelay = 4f;  // Time before the enemy despawns after being cut

    private bool isDead = false;  // To track if the enemy is dead
    private bool isMoving = true;  // To track if the enemy should be moving

    private void Update()
    {
        if (player == null || isDead) return;  // If there's no player or enemy is dead, do nothing

        // Calculate the distance from AI to player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            // If player is far, move towards the player
            MoveTowardsPlayer();
        }
        else
        {
            // If player is close enough, attack
            AttackPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (!isMoving) return;  // If not moving, do nothing

        // Calculate direction towards the player, but set z to 0 to restrict movement to the x-y plane
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;  // Make sure there's no movement along the z-axis

        // Move AI towards the player in the x-y plane only
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    private void AttackPlayer()
    {
        // Simulate an attack (you can replace this with an animation later)
        UnityEngine.Debug.Log("AI is attacking the player!");
    }

    // Call this method when the enemy is hit by the sword
    public void OnHitBySword()
    {
        if (isDead) return;  // If already dead, do nothing

        // Stop the enemy's movement
        isDead = true;
        isMoving = false;

        // Optionally, play a "cut" animation or sound effect
        UnityEngine.Debug.Log("Enemy has been cut!");

        // Start the despawn coroutine (this will destroy the enemy after a delay)
        StartCoroutine(DespawnAfterDelay(despawnDelay));
    }

    private IEnumerator DespawnAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the enemy GameObject after the delay
        Destroy(gameObject);
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}

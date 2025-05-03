using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class AISwordFighter : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float moveSpeed = 3f;  // Movement speed
    public float attackRange = 1.5f;  // Distance to attack

    private void Update()
    {
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

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object collided with has the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Handle the collision (e.g., the player takes damage or the AI attacks)
            Debug.Log("Player collided with the enemy!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Optionally, you can use a trigger instead of a solid collision
        if (other.CompareTag("Enemy"))
        {
            // Handle when the player enters the enemy's trigger zone
            Debug.Log("Player is within the enemy's range!");
        }
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}

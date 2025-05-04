using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

//[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class EnemySpawnManager : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public GameObject enemyPrefab;  // The enemy prefab to spawn
    public float spawnRadius = 10f;  // Fixed radius around the player to spawn enemies
    public float spawnInterval = 3f;  // How often enemies spawn (in seconds)
    private float timer = 0f;
    public float interval = 5f;
    [SerializeField] DeathMenu deathMenu;
    private void Start()
    {
        // Start spawning enemies at regular intervals
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            spawnInterval *= 0.15f;
        }
    }

    private void SpawnEnemy()
    {
        // Generate a random direction on the x-z plane
        Vector2 randomDirection = Random.insideUnitCircle.normalized;  // Random direction vector with magnitude of 1

        // Calculate the spawn position at a fixed radius from the player
        Vector3 spawnPosition = new Vector3(
            player.position.x + randomDirection.x * spawnRadius,
            player.position.y + 0.2f,  // Keep the same height as the player
            player.position.z + randomDirection.y * spawnRadius
        );

        // Instantiate a new enemy at the calculated position
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Ensure the newly spawned enemy knows about the player
        AISwordFighter enemyAIScript = newEnemy.GetComponent<AISwordFighter>();
        if (enemyAIScript != null)
        {
            enemyAIScript.player = player;  // Set the player reference for the newly spawned enemy
            enemyAIScript.deathMenu = deathMenu;
        }
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}

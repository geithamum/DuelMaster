using System.Collections;

using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


//[DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class AISwordFighter : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float moveSpeed = 3f;  // Movement speed
    public float attackRange = 2.5f;  // Distance to attack
    public float despawnDelay = 4f;  // Time before the enemy despawns after being cut

    private bool isDead = false;  // To track if the enemy is dead
    private bool isMoving = true;  // To track if the enemy should be moving

    public DeathMenu deathMenu;

    private void Update()
    {
        if (player == null || isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Rotate toward the player regardless of distance
        FacePlayer();

        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Debug.Log("We do get to the attack state");
            AttackPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (!isMoving) return;

        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;  // Restrict movement to horizontal plane

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    private void AttackPlayer()
    {
        SceneManager.LoadScene("UI");

    }

    public void OnHitBySword()
    {
        if (isDead) return;

        isDead = true;
        isMoving = false;

        UnityEngine.Debug.Log("Enemy has been cut!");

        //StartCoroutine(DespawnAfterDelay(despawnDelay));
    }

    //private IEnumerator DespawnAfterDelay(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    Destroy(gameObject);
    //}

    private void FacePlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;  // Keep the enemy upright

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}

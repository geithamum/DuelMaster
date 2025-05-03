using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject swordPrefab;  // Reference to the sword prefab
    public Transform swordHolder;   // Reference to where you want to attach the sword (usually a hand or hip position)

    private GameObject currentSword;

    void Start()
    {
        // Check if the sword prefab is assigned
        if (swordPrefab != null && swordHolder != null)
        {
            // Instantiate the sword and set it as a child of the swordHolder (e.g., player's hand)
            currentSword = Instantiate(swordPrefab, swordHolder.position, swordHolder.rotation, swordHolder);

            // Optionally, you can adjust the sword's position if needed (e.g., in the player's hand)
            currentSword.transform.localPosition = Vector3.zero;  // Reset position to hold it exactly at the holder
            currentSword.transform.localRotation = Quaternion.identity;  // Reset rotation to make it face correctly
        }
        else
        {
            Debug.LogError("SwordPrefab or SwordHolder is not assigned!");
        }
    }
}

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject swordPrefab;
    public Transform swordHolder;

    public Vector3 swordLocalPosition = Vector3.zero;
    public Vector3 swordLocalRotation = Vector3.zero;

    private GameObject currentSword;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);

        if (swordPrefab != null && swordHolder != null)
        {
            currentSword = Instantiate(swordPrefab);
        }
        else
        {
            Debug.LogError("SwordPrefab or SwordHolder is not assigned.");
        }
    }

    void LateUpdate()
    {
        if (currentSword != null && swordHolder != null)
        {
            // Match position and rotation of the holder
            currentSword.transform.position = swordHolder.position;
            currentSword.transform.rotation = swordHolder.rotation;

            // Apply local offset if needed
            currentSword.transform.localPosition += swordLocalPosition;
            currentSword.transform.localRotation *= Quaternion.Euler(swordLocalRotation);
        }
    }
}

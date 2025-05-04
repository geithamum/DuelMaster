using Unity.Netcode;
using UnityEngine;

public class XRNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private GameObject xrRigRoot;

    private void Start()
    {
        if (!IsOwner)
        {
            // Disable input & camera for remote players
            xrRigRoot.SetActive(false);
        }
    }
}

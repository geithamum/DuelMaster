using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSetup : NetworkBehaviour
{
    public GameObject xrRigRoot;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            // Disable XR rig for observers
            xrRigRoot.SetActive(false);
        }
    }
}

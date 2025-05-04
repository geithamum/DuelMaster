using Fusion;
using UnityEngine;

public class NetworkRig : NetworkBehaviour
{
    [Header("Rig Components")]
    [SerializeField] private NetworkTransform playerTransform;
    [SerializeField] private NetworkTransform headTransform;
    [SerializeField] private NetworkTransform leftHandTransform;
    [SerializeField] private NetworkTransform rightHandTransform;
    [SerializeField] private NetworkTransform torsoTransform;

    [Header("Combat")]
    [SerializeField] private Collider torsoCollider;

    public bool IsLocalNetworkRig => Object.HasStateAuthority;
    private HardwareRig hardwareRig;

    public override void Spawned()
    {
        base.Spawned();

        if (IsLocalNetworkRig)
        {
            hardwareRig = FindObjectOfType<HardwareRig>();
            if (hardwareRig == null)
                Debug.LogError("Missing HardwareRig in the scene");

            // Tag the torso collider for hit detection
            torsoCollider.tag = "Torso";
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput<RigState>(out var input))
        {
            playerTransform.transform.SetPositionAndRotation(input.PlayerPosition, input.PlayerRotation);
            torsoTransform.transform.SetPositionAndRotation(input.TorsoPosition, input.TorsoRotation);
            headTransform.transform.SetPositionAndRotation(input.HeadsetPosition, input.HeadsetRotation);
            leftHandTransform.transform.SetPositionAndRotation(input.LeftHandPosition, input.LeftHandRotation);
            rightHandTransform.transform.SetPositionAndRotation(input.RightHandPosition, input.RightHandRotation);
        }
    }

    public override void Render()
    {
        if (IsLocalNetworkRig && hardwareRig != null)
        {
            playerTransform.transform.SetPositionAndRotation(hardwareRig.playerTransform.position, hardwareRig.playerTransform.rotation);
            torsoTransform.transform.SetPositionAndRotation(hardwareRig.torsoTransform.position, hardwareRig.torsoTransform.rotation);
            headTransform.transform.SetPositionAndRotation(hardwareRig.headTransform.position, hardwareRig.headTransform.rotation);
            leftHandTransform.transform.SetPositionAndRotation(hardwareRig.leftHandTransform.position, hardwareRig.leftHandTransform.rotation);
            rightHandTransform.transform.SetPositionAndRotation(hardwareRig.rightHandTransform.position, hardwareRig.rightHandTransform.rotation);
        }
    }
}
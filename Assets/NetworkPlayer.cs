using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{

    public Transform root;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;
    public Transform torso;
    public float offsetY;

    public Renderer[] meshToDisable;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            foreach (var item in meshToDisable)
            {
                item.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            root.position = VrRigReferences.Singleton.root.position;
            root.rotation = VrRigReferences.Singleton.root.rotation;

            head.position = VrRigReferences.Singleton.head.position;
            head.rotation = VrRigReferences.Singleton.head.rotation;

            leftHand.position = VrRigReferences.Singleton.leftHand.position;
            leftHand.rotation = VrRigReferences.Singleton.leftHand.rotation;

            rightHand.position = VrRigReferences.Singleton.rightHand.position;
            rightHand.rotation = VrRigReferences.Singleton.rightHand.rotation;

            torso.position = VrRigReferences.Singleton.torso.position + new Vector3(0, -offsetY, 0);
            torso.rotation = VrRigReferences.Singleton.torso.rotation;
        }
    }
}

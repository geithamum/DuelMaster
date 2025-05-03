using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;
using UnityEngine.UI;

public class NetworkConnect : MonoBehaviour
{
    public TMP_InputField ipInput;

    public void Create()
    {
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        NetworkManager.Singleton.StartHost();
        Debug.Log("Server started on " + transport.ConnectionData.Address);
    }

    public void Join()
    {
        string ipAddress = ipInput.text;

        // Save it (optional)
        PlayerPrefs.SetString("LastIP", ipAddress);
        PlayerPrefs.Save();

        // Set the transport address
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        if (transport != null)
        {
            transport.ConnectionData.Address = ipAddress;
            transport.ConnectionData.Port = 7777; // Make sure this matches server
        }
        else
        {
            Debug.LogError("UnityTransport not found on NetworkManager!");
            return;
        }

        // Start client
        NetworkManager.Singleton.StartClient();
        Debug.Log("Connecting to: " + ipAddress);
    }
    public void JoinLan()
    {
        string ipAddress = "127.0.0.1";

        // Save it (optional)

        // Set the transport address
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        if (transport != null)
        {
            transport.ConnectionData.Address = ipAddress;
            transport.ConnectionData.Port = 7777; // Make sure this matches server
        }
        else
        {
            Debug.LogError("UnityTransport not found on NetworkManager!");
            return;
        }

        // Start client
        NetworkManager.Singleton.StartClient();
        Debug.Log("Connecting to: " + ipAddress);
    }
}

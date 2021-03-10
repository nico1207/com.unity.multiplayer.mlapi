using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Configuration;
using MLAPI.Transports.UNET;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    NetworkManager m_NetworkManager;

    NetworkManager m_PreviousNetworkManager;


    public void OnTestMultipleNetworkManagers()
    {
        if(NetworkManager.Singleton && m_NetworkManager == null && m_PreviousNetworkManager == null)
        {
            m_PreviousNetworkManager = NetworkManager.Singleton;
            var go = new GameObject("NetworkManager");
            m_NetworkManager = go.AddComponent<NetworkManager>();
            var networkConfig = new NetworkConfig();
            m_NetworkManager.NetworkConfig = networkConfig;
            // transport
            var transport = go.AddComponent<UNetTransport>();
            m_NetworkManager.NetworkConfig.NetworkTransport = transport;
            m_NetworkManager.StartServer();
        }
    }

    private void OnDestroy()
    {
        if(m_NetworkManager && m_NetworkManager.IsListening)
        {
            m_NetworkManager.StopHost();
        }

        if(m_PreviousNetworkManager && m_PreviousNetworkManager.IsListening)
        {
            m_PreviousNetworkManager.StopServer();
        }


    }
}

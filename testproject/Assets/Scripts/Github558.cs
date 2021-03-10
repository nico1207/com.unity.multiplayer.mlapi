using UnityEngine;

using MLAPI;
using MLAPI.Configuration;
using MLAPI.Transports.UNET;

public class Github558 : NetworkBehaviour
{
    private NetworkManager m_NetworkManager;

    public override void NetworkStart()
    {
        var go = new GameObject("NetworkManager");
        m_NetworkManager = go.AddComponent<NetworkManager>();
        var networkConfig = new NetworkConfig();
        m_NetworkManager.NetworkConfig = networkConfig;
        // transport
        var transport = go.AddComponent<UNetTransport>();
        m_NetworkManager.NetworkConfig.NetworkTransport = transport;
        m_NetworkManager.StartServer();

        base.NetworkStart();
    }

}

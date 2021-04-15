using UnityEngine;
using MLAPI;

public class ConnectionMode : NetworkBehaviour
{
    [SerializeField]
    private GameObject m_ButtonContainer;

    private void HideButtons()
    {
        m_ButtonContainer.SetActive(false);
    }


    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
        HideButtons();
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        HideButtons();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        HideButtons();
    }
}

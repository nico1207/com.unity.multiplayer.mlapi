using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAPI;

public class UIController : MonoBehaviour
{
    public NetworkManager network;
    public GameObject buttonsUI;

    public void CreateServer()
    {
        network.StartServer();
        HideButtons();
    }

    public void CreateHost()
    {
        network.StartHost();
        HideButtons();
    }

    public void JoinGame()
    {
        network.StartClient();
        HideButtons();
    }

    private void HideButtons()
    {
        List<Button> buttonsList = new List<Button>(buttonsUI.gameObject.GetComponentsInChildren<Button>());
        foreach(Button button in buttonsList)
        {
            if(button.gameObject.name != "TestBug")
            {
                button.gameObject.SetActive(false);
            }
        }


        //buttonsUI.SetActive(false);
    }
}

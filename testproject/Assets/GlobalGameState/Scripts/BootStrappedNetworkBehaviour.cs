using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class BootStrappedNetworkBehaviour : NetworkBehaviour
{
    [SerializeField]
    private bool m_LaunchAsHostInEditor;

    protected virtual void OnAwake()
    {

    }

    private void Awake()
    {
#if UNITY_EDITOR
        if (NetworkManager.Singleton == null)
        {
            GlobalGameState.s_EditorLaunchingAsHost = m_LaunchAsHostInEditor;
            //This will automatically launch the MLAPIBootStrap and then transition directly to the scene this control is contained within (for easy development of scenes)
            GlobalGameState.LoadBootStrapScene();
            return;
        }
#endif
        OnAwake();
    }



    protected virtual void OnStart()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        OnStart();
    }



    protected virtual void OnUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }


    protected virtual void OnFixedUpdate()
    {

    }


    private void FixedUpdate()
    {
        OnFixedUpdate();
    }


    protected virtual void OnLateUpdate()
    {

    }

    private void LateUpdate()
    {
        OnLateUpdate();
    }


    protected virtual void OnDestroyNotification()
    {

    }

    private void OnDestroy()
    {
        OnDestroyNotification();
    }


    protected virtual void UpdateGUI()
    {

    }

    private void OnGUI()
    {
        UpdateGUI();
    }
}



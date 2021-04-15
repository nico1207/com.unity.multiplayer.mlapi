using UnityEngine;
using MLAPI;
using MLAPI.Spawning;


/// <summary>
/// InSceneObjectSample demonstrates how to spawn an in-scene placed GameObject with a NetworkObject component
/// if the in-scene placed GameObject defaults to disabled (i.e. is disabled via the editor's inspector view)
/// Expected results:
/// Visually, you will see the red sphere appear
/// If you look at the console, you will see messages for the in-scene object being spawned or despawned (with verification checks)
/// </summary>
public class InSceneObjectSample : NetworkBehaviour
{
    public override void NetworkStart()
    {
        base.NetworkStart();
        Debug.Log($"{gameObject.name}'s {nameof(InSceneObjectSample)} NetworkStart was invoked!");
    }

    /// <summary>
    /// When the GameObject is enabled, it will spawn the NetworkObject and check to make sure it was added to the SpawnedObjects list (for this sample's verification purposes only)
    /// </summary>
    private void OnEnable()
    {
        if (NetworkManager != null && NetworkManager.IsListening && NetworkManager.IsServer)
        {
            if (NetworkManager.SpawnManager.SpawnedObjects.ContainsValue(NetworkObject))
            {
                Debug.Log($"{gameObject.name} was found within the {nameof(NetworkSpawnManager)}'s SpawnedObjects list!");
            }
            else
            {
                Debug.LogError($"{gameObject.name} was found NOT found within the {nameof(NetworkSpawnManager)}'s SpawnedObjects list!");
            }
        }
    }

    /// <summary>
    /// This will enable or disable the targeted GameObject with a NetworkObject component that is disabled by default via the editor inspector view
    /// </summary>
    public void SpawnInSceneObject()
    {
        if (gameObject.activeInHierarchy)
        {
            if (NetworkManager.SpawnManager.SpawnedObjects.ContainsValue(NetworkObject))
            {
                Debug.Log($"{gameObject.name} was disabled but still found within the {nameof(NetworkSpawnManager)}'s SpawnedObjects list!");
            }
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}

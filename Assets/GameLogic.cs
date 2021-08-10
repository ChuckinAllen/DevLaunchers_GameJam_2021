using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
public class GameLogic : MonoBehaviour
{
    private void Start()
    {
        NetworkManager.Instance.InstantiatePlayerCube();
        NetworkManager.Instance.InstantiateNetworkCamera();

        NetworkManager.Instance.InstantiateChatManager();
    }
}
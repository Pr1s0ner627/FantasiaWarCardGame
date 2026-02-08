using Mirror;
using UnityEngine;

public class NetworkServices : MonoBehaviour
{
    public void Send(string json)
    {
        NetworkClient.Send(new JsonNetMessage { json = json });
    }

    public void Broadcast(string json)
    {
        NetworkServer.SendToAll(new JsonNetMessage { json = json });
    }
}
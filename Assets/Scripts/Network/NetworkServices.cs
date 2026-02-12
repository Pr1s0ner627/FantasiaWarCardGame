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

    public void OnEndTurnClicked()
    {
        var msg = new ActionMessage { 
            action = "endTurn", 
            playerId = NetworkClient.isHost ? "host" : "client"
            };

        var json = JsonUtility.ToJson(msg);
        Send(json);
    }
}
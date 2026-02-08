using Mirror;
using UnityEngine;

public class MessageRouter : MonoBehaviour
{
    public GameManager gameManager;

    void Awake()
    {
        NetworkClient.RegisterHandler<JsonNetMessage>(OnMessage);
        NetworkServer.RegisterHandler<JsonNetMessage>(OnMessageServer);
    }

    void OnMessage(JsonNetMessage msg)
    {
        Handle(msg.json);
    }

    void OnMessageServer(NetworkConnectionToClient conn, JsonNetMessage msg)
    {
        Handle(msg.json);
    }

    void Handle(string json)
    {
        var action = JsonUtility.FromJson<ActionMessage>(json);
        if (action.action == "endTurn")
        {
            gameManager.EndTurn(action.playerId);
        }
    }
}

[System.Serializable]
public class ActionMessage
{
    public string action;
    public string playerId;
}

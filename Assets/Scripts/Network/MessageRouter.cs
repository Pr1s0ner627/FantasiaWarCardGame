using Mirror;
using UnityEngine;

public class MessageRouter : MonoBehaviour
{
    public GameManager gameManager;
    public BoardView boardView;

    void OnEnable()
    {
        NetworkClient.RegisterHandler<JsonNetMessage>(OnClientMessage);
        NetworkServer.RegisterHandler<JsonNetMessage>(OnMessageServer);
    }

    void OnDisable()
    {
        NetworkClient.UnregisterHandler<JsonNetMessage>();
        NetworkServer.UnregisterHandler<JsonNetMessage>();
    }

    void OnClientMessage(JsonNetMessage msg)
    {
        var action = JsonUtility.FromJson<ActionMessage>(msg.json);
        if (action.action == "sync")    {
            gameManager.RefreshUI();
            boardView.RenderFolded(gameManager.Player1.Folded, gameManager.Player2.Folded);
        }
    }

    void OnMessageServer(NetworkConnectionToClient conn, JsonNetMessage msg)
    {
        Handle(msg.json);
    }

    void Handle(string json)
    {
        var action = JsonUtility.FromJson<ActionMessage>(json);
        if (NetworkServer.active && action.action == "endTurn")
        {
            gameManager.EndTurn(action.playerId);

            var sync = JsonUtility.ToJson(new ActionMessage { action = "sync"}); 
            NetworkServer.SendToAll(new JsonNetMessage { json = sync });
        }
    }
}

[System.Serializable]
public class ActionMessage
{
    public string action;
    public string playerId;
}

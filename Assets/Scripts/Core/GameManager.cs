using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerState Player1 = new PlayerState();
    public PlayerState Player2 = new PlayerState();

    public TurnManager TurnManager = new TurnManager();
    public RevealManager RevealManager = new RevealManager();

    private bool p1Ended, p2Ended;

    void Start()
    {
        TurnManager.StartTurn();
    }

    public void EndTurn(string playerId)
    {
        if (playerId == "P1") p1Ended = true;
        if (playerId == "P2") p2Ended = true;

        if (p1Ended && p2Ended)
        {
            RevealManager.Resolve(Player1, Player2);
            p1Ended = p2Ended = false;
            TurnManager.NextTurn();
            TurnManager.StartTurn();
        }
    }
}

using UnityEngine;

public class GameManager
{
    public int playerScore;
    public int turnNumber = 0;

    public void StartGame()
    {
        playerScore = 0;
        turnNumber = 1;
        Debug.Log("Game Started!");
    }
}

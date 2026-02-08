using UnityEngine;

public class TurnManager
{
    public int currentTurn = 0;
    public void NextTurn()
    {
        currentTurn++;
        Debug.Log("Turn " + currentTurn);
    
        if (currentTurn > 6)
        {
            Debug.Log("Game Over!");
        }
    }
}

using System;
using UnityEngine;

public class TurnManager
{
    public int CurrentTurn { get; private set; } = 1;
    public int MaxTurns = 6;
    public float TurnTime = 30f;

    public event Action OnTurnStarted;
    public event Action OnTurnEnded;

    public int GetAvailableCost() => CurrentTurn;

    public void StartTurn()
    {
        OnTurnStarted?.Invoke();
    }

    public void EndTurn()
    {
        OnTurnEnded?.Invoke();
    }

    public void NextTurn()
    {
        CurrentTurn++;
    }
}

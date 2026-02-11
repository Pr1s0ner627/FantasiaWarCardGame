using System;

public class TurnManager
{
    public int CurrentTurn { get; private set; } = 1;
    public int MaxTurns = 6;
    public float TurnDuration = 30f;

    private float timeLeft;
    private bool running;

    public event Action<float> OnTimerTick;
    public event Action OnTimerExpired;

    public void StartTurn()
    {
        timeLeft = TurnDuration;
        running = true;
    }

    public void Tick(float deltaTime)
    {
        if (!running) return;

        timeLeft -= deltaTime;
        OnTimerTick?.Invoke(timeLeft);

        if (timeLeft <= 0f)
        {
            running = false;
            OnTimerExpired?.Invoke();
        }
    }

    public void EndTurn()
    {
        running = false;
    }

    public void AdvanceTurn()
    {
        CurrentTurn++;
    }
}

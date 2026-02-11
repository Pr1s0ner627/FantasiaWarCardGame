using System.Collections.Generic;

public class PlayerState
{
    public List<CardData> Deck { get; set; } = new();
    public List<CardData> Hand { get; set; } = new();
    public List<CardData> Folded { get; set; } = new();
    public int Score { get; set; }
    public bool HasEndedTurn { get; set; }

    public void ResetTurn()
    {
        Folded.Clear();
        HasEndedTurn = false;
    }
}

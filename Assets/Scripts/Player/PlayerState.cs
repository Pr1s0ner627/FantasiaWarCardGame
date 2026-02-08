using System.Collections.Generic;

public class PlayerState
{
    public List<CardData> Hand = new();
    public List<CardData> Folded = new();
    public int Score;
}

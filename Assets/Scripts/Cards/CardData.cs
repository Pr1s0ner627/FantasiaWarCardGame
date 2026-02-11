using System.Collections.Generic;

[System.Serializable]
public class CardData
{
    public int id;
    public string name;
    public int cost;
    public int power;
    public AbilityData ability;
}

[System.Serializable]
public class AbilityData
{
    public string type;
    public int value;
}

[System.Serializable]
public class CardListWrapper
{
    public List<CardData> cards;
}

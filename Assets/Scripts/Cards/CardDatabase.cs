using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    [System.Serializable]
    private class CardDataList
    {
        public List<CardData> cards;
    }

    public List<CardData> LoadAll()
    {
        TextAsset json = Resources.Load<TextAsset>("cardsData"); 
        if (json == null)
        {
            Debug.LogError("cardsData.json not found in Resources folder.");
            return new List<CardData>();
        }

        // Wrap JSON array for JsonUtility
        string wrapped = "{ \"cards\": " + json.text + " }";
        var list = JsonUtility.FromJson<CardDataList>(wrapped);
        return list.cards;
    }
}

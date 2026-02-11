using System.Collections.Generic;
using UnityEngine;

public class HandView : MonoBehaviour
{
    public Transform handRoot;
    public GameObject cardPrefab;

    public void Render(List<CardData> hand)
    {
        foreach (Transform child in handRoot)
            Destroy(child.gameObject);

        foreach (var card in hand)
        {
            var go = Instantiate(cardPrefab, handRoot);
            var view = go.GetComponent<CardView>();
            view.Bind(card);
        }
    }

    public List<CardData> GetSelectedCards()
{
    var selected = new List<CardData>();

    foreach (Transform child in handRoot)
    {
        var view = child.GetComponent<CardView>();
        if (view != null && view.IsSelected)
            selected.Add(view.BoundData);
    }

    return selected;
}

}

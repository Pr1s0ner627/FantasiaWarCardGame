using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private Transform player1Root;
    [SerializeField] private Transform player2Root;
    [SerializeField] private GameObject cardBackPrefab;

    public void RenderFolded(List<CardData> p1Folded, List<CardData> p2Folded)
    {
        Clear(player1Root);
        Clear(player2Root);

        for (int i = 0; i < p1Folded.Count; i++)
        {
            Instantiate(cardBackPrefab, player1Root);
        }

        for (int i = 0; i < p2Folded.Count; i++)
        {
            Instantiate(cardBackPrefab, player2Root);
        }
    }

    private void Clear(Transform root)
    {
        foreach (Transform child in root)
            Destroy(child.gameObject);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField] private Transform player1Root;
    [SerializeField] private Transform player2Root;
    [SerializeField] private GameObject cardBackPrefab; // face-down card visual

    public void RenderFolded(List<CardData> p1Folded, List<CardData> p2Folded)
    {
        Clear(player1Root);
        Clear(player2Root);

        foreach (var _ in p1Folded)
            Instantiate(cardBackPrefab, player1Root);

        foreach (var _ in p2Folded)
            Instantiate(cardBackPrefab, player2Root);
    }

    private void Clear(Transform root)
    {
        foreach (Transform child in root)
            Destroy(child.gameObject);
    }
}

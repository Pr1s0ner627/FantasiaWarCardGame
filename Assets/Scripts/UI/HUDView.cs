using TMPro;
using UnityEngine;

public class HUDView : MonoBehaviour
{
    [SerializeField] private TMP_Text turnText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text costText;

    public void UpdateTurn(int turn, int maxTurns)
    {
        turnText.text = $"Turn {turn}/{maxTurns}";
    }

    public void UpdateScore(int p1Score)
    {
        scoreText.text = $"Score: {p1Score}";
    }

    public void UpdateCost(int used, int available)
    {
        costText.text = $"Cost: {used}/{available}";
    }
}

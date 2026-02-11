using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private TMP_Text powerText;
    [SerializeField] private TMP_Text abilityText;
    [SerializeField] private Image background;
    [SerializeField] private Button button;

    public CardData BoundData { get; private set; }
    public bool IsSelected { get; private set; }

    private void Awake()
    {
        if (button != null)
            button.onClick.AddListener(ToggleSelect);
    }

    public void Bind(CardData data)
    {
        BoundData = data;
        nameText.text = data.name;
        costText.text = $"Cost: {data.cost}";
        powerText.text = $"Power: {data.power}";
        abilityText.text = data.ability != null ? data.ability.type : "-";
    }

    private void ToggleSelect()
    {
        if (!IsSelected)
        {
            int CurrentCost = GameManager.Instance.GetSelectedCost();
            int budget = GameManager.Instance.GetAvailableCost();
            if (CurrentCost + BoundData.cost > budget)
            {
                background.color = Color.red;
                CancelInvoke(nameof(ResetColor));
                Invoke(nameof(ResetColor), 0.2f);

                GameManager.Instance.Draw(GameManager.Instance.Player1, 1);
                GameManager.Instance.RefreshUI();
                return;
            }
        }
        
        IsSelected = !IsSelected;
        background.color = IsSelected ? new Color(0.8f, 1f, 0.8f) : Color.white;

        GameManager.Instance.RefreshUI();
    }

    private void ResetColor()
    {
        if (background != null)
            background.color = IsSelected ? new Color(0.8f, 1f, 0.8f) : Color.white;
    }
}

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextAsset cardsJsonFile;

    [Header("Systems")]
    public TurnManager TurnManager = new();
    public RevealManager RevealManager = new();

    [Header("Players")]
    public PlayerState Player1 = new();
    public PlayerState Player2 = new();

    [Header("UI")]
    [SerializeField] private HandView handView;
    [SerializeField] private BoardView boardView;
    [SerializeField] private TimerUI timerUI;
    [SerializeField] private HUDView hudPanel;

    private List<CardData> cardDatabase;
    private bool p1Ended;
    private bool p2Ended;

    private void Awake()
    {
        Instance = this;
        LoadCards();
    }

    private void Start()
    {
        SetupDeck(Player1);
        SetupDeck(Player2);
        Draw(Player1, 3);
        Draw(Player2, 3);

        TurnManager.StartTurn();
        RefreshUI();

        TurnManager.OnTimerTick += t => timerUI.UpdateTimer(t);
        TurnManager.OnTimerExpired += () => EndTurn("P1");
    }

    private void LoadCards()
    {
        var wrapper = JsonUtility.FromJson<CardListWrapper>(cardsJsonFile.text);
        cardDatabase = wrapper.cards;
    }

    private void SetupDeck(PlayerState player)
    {
        player.Deck = new List<CardData>(12);

        for (int i = 0; i < 12; i++)
        {
            var template = cardDatabase[Random.Range(0, cardDatabase.Count)];
            player.Deck.Add(Clone(template));
        }
    }

    private void Update()
    {
        TurnManager.Tick(Time.deltaTime);
    }

    public void Draw(PlayerState player, int count)
    {
        for (int i = 0; i < count && player.Deck.Count > 0; i++)
        {
            var card = player.Deck[0];
            player.Deck.RemoveAt(0);
            player.Hand.Add(card);
        }
    }

    private CardData Clone(CardData src)
    {
        return new CardData
        {
            id = src.id,
            name = src.name,
            cost = src.cost,
            power = src.power,
            ability = src.ability != null
                ? new AbilityData { type = src.ability.type, value = src.ability.value }
                : null
        };
    }

    public void EndTurn(string playerId)
{
    if (playerId == "P1")
    {
        var selected = handView.GetSelectedCards();

        if (Player1.Hand == null)
        {
            Debug.LogError("Player1.Hand is null");
            Player1.Hand = new List<CardData>();
        }

        foreach (var card in selected)
        {
            if (card == null)
                continue;

            var handCard = Player1.Hand
                .FirstOrDefault(c => c != null && c.id == card.id);

            if (handCard != null)
            {
                Player1.Hand.Remove(handCard);
                Player1.Folded.Add(handCard);
            }
            else
            {
                Debug.LogWarning($"Selected card not found in hand: id={card.id}");
            }
        }

        p1Ended = true;
    }

    if (playerId == "P2") p2Ended = true;

    if (!p1Ended || !p2Ended)
        return;

    RevealManager.Resolve(Player1, Player2);

    p1Ended = p2Ended = false;

    TurnManager.AdvanceTurn();
    Draw(Player1, 1);

    TurnManager.StartTurn();
    RefreshUI();
}
    public void RefreshUI()
    {
        handView?.Render(Player1.Hand);
        hudPanel?.UpdateTurn(TurnManager.CurrentTurn, TurnManager.MaxTurns);
        hudPanel?.UpdateScore(Player1.Score);
        hudPanel?.UpdateCost(GetSelectedCost(), GetAvailableCost());
    }

    public void OnEndTurnClicked()
    {
        EndTurn("P1");
    }

    public int GetAvailableCost() =>TurnManager.CurrentTurn;

    public int GetSelectedCost()
    {
        return handView.GetSelectedCards().Sum(c => c.cost);
    }

    public void RefreshCostUI()
    {
        hudPanel?.UpdateCost(GetAvailableCost(), GetSelectedCost());
    }

    internal void Draw(object player1, int v)
    {
        throw new System.NotImplementedException();
    }
}

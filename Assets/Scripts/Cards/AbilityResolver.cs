using System.Collections.Generic;
using UnityEngine;

public static class CardResolver
{
    private static readonly Dictionary<string, System.Action<PlayerState, PlayerState, CardData>> AbilityMap =
        new()
        {
            { "GainPoints", (self, opp, card) => self.Score += card.ability.value },
            { "StealPoints", (self, opp, card) =>
                {
                    int steal = Mathf.Min(opp.Score, card.ability.value);
                    opp.Score -= steal;
                    self.Score += steal;
                }
            },
            { "DoublePower", (self, opp, card) =>
                {
                    self.Score += card.power * Mathf.Max(0, card.ability.value - 1);
                }
            },
            { "DrawExtraCard", (self, opp, card) => { /* handled in GameManager draw phase */ } },
            { "None", (self, opp, card) => { } }
        };

    public static void Resolve(PlayerState self, PlayerState opp, CardData card)
    {
        if (card.ability == null || string.IsNullOrEmpty(card.ability.type))
            return;

        if (AbilityMap.TryGetValue(card.ability.type, out var action))
        {
            action(self, opp, card);
        }
        else
        {
            Debug.LogWarning($"Unknown ability: {card.ability.type}");
        }
    }
}

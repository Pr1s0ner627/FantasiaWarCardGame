using System.Collections.Generic;
using UnityEngine;

public static class AbilityResolver
{
    private static Dictionary<string, System.Action<PlayerState, PlayerState, CardData>> _map =
        new Dictionary<string, System.Action<PlayerState, PlayerState, CardData>>
        {
            { "GainPoints", (self, opp, card) => self.Score += card.ability.value },
            { "StealPoints", (self, opp, card) =>
                {
                    int steal = Mathf.Min(opp.Score, card.ability.value);
                    opp.Score -= steal;
                    self.Score += steal;
                }
            },
            { "DoublePower", (self, opp, card) => self.Score += card.power * (card.ability.value - 1) }
        };

    public static void Resolve(PlayerState self, PlayerState opp, CardData card)
    {
        self.Score += card.power;
        if (card.ability != null && _map.TryGetValue(card.ability.type, out var action))
        {
            action(self, opp, card);
        }
    }
}

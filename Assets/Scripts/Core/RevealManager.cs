using UnityEngine;

public class RevealManager
{
    public void Resolve(PlayerState p1, PlayerState p2)
    {
        var initiative = DetermineInitiative(p1, p2);
        var other = initiative == p1 ? p2 : p1;

        int revealCount = Mathf.Max(initiative.Folded.Count, other.Folded.Count);

        for (int i = 0; i < revealCount; i++)
        {
            if (i < initiative.Folded.Count)
                ResolveCard(initiative, other, initiative.Folded[i]);

            if (i < other.Folded.Count)
                ResolveCard(other, initiative, other.Folded[i]);
        }

        initiative.Folded.Clear();
        other.Folded.Clear();
    }

    private PlayerState DetermineInitiative(PlayerState p1, PlayerState p2)
    {
        if (p1.Score > p2.Score) return p1;
        if (p2.Score > p1.Score) return p2;
        return Random.value > 0.5f ? p1 : p2;
    }

    private void ResolveCard(PlayerState self, PlayerState opponent, CardData card)
    {
        self.Score += card.power;
        CardResolver.Resolve(self, opponent, card);
    }
}

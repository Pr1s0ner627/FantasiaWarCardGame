using UnityEngine;

public class RevealManager
{
    public void Resolve(PlayerState p1, PlayerState p2)
    {
        PlayerState initiative = p1.Score > p2.Score ? p1 :
                                 p2.Score > p1.Score ? p2 :
                                 (Random.value > 0.5f ? p1 : p2);

        PlayerState other = initiative == p1 ? p2 : p1;

        int max = Mathf.Max(initiative.Folded.Count, other.Folded.Count);

        for (int i = 0; i < max; i++)
        {
            if (i < initiative.Folded.Count)
                AbilityResolver.Resolve(initiative, other, initiative.Folded[i]);

            if (i < other.Folded.Count)
                AbilityResolver.Resolve(other, initiative, other.Folded[i]);
        }

        initiative.Folded.Clear();
        other.Folded.Clear();
    }
}

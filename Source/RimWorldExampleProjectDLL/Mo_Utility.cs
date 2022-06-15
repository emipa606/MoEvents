using Verse;

namespace MoreIncidents;

public static class Mo_Utility
{
    public static void HealHediff(Pawn pawn, Hediff hediff, int amount)
    {
        hediff.Heal(amount);
        pawn.health.hediffSet.DirtyCache();
    }
}
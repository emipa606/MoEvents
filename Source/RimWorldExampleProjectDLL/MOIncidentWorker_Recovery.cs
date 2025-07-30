using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace MoreIncidents;

public class MOIncidentWorker_Recovery : IncidentWorker
{
    private void HealInjuryRandom(Pawn pawn, float amount)
    {
        if (!pawn.health.hediffSet.GetInjuredParts().TryRandomElement(out var part))
        {
            return;
        }

        Hediff_Injury hediff_Injury = null;
        foreach (var hediff_Injury2 in from hediff in pawn.health.hediffSet.hediffs
                 where hediff.Part == part && hediff is Hediff_Injury
                 select hediff)
        {
            if (hediff_Injury == null || hediff_Injury2.Severity > hediff_Injury.Severity)
            {
                hediff_Injury = (Hediff_Injury)hediff_Injury2;
            }
        }

        hediff_Injury?.Heal(amount);
    }

    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var map = (Map)parms.target;
        IEnumerable<Pawn> freeColonists = map.mapPawns.FreeColonists;
        Func<Pawn, bool> predicate;
        if ((predicate = recover.instancePawn) == null)
        {
            predicate = recover.instancePawn = recover.IncidentRecovery.TryExecute;
        }

        bool result;
        if (freeColonists.Where(predicate).TryRandomElement(out var pawn) && pawn.HitPoints < 70)
        {
            pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("MO_Healed"));
            Find.LetterStack.ReceiveLetter("MO_Recovery".Translate(),
                "MO_RecoveryDesc".Translate(pawn.Label, pawn.Named("PAWN")), LetterDefOf.PositiveEvent, pawn);
            HealInjuryRandom(pawn, 30f);
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    private sealed class recover
    {
        public static readonly recover IncidentRecovery = new();

        public static Func<Pawn, bool> instancePawn;

        public bool TryExecute(Pawn col)
        {
            return col.IsColonist;
        }
    }
}
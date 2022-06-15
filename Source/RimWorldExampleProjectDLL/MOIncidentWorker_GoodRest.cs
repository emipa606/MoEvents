using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace MoreIncidents;

public class MOIncidentWorker_GoodRest : IncidentWorker
{
    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var map = (Map)parms.target;
        IEnumerable<Pawn> freeColonists = map.mapPawns.FreeColonists;
        Func<Pawn, bool> predicate;
        if ((predicate = rest.instancePawn) == null)
        {
            predicate = rest.instancePawn = rest.IncidentRest.TryExecute;
        }

        bool result;
        if (freeColonists.Where(predicate).TryRandomElement(out var pawn))
        {
            pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("MO_GoodRest"));
            Find.LetterStack.ReceiveLetter("MO_WellRested".Translate(),
                "MO_WellRestedDesc".Translate(pawn.Label, pawn.Named("PAWN")), LetterDefOf.PositiveEvent, pawn);
            //pawn.Name + " is feeling well rested and fit.", LetterDefOf.PositiveEvent, pawn, null);
            //pawn.Name + " is feeling well rested and fit.", LetterDefOf.PositiveEvent, pawn, null);
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    private sealed class rest
    {
        public static readonly rest IncidentRest = new rest();

        public static Func<Pawn, bool> instancePawn;

        public bool TryExecute(Pawn col)
        {
            return col.IsColonist;
        }
    }
}
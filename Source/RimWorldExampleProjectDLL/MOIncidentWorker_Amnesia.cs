using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace MoreIncidents;

public class MOIncidentWorker_Amnesia : IncidentWorker
{
    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var map = (Map)parms.target;
        IEnumerable<Pawn> freeColonists = map.mapPawns.FreeColonists;
        Func<Pawn, bool> predicate;
        if ((predicate = Amnesiac.instancePawn) == null)
        {
            predicate = Amnesiac.instancePawn = Amnesiac.TryExecute;
        }

        bool result;
        if (freeColonists.Where(predicate).TryRandomElement(out var pawn))
        {
            pawn.jobs.EndCurrentJob(JobCondition.Succeeded, false);
            pawn.jobs.ClearQueuedJobs();
            pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("MO_Amnesia"));
            Find.LetterStack.ReceiveLetter("MO_Amnesia".Translate(),
                "MO_AmnesiaDesc".Translate(pawn.Label, pawn.Named("PAWN")), LetterDefOf.NegativeEvent, pawn);
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    private sealed class Amnesiac
    {
        public static readonly Amnesiac IncidentAmnesia = new();

        public static Func<Pawn, bool> instancePawn;

        public static bool TryExecute(Pawn col)
        {
            return col.IsColonist;
        }
    }
}
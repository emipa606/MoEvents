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
        if ((predicate = amnesiac.instancePawn) == null)
        {
            predicate = amnesiac.instancePawn = amnesiac.IncidentAmnesia.TryExecute;
        }

        bool result;
        if (freeColonists.Where(predicate).TryRandomElement(out var pawn))
        {
            pawn.jobs.EndCurrentJob(JobCondition.Succeeded, false);
            pawn.jobs.ClearQueuedJobs();
            pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("MO_Amnesia"));
            Find.LetterStack.ReceiveLetter("MO_Amnesia".Translate(),
                "MO_AmnesiaDesc".Translate(pawn.Label, pawn.Named("PAWN")), LetterDefOf.NegativeEvent, pawn);
            //pawn.Name + " suddenly forgot what he was doing. He can't remember what to do."
            //pawn.Name + " suddenly forgot what she was doing. She can't remember what to do.", LetterDefOf.NegativeEvent, pawn, null);
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    private sealed class amnesiac
    {
        public static readonly amnesiac IncidentAmnesia = new amnesiac();

        public static Func<Pawn, bool> instancePawn;

        public bool TryExecute(Pawn col)
        {
            return col.IsColonist;
        }
    }
}
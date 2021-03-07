using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace MoreIncidents
{
    public class MOIncidentWorker_Nausea : IncidentWorker
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = (Map) parms.target;
            IEnumerable<Pawn> freeColonists = map.mapPawns.FreeColonists;
            Func<Pawn, bool> predicate;
            if ((predicate = nausea.instancePawn) == null)
            {
                predicate = nausea.instancePawn = nausea.IncidentNausea.TryExecute;
            }

            if (!freeColonists.Where(predicate).TryRandomElement(out var pawn))
            {
                return true;
            }

            pawn.needs.rest.CurLevel = 0.1f;
            pawn.jobs.EndCurrentJob(JobCondition.Succeeded);
            pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("MO_Nauseated"));

            Find.LetterStack.ReceiveLetter("MO_Nausea".Translate(),
                "MO_NauseaDesc"
                    .Translate().Formatted(pawn.Named("PAWN"))
                    .AdjustedFor(pawn), LetterDefOf.NegativeEvent, pawn);

            //pawn.Name + " is feeling nauseous. He vomited all over and needs rest.", LetterDefOf.NegativeEvent, pawn, null);
            //pawn.Name + " is feeling nauseous. She vomited all over and needs rest.", LetterDefOf.NegativeEvent, pawn, null);
            int num;
            for (var i = 0; i < 10; i = num + 1)
            {
                var intVec = CellFinder.RandomClosewalkCellNear(pawn.Position, map, 3);
                var named = DefDatabase<ThingDef>.GetNamed("Filth_Vomit");
                FilthMaker.TryMakeFilth(intVec, map, named);
                num = i;
            }

            return true;
        }

        private sealed class nausea
        {
            public static readonly nausea IncidentNausea = new nausea();

            public static Func<Pawn, bool> instancePawn;

            public bool TryExecute(Pawn col)
            {
                return col.IsColonist;
            }
        }
    }
}
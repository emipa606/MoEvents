using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace MoreIncidents;

public class MOIncidentWorker_Stroke : IncidentWorker
{
    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var map = (Map)parms.target;
        IEnumerable<Pawn> freeColonists = map.mapPawns.FreeColonists;
        Func<Pawn, bool> predicate;
        if ((predicate = stroke.instancePawn) == null)
        {
            predicate = stroke.instancePawn = stroke.IncidentStroke.TryExecute;
        }

        bool result;
        if (freeColonists.Where(predicate).TryRandomElement(out var pawn))
        {
            HealthUtility.DamageUntilDowned(pawn);
            pawn.needs.rest.CurLevel = 10f;
            pawn.jobs.EndCurrentJob(0);
            pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("MO_HadStroke"));
            Find.LetterStack.ReceiveLetter("MO_Stroke".Translate(),
                "MO_StrokeDesc".Translate(pawn.Label, pawn.Named("PAWN")), LetterDefOf.NegativeEvent, pawn);
            //pawn.Name + " had a stroke. He needs medical care and rest.", LetterDefOf.NegativeEvent, pawn, null);
            //pawn.Name + " had a stroke. She needs medical care and rest.", LetterDefOf.NegativeEvent, pawn, null);
            int num;
            for (var i = 0; i < 10; i = num + 1)
            {
                var intVec = CellFinder.RandomClosewalkCellNear(pawn.Position, map, 3);
                var named = DefDatabase<ThingDef>.GetNamed("Filth_Blood");
                FilthMaker.TryMakeFilth(intVec, map, named);
                num = i;
            }

            Find.TickManager.slower.SignalForceNormalSpeedShort();
            result = true;
        }
        else
        {
            result = false;
        }

        return result;
    }

    private sealed class stroke
    {
        public static readonly stroke IncidentStroke = new stroke();

        public static Func<Pawn, bool> instancePawn;

        public bool TryExecute(Pawn col)
        {
            return col.IsColonist;
        }
    }
}
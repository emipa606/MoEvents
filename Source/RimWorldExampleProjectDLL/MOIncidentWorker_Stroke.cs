using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace MoreIncidents
{
	public class MOIncidentWorker_Stroke : IncidentWorker
	{
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
			IEnumerable<Pawn> freeColonists = map.mapPawns.FreeColonists;
			Func<Pawn, bool> predicate;
			bool flag = (predicate = MOIncidentWorker_Stroke.stroke.instancePawn) == null;
			if (flag)
			{
				predicate = (MOIncidentWorker_Stroke.stroke.instancePawn = new Func<Pawn, bool>(MOIncidentWorker_Stroke.stroke.IncidentStroke.TryExecute));
			}
            Pawn pawn = null;
            bool flag2 = GenCollection.TryRandomElement<Pawn>(freeColonists.Where(predicate), out pawn);
			bool flag3 = flag2;
			bool result;
			if (flag3)
			{
                HealthUtility.DamageUntilDowned(pawn);
                pawn.needs.rest.CurLevel = 10f;
                pawn.jobs.EndCurrentJob(0, true);
                pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("MO_HadStroke"), null);
                Find.LetterStack.ReceiveLetter("MO_Stroke".Translate(), "MO_StrokeDesc".Translate(pawn.Label, pawn.Named("PAWN")), LetterDefOf.NegativeEvent, pawn, null);
                //pawn.Name + " had a stroke. He needs medical care and rest.", LetterDefOf.NegativeEvent, pawn, null);
                //pawn.Name + " had a stroke. She needs medical care and rest.", LetterDefOf.NegativeEvent, pawn, null);
                int num;
	            for (int i = 0; i < 10; i = num + 1)
	            {
	            	IntVec3 intVec = CellFinder.RandomClosewalkCellNear(pawn.Position, map, 3);
	            	ThingDef named = DefDatabase<ThingDef>.GetNamed("Filth_Blood", true);
	            	FilthMaker.TryMakeFilth(intVec, map, named, 1);
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
			public bool TryExecute(Pawn col)
			{
				return col.IsColonist;
			}

			public static readonly MOIncidentWorker_Stroke.stroke IncidentStroke = new MOIncidentWorker_Stroke.stroke();

			public static Func<Pawn, bool> instancePawn;
		}
	}
}


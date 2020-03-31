using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace MoreIncidents
{
	public class MOIncidentWorker_GoodRest : IncidentWorker
	{
		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			IEnumerable<Pawn> freeColonists = map.mapPawns.FreeColonists;
			Func<Pawn, bool> predicate;
			bool flag = (predicate = MOIncidentWorker_GoodRest.rest.instancePawn) == null;
			if (flag)
			{
				predicate = (MOIncidentWorker_GoodRest.rest.instancePawn = new Func<Pawn, bool>(MOIncidentWorker_GoodRest.rest.IncidentRest.TryExecute));
			}
            Pawn pawn = null;
            bool flag2 = GenCollection.TryRandomElement<Pawn>(freeColonists.Where(predicate), out pawn);
			bool flag3 = flag2;
			bool result;
			if (flag3)
			{
                pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("MO_GoodRest"), null);
                Find.LetterStack.ReceiveLetter("MO_WellRested".Translate(), "MO_WellRestedDesc".Translate(pawn.Label, pawn.Named("PAWN")), LetterDefOf.PositiveEvent, pawn, null);
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
			public bool TryExecute(Pawn col)
			{
				return col.IsColonist;
			}

			public static readonly MOIncidentWorker_GoodRest.rest IncidentRest = new MOIncidentWorker_GoodRest.rest();

			public static Func<Pawn, bool> instancePawn;
		}
	}
}


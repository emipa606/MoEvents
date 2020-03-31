using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace MoreIncidents
{
	public class MOIncidentWorker_Amnesia : IncidentWorker
	{
		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			IEnumerable<Pawn> freeColonists = map.mapPawns.FreeColonists;
			Func<Pawn, bool> predicate;
			bool flag = (predicate = MOIncidentWorker_Amnesia.amnesiac.instancePawn) == null;
			if (flag)
			{
				predicate = (MOIncidentWorker_Amnesia.amnesiac.instancePawn = new Func<Pawn, bool>(MOIncidentWorker_Amnesia.amnesiac.IncidentAmnesia.TryExecute));
			}
            Pawn pawn = null;
            bool flag2 = GenCollection.TryRandomElement<Pawn>(freeColonists.Where(predicate), out pawn);
			bool flag3 = flag2;
			bool result;
			if (flag3)
			{
                pawn.jobs.EndCurrentJob(JobCondition.Succeeded, false);
                pawn.jobs.ClearQueuedJobs(true);
                pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("MO_Amnesia"), null);
                Find.LetterStack.ReceiveLetter("MO_Amnesia".Translate(), "MO_AmnesiaDesc".Translate(pawn.Label, pawn.Named("PAWN")), LetterDefOf.NegativeEvent, pawn, null);
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
			public bool TryExecute(Pawn col)
			{
				return col.IsColonist;
			}

			public static readonly MOIncidentWorker_Amnesia.amnesiac IncidentAmnesia = new MOIncidentWorker_Amnesia.amnesiac();

			public static Func<Pawn, bool> instancePawn;
		}
	}
}


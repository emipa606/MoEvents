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
            Map map = (Map)parms.target;
			IEnumerable<Pawn> freeColonists = map.mapPawns.FreeColonists;
			Func<Pawn, bool> predicate;
			bool flag = (predicate = MOIncidentWorker_Nausea.nausea.instancePawn) == null;
			if (flag)
			{
				predicate = (MOIncidentWorker_Nausea.nausea.instancePawn = new Func<Pawn, bool>(MOIncidentWorker_Nausea.nausea.IncidentNausea.TryExecute));
			}
            Pawn pawn = null;
            bool flag2 = GenCollection.TryRandomElement<Pawn>(freeColonists.Where(predicate), out pawn);
			bool flag3 = flag2;
			bool result;
			if (flag3) 
			{
                pawn.needs.rest.CurLevel = 0.1f;
                pawn.jobs.EndCurrentJob(JobCondition.Succeeded, true);
                pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("MO_Nauseated"), null);

                Find.LetterStack.ReceiveLetter("MO_Nausea".Translate(),
                    GrammarResolverSimpleStringExtensions.Formatted(Translator
                    .Translate("MO_NauseaDesc"), NamedArgumentUtility.Named(pawn, "PAWN"))
                    .AdjustedFor(pawn, "PAWN", true), LetterDefOf.NegativeEvent, pawn, null);

                //pawn.Name + " is feeling nauseous. He vomited all over and needs rest.", LetterDefOf.NegativeEvent, pawn, null);
                //pawn.Name + " is feeling nauseous. She vomited all over and needs rest.", LetterDefOf.NegativeEvent, pawn, null);
                int num;
                for (int i = 0; i < 10; i = num + 1)
                {
                	IntVec3 intVec = CellFinder.RandomClosewalkCellNear(pawn.Position, map, 3);
                	ThingDef named = DefDatabase<ThingDef>.GetNamed("Filth_Vomit", true);
                	FilthMaker.TryMakeFilth(intVec, map, named, 1);
                	num = i;
                }
				result = true;
			}
			else
			{
				result = true;
			}
			return result;
		}

		private sealed class nausea
		{
			public bool TryExecute(Pawn col)
			{
				return col.IsColonist;
			}

			public static readonly MOIncidentWorker_Nausea.nausea IncidentNausea = new MOIncidentWorker_Nausea.nausea();

			public static Func<Pawn, bool> instancePawn;
		}
	}
}


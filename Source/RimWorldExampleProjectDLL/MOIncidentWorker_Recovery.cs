using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace MoreIncidents
{
	public class MOIncidentWorker_Recovery : IncidentWorker
	{

        private void HealInjuryRandom(Pawn pawn, float amount)
        {
            BodyPartRecord part;
            if (!pawn.health.hediffSet.GetInjuredParts().TryRandomElement(out part))
            {
                return;
            }
            Hediff_Injury hediff_Injury = null;
            foreach (Hediff_Injury hediff_Injury2 in from x in pawn.health.hediffSet.GetHediffs<Hediff_Injury>()
                                                     where x.Part == part
                                                     select x)
            {
                if (hediff_Injury == null || hediff_Injury2.Severity > hediff_Injury.Severity)
                {
                    hediff_Injury = hediff_Injury2;
                }
            }
            if (hediff_Injury != null)
            {
                hediff_Injury.Heal(amount);
            }
        }
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
			IEnumerable<Pawn> freeColonists = map.mapPawns.FreeColonists;
			Func<Pawn, bool> predicate;
			bool flag = (predicate = MOIncidentWorker_Recovery.recover.instancePawn) == null;
			if (flag)
			{
				predicate = (MOIncidentWorker_Recovery.recover.instancePawn = new Func<Pawn, bool>(MOIncidentWorker_Recovery.recover.IncidentRecovery.TryExecute));
			}
			Pawn pawn = null;
			bool flag2 = GenCollection.TryRandomElement<Pawn>(freeColonists.Where(predicate), out pawn);
			int hitPoints = pawn.HitPoints;
			bool flag3 = flag2 && hitPoints < 70;
			bool result;
			if (flag3)
			{
                pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("MO_Healed"), null);
                Find.LetterStack.ReceiveLetter("MO_Recovery".Translate(), "MO_RecoveryDesc".Translate(pawn.Label, pawn.Named("PAWN")), LetterDefOf.PositiveEvent, pawn, null);
                //pawn.Name + " felt a surge of power. One of his injuries got healed and he is feeling exceptionally well.", LetterDefOf.PositiveEvent, pawn, null);
                //pawn.Name + " felt a surge of power. One of her injuries got healed and she is feeling exceptionally well.", LetterDefOf.PositiveEvent, pawn, null);
                this.HealInjuryRandom(pawn, 30f);
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
			public bool TryExecute(Pawn col)
			{
				return col.IsColonist;
			}

			public static readonly MOIncidentWorker_Recovery.recover IncidentRecovery = new MOIncidentWorker_Recovery.recover();

			public static Func<Pawn, bool> instancePawn;
		}
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace MoreIncidents
{
	public class MOIncidentWorker_Insect : IncidentWorker
	{
		public static Faction OfInsectoid
		{
			get
			{
				return Find.FactionManager.FirstFactionOfDef(FactionDef.Named("Insect"));
			} 
		}

		protected override bool TryExecuteWorker(IncidentParms parms)
		{
			Map map = (Map)parms.target;
			Pawn pawn = null;
			Pawn pawn2 = null;
			PawnKindDef pawnKindDef = PawnKindDef.Named("Spelopede");
			PawnKindDef pawnKindDef2 = PawnKindDef.Named("Megaspider");
			bool flag = pawnKindDef == null && pawnKindDef2 == null;
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				Log.Error("Can't spawn any insects");
				result = false;
			}
			else
			{
				IEnumerable<Pawn> source = map.mapPawns.AllPawns.Cast<Pawn>();
				Func<Pawn, bool> predicate;
				bool flag3 = (predicate = MOIncidentWorker_Insect.insect.isInsect) == null;
				if (flag3)
				{
					predicate = (MOIncidentWorker_Insect.insect.isInsect = new Func<Pawn, bool>(MOIncidentWorker_Insect.insect.insects.TryExecute));
				}
				List<Pawn> source2 = source.Where(predicate).ToList<Pawn>();
				double value = (double)(source2.Count<Pawn>() / 3);
				int num = (int)Math.Round(value, 1);
				bool flag4 = num <= 1;
				bool flag5 = flag4;
				if (flag5)
				{
					num = 2;
				}
				IntVec3 intVec = new IntVec3();
				bool flag6 = !RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, 0f);
				bool flag7 = flag6;
				if (flag7)
				{
					result = false;
				}
				else
				{
					IntVec3 intVec2 = CellFinder.RandomClosewalkCellNear(intVec, map, 10);
					int num2;
					for (int i = 0; i < num; i = num2 + 1)
					{
						pawn = PawnGenerator.GeneratePawn(pawnKindDef, MOIncidentWorker_Insect.OfInsectoid);
						pawn.needs.food.CurLevel = 0.01f;
						pawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent, null, false, false, null);
						GenSpawn.Spawn(pawn, intVec2, map);
						num2 = i;
					}
					for (int j = 0; j < num; j = num2 + 1)
					{
						pawn2 = PawnGenerator.GeneratePawn(pawnKindDef2, MOIncidentWorker_Insect.OfInsectoid);
						pawn2.needs.food.CurLevel = 0.01f;
						pawn2.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent, null, false, false, null);
						GenSpawn.Spawn(pawn2, intVec2, map);
						num2 = j;
					}
					Find.LetterStack.ReceiveLetter("MO_Insects".Translate(), "MO_InsectsDesc".Translate(), LetterDefOf.ThreatBig, new TargetInfo(intVec, map, false), null); //"A group of hungry insects have entered your area. They'll do anything to get your food!"
					pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + Rand.Range(90000, 130000);
					pawn2.mindState.exitMapAfterTick = Find.TickManager.TicksGame + Rand.Range(90000, 130000);
					Find.TickManager.slower.SignalForceNormalSpeedShort();
					result = true;
				}
			}
			return result;
		}

		private sealed class insect
		{
			public bool TryExecute(Pawn p)
			{
				return (p.RaceProps.Humanlike && !GenHostility.HostileTo(p, Faction.OfPlayer) && !p.IsColonist) || p.IsPrisonerOfColony;
			}

			public static readonly MOIncidentWorker_Insect.insect insects = new MOIncidentWorker_Insect.insect();

			public static Func<Pawn, bool> isInsect;
		}
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace MoreIncidents
{
	public class MOIncidentWorker_Migration : IncidentWorker
	{
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
			IntVec3 intVec = new IntVec3();
			bool flag = !RCellFinder.TryFindRandomPawnEntryCell(out intVec, map, 0f);
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				result = false;
			}
			else
			{
				bool flag3 = this.SpawnRandomMigrationAt(intVec, map);
				bool flag4 = flag3;
				if (flag4)
				{
					Find.LetterStack.ReceiveLetter("MO_Migration".Translate(), "MO_MigrationDesc".Translate(), LetterDefOf.NeutralEvent, new TargetInfo(intVec, map, false), null); //A large group of animals are migrating through your area. You can hunt, tame or leave them.
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public bool checkDistance(IntVec3 cell1, IntVec3 cell2)
		{
			return (cell1 - cell2).ToVector3().magnitude > 50f;
		}

		public bool SpawnRandomMigrationAt(IntVec3 loc, Map map)
		{
			PawnKindDef pawnKindDef = GenCollection.RandomElement<PawnKindDef>(from a in map.Biome.AllWildAnimals
			where map.mapTemperature.SeasonAcceptableFor(a.race)
			select a);
			bool flag = pawnKindDef == null;
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				Log.Error("No spawnable animals right now.");
				result = false;
			}
			else
			{
				int num = Rand.Range(4, 8);
				int num2 = Rand.Range(2, 6);
				IEnumerable<Pawn> source = map.mapPawns.AllPawns.Cast<Pawn>();
				Func<Pawn, bool> predicate;
				bool flag3 = (predicate = MOIncidentWorker_Migration.migrate.letMigrate) == null;
				if (flag3)
				{
					predicate = (MOIncidentWorker_Migration.migrate.letMigrate = new Func<Pawn, bool>(MOIncidentWorker_Migration.migrate.IncidentMigration.SpawnRandomMigrationAt));
				}
				List<Pawn> source2 = source.Where(predicate).ToList<Pawn>();
				int num3 = source2.Count<Pawn>();
				int num4 = num3 * num / (num3 * num2);
				bool flag4 = num4 <= 8;
				bool flag5 = flag4;
				if (flag5)
				{
					num4 += num;
				}
				int num5 = (int)Math.Round((double)num4, 1);
				IntVec3 intVec = CellFinder.RandomEdgeCell(map);
				bool flag6 = this.checkDistance(loc, intVec);
				bool flag7 = flag6;
				if (flag7)
				{
					int num6;
					for (int i = 0; i < num5; i = num6 + 1)
					{
                        Pawn pawn = PawnGenerator.GeneratePawn(
                        new PawnGenerationRequest(pawnKindDef, null, PawnGenerationContext.NonPlayer, -1, true, false, false, false, true, false, 1f, true, true, true, false, false, false, false, false, 0f,
                            null, 1f, null, null, null, null, null, null));
						IntVec3 intVec2 = CellFinder.RandomClosewalkCellNear(loc, map, 10);
						GenSpawn.Spawn(pawn, intVec2, map);
						this.job1 = new Job(JobDefOf.Goto, intVec)
						{
							exitMapOnArrival = true
						};
						pawn.jobs.StartJob(this.job1, 0, null, false, true, null);
						pawn.mindState.exitMapAfterTick = Find.TickManager.TicksGame + Rand.Range(10000, 12000);
						num6 = i;
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		private Job job1 = new Job();

		private Job job2 = new Job();

		private sealed class migrate
		{
			public bool SpawnRandomMigrationAt(Pawn pwn)
			{
				return pwn.IsColonist;
			}

			public static readonly MOIncidentWorker_Migration.migrate IncidentMigration = new MOIncidentWorker_Migration.migrate();

			public static Func<Pawn, bool> letMigrate;
		}
	}
}


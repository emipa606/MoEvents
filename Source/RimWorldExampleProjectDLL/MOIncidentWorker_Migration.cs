using System;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace MoreIncidents;

public class MOIncidentWorker_Migration : IncidentWorker
{
    private Job job1 = new Job();

    private Job job2 = new Job();

    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var map = (Map)parms.target;
        bool result;
        if (!RCellFinder.TryFindRandomPawnEntryCell(out var intVec, map, 0f))
        {
            result = false;
        }
        else
        {
            if (SpawnRandomMigrationAt(intVec, map))
            {
                Find.LetterStack.ReceiveLetter("MO_Migration".Translate(), "MO_MigrationDesc".Translate(),
                    LetterDefOf.NeutralEvent,
                    new TargetInfo(intVec,
                        map)); //A large group of animals is migrating through your area. You can hunt, tame or leave them.
                result = true;
            }
            else
            {
                result = false;
            }
        }

        return result;
    }

    private bool checkDistance(IntVec3 cell1, IntVec3 cell2)
    {
        return (cell1 - cell2).ToVector3().magnitude > 50f;
    }

    private bool SpawnRandomMigrationAt(IntVec3 loc, Map map)
    {
        var pawnKindDef = (from a in map.Biome.AllWildAnimals
            where map.mapTemperature.SeasonAcceptableFor(a.race)
            select a).RandomElement();
        bool result;
        if (pawnKindDef == null)
        {
            Log.Error("No spawnable animals right now.");
            result = false;
        }
        else
        {
            var num = Rand.Range(4, 8);
            var num2 = Rand.Range(2, 6);
            var source = map.mapPawns.AllPawns;
            Func<Pawn, bool> predicate;
            if ((predicate = migrate.letMigrate) == null)
            {
                predicate = migrate.letMigrate = migrate.IncidentMigration.SpawnRandomMigrationAt;
            }

            var source2 = source.Where(predicate).ToList();
            var num3 = source2.Count;
            var num4 = num3 * num / (num3 * num2);
            if (num4 <= 8)
            {
                num4 += num;
            }

            var num5 = (int)Math.Round((double)num4, 1);
            var intVec = CellFinder.RandomEdgeCell(map);
            if (checkDistance(loc, intVec))
            {
                int num6;
                for (var i = 0; i < num5; i = num6 + 1)
                {
                    var pawn = PawnGenerator.GeneratePawn(
                        new PawnGenerationRequest(pawnKindDef, null, PawnGenerationContext.NonPlayer, -1, true,
                            false, false, false, true, 1f, true, true, true, false));
                    var intVec2 = CellFinder.RandomClosewalkCellNear(loc, map, 10);
                    GenSpawn.Spawn(pawn, intVec2, map);
                    job1 = new Job(JobDefOf.Goto, intVec)
                    {
                        exitMapOnArrival = true
                    };
                    pawn.jobs.StartJob(job1);
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

    private sealed class migrate
    {
        public static readonly migrate IncidentMigration = new migrate();

        public static Func<Pawn, bool> letMigrate;

        public bool SpawnRandomMigrationAt(Pawn pwn)
        {
            return pwn.IsColonist;
        }
    }
}
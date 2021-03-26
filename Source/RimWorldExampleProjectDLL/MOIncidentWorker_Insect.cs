using System;
using System.Linq;
using RimWorld;
using Verse;

namespace MoreIncidents
{
    public class MOIncidentWorker_Insect : IncidentWorker
    {
        private static Faction OfInsectoid => Find.FactionManager.FirstFactionOfDef(FactionDef.Named("Insect"));

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = (Map) parms.target;
            var spelopedePawnKindDef = PawnKindDef.Named("Spelopede");
            var megaspiderPawnKindDef = PawnKindDef.Named("Megaspider");
            bool result;
            if (spelopedePawnKindDef == null && megaspiderPawnKindDef == null)
            {
                Log.Error("Can't spawn any insects");
                result = false;
            }
            else
            {
                var source = map.mapPawns.AllPawns;
                Func<Pawn, bool> predicate;
                if ((predicate = insect.isInsect) == null)
                {
                    predicate = insect.isInsect = insect.insects.TryExecute;
                }

                var source2 = source.Where(predicate).ToList();
                double value = source2.Count / 3;
                var num = (int) Math.Round(value, 1);
                if (num <= 1)
                {
                    num = 2;
                }

                if (!RCellFinder.TryFindRandomPawnEntryCell(out var intVec, map, 0f))
                {
                    result = false;
                }
                else
                {
                    var intVec2 = CellFinder.RandomClosewalkCellNear(intVec, map, 10);
                    var rot = Rot4.FromAngleFlat((map.Center - intVec).AngleFlat);
                    int num2;
                    for (var i = 0; i < num; i = num2 + 1)
                    {
                        var spelopedePawn = PawnGenerator.GeneratePawn(spelopedePawnKindDef, OfInsectoid);
                        GenSpawn.Spawn(spelopedePawn, intVec2, map, rot);
                        spelopedePawn.needs.food.CurLevel = 0.01f;
                        spelopedePawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf
                            .ManhunterPermanent);
                        spelopedePawn.mindState.exitMapAfterTick =
                            Find.TickManager.TicksGame + Rand.Range(90000, 130000);
                        num2 = i;
                    }

                    for (var j = 0; j < num; j = num2 + 1)
                    {
                        var megaspiderPawn = PawnGenerator.GeneratePawn(megaspiderPawnKindDef, OfInsectoid);
                        GenSpawn.Spawn(megaspiderPawn, intVec2, map, rot);
                        megaspiderPawn.needs.food.CurLevel = 0.01f;
                        megaspiderPawn.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf
                            .ManhunterPermanent);
                        megaspiderPawn.mindState.exitMapAfterTick =
                            Find.TickManager.TicksGame + Rand.Range(90000, 130000);
                        num2 = j;
                    }

                    Find.LetterStack.ReceiveLetter("MO_Insects".Translate(), "MO_InsectsDesc".Translate(),
                        LetterDefOf.ThreatBig,
                        new TargetInfo(intVec,
                            map)); //"A group of hungry insects have entered your area. They'll do anything to get your food!"
                    Find.TickManager.slower.SignalForceNormalSpeedShort();
                    result = true;
                }
            }

            return result;
        }

        private sealed class insect
        {
            public static readonly insect insects = new();

            public static Func<Pawn, bool> isInsect;

            public bool TryExecute(Pawn p)
            {
                return p.RaceProps.Humanlike && !p.HostileTo(Faction.OfPlayer) && !p.IsColonist || p.IsPrisonerOfColony;
            }
        }
    }
}
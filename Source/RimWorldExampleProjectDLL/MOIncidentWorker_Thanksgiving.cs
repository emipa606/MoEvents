using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MoreIncidents
{
    public class MOIncidentWorker_Thanksgiving : IncidentWorker
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = (Map) parms.target;
            var faction = Find.FactionManager.RandomNonHostileFaction(true, true, true, TechLevel.Neolithic);
            var faction2 = Find.FactionManager.FirstFactionOfDef(FactionDefOf.PlayerColony);
            var freeColonistsSpawnedCount = map.mapPawns.FreeColonistsSpawnedCount;
            var flag = !faction.HostileTo(faction2) &&
                       map.resourceCounter.TotalHumanEdibleNutrition < 4f * freeColonistsSpawnedCount;
            bool result;
            if (flag)
            {
                var thing = ThingMaker.MakeThing(ThingDef.Named("MealSimple"));
                var thing2 = ThingMaker.MakeThing(ThingDef.Named("MealFine"));
                var thing3 = ThingMaker.MakeThing(ThingDef.Named("MealSimple"));
                var thing4 = ThingMaker.MakeThing(ThingDef.Named("MealFine"));
                var list = new List<Thing>();
                var num = Rand.Range(20, 40);
                var flag2 = num > thing.def.stackLimit;
                if (flag2)
                {
                    num = thing.def.stackLimit;
                }

                var flag3 = num == 0;
                if (flag3)
                {
                    num = 1;
                }

                var num2 = Rand.Range(20, 40);
                var flag4 = num2 > thing2.def.stackLimit;
                if (flag4)
                {
                    num2 = thing2.def.stackLimit;
                }

                var flag5 = num2 == 0;
                if (flag5)
                {
                    num2 = 1;
                }

                thing.stackCount = num;
                thing2.stackCount = num2;
                thing3.stackCount = num;
                thing4.stackCount = num2;
                list.Add(thing);
                list.Add(thing2);
                list.Add(thing3);
                list.Add(thing4);
                var intVec = DropCellFinder.RandomDropSpot(map);
                DropPodUtility.DropThingsNear(intVec, map, list);
                Find.LetterStack.ReceiveLetter("MO_Thanksgiving".Translate(), "MO_ThanksgivingDesc".Translate(),
                    LetterDefOf.PositiveEvent,
                    new TargetInfo(intVec,
                        map)); //"One of the tribes has noticed your food stores are low. They have provided you with some food, no strings attached.\n\nThey thank you for being such good friends and they hope your friendship will be further cemented by this act."
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}
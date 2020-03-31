using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MoreIncidents
{
	public class MOIncidentWorker_Thanksgiving : IncidentWorker
	{
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            Faction faction = Find.FactionManager.RandomNonHostileFaction(true, true, true, TechLevel.Neolithic);
            Faction faction2 = Find.FactionManager.FirstFactionOfDef(FactionDefOf.PlayerColony);
			int freeColonistsSpawnedCount = map.mapPawns.FreeColonistsSpawnedCount;
			bool flag = !FactionUtility.HostileTo(faction, faction2) && map.resourceCounter.TotalHumanEdibleNutrition < 4f * (float)freeColonistsSpawnedCount;
			bool result;
			if (flag)
			{
				Thing thing = ThingMaker.MakeThing(ThingDef.Named("MealSimple"), null);
				Thing thing2 = ThingMaker.MakeThing(ThingDef.Named("MealFine"), null);
				Thing thing3 = ThingMaker.MakeThing(ThingDef.Named("MealSimple"), null);
				Thing thing4 = ThingMaker.MakeThing(ThingDef.Named("MealFine"), null);
				List<Thing> list = new List<Thing>();
				int num = Rand.Range(20, 40);
				bool flag2 = num > thing.def.stackLimit;
				if (flag2)
				{
					num = thing.def.stackLimit;
				}
				bool flag3 = num == 0;
				if (flag3)
				{
					num = 1;
				}
				int num2 = Rand.Range(20, 40);
				bool flag4 = num2 > thing2.def.stackLimit;
				if (flag4)
				{
					num2 = thing2.def.stackLimit;
				}
				bool flag5 = num2 == 0;
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
				IntVec3 intVec = DropCellFinder.RandomDropSpot(map);
				DropPodUtility.DropThingsNear(intVec, map, list, 110, false, false, true);
				Find.LetterStack.ReceiveLetter("MO_Thanksgiving".Translate(), "MO_ThanksgivingDesc".Translate() , LetterDefOf.PositiveEvent, new TargetInfo(intVec, map, false), null); //"One of the tribes has noticed your food stores are low. They have provided you with some food, no strings attached.\n\nThey thank you for being such good friends and they hope your friendship will be further cemented by this act."
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


using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MoreIncidents
{
	public class MOIncidentWorker_SurvivalPod : IncidentWorker
	{
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
			Thing item = ThingMaker.MakeThing(ThingDef.Named("Apparel_Pants"), ThingDefOf.Hyperweave);
			Thing item2 = ThingMaker.MakeThing(ThingDef.Named("Apparel_BasicShirt"), ThingDefOf.Hyperweave);
			Thing item3 = ThingMaker.MakeThing(ThingDef.Named("Apparel_Jacket"), ThingDefOf.Hyperweave);
			Thing item4 = ThingMaker.MakeThing(ThingDef.Named("Apparel_Tuque"), ThingDefOf.Hyperweave);
			Thing item5 = ThingMaker.MakeThing(ThingDef.Named("MealSurvivalPack"), null);
			Thing item6 = ThingMaker.MakeThing(ThingDef.Named("MealSurvivalPack"), null);
			Thing item7 = ThingMaker.MakeThing(ThingDef.Named("MealSurvivalPack"), null);
			Thing item8 = ThingMaker.MakeThing(ThingDef.Named("MealSurvivalPack"), null);
			Thing item9 = ThingMaker.MakeThing(ThingDef.Named("Gun_Autopistol"), null);
			List<Thing> list = new List<Thing>();
			list.Add(item);
			list.Add(item2);
			list.Add(item3);
			list.Add(item4);
			list.Add(item5);
			list.Add(item6);
			list.Add(item7);
			list.Add(item8);
			list.Add(item9);
			IntVec3 intVec = DropCellFinder.RandomDropSpot(map);
			DropPodUtility.DropThingsNear(intVec, map, list, 110, false, false, true);
			Find.LetterStack.ReceiveLetter("MO_SurvivalPods".Translate(), "MO_SurvivalPodsDesc".Translate(), LetterDefOf.PositiveEvent, new TargetInfo(intVec, map, false), null); //"A cluster of survival pods landed nearby, you will find some essential surviving items in there."
			return true;
		}
	}
}


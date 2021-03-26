using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MoreIncidents
{
    public class MOIncidentWorker_SurvivalPod : IncidentWorker
    {
        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            var map = (Map) parms.target;
            var item = ThingMaker.MakeThing(ThingDef.Named("Apparel_Pants"), ThingDefOf.Hyperweave);
            var item2 = ThingMaker.MakeThing(ThingDef.Named("Apparel_BasicShirt"), ThingDefOf.Hyperweave);
            var item3 = ThingMaker.MakeThing(ThingDef.Named("Apparel_Jacket"), ThingDefOf.Hyperweave);
            var item4 = ThingMaker.MakeThing(ThingDef.Named("Apparel_Tuque"), ThingDefOf.Hyperweave);
            var item5 = ThingMaker.MakeThing(ThingDef.Named("MealSurvivalPack"));
            var item6 = ThingMaker.MakeThing(ThingDef.Named("MealSurvivalPack"));
            var item7 = ThingMaker.MakeThing(ThingDef.Named("MealSurvivalPack"));
            var item8 = ThingMaker.MakeThing(ThingDef.Named("MealSurvivalPack"));
            var item9 = ThingMaker.MakeThing(ThingDef.Named("Gun_Autopistol"));
            var list = new List<Thing>
            {
                item,
                item2,
                item3,
                item4,
                item5,
                item6,
                item7,
                item8,
                item9
            };
            var intVec = DropCellFinder.RandomDropSpot(map);
            DropPodUtility.DropThingsNear(intVec, map, list);
            Find.LetterStack.ReceiveLetter("MO_SurvivalPods".Translate(), "MO_SurvivalPodsDesc".Translate(),
                LetterDefOf.PositiveEvent,
                new TargetInfo(intVec,
                    map)); //"A cluster of survival pods landed nearby, you will find some essential surviving items in there."
            return true;
        }
    }
}
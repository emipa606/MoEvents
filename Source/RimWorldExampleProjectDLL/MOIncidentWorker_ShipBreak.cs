using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreIncidents;

public class MOIncidentWorker_ShipBreak : IncidentWorker
{
    private const int MaxStacks = 25;

    private static ThingDef RandomPodContentsDef()
    {
        bool IsLeather(ThingDef d)
        {
            return d.category == ThingCategory.Item && d.thingCategories != null &&
                   d.thingCategories.Contains(ThingCategoryDefOf.Leathers);
        }

        bool IsMeat(ThingDef d)
        {
            return d.category == ThingCategory.Item && d.thingCategories != null &&
                   d.thingCategories.Contains(ThingCategoryDefOf.MeatRaw);
        }

        var numLeathers = DefDatabase<ThingDef>.AllDefs.Where(IsLeather).Count();
        var numMeats = DefDatabase<ThingDef>.AllDefs.Where(IsMeat).Count();
        var allDefs = from d in DefDatabase<ThingDef>.AllDefs
            where d.category == ThingCategory.Item && d.tradeability == Tradeability.Buyable &&
                  d.equipmentType == EquipmentType.None && d.BaseMarketValue is >= 20f and < 200f &&
                  !d.HasComp(typeof(CompHatcher))
            select d;
        allDefs.TryRandomElementByWeight(delegate(ThingDef d)
        {
            var num = 100f;
            if (IsLeather(d))
            {
                num *= 5f / numLeathers;
            }

            if (IsMeat(d))
            {
                num *= 5f / numMeats;
            }

            return num;
        }, out var returnValue);
        return returnValue;
    }

    protected override bool TryExecuteWorker(IncidentParms parms)
    {
        var map = (Map)parms.target;
        var thingDef = RandomPodContentsDef();
        var list = new List<Thing>();
        float num = Rand.Range(150, 900);
        do
        {
            var thing = ThingMaker.MakeThing(thingDef);
            var num2 = Rand.Range(20, 40);
            if (num2 > thing.def.stackLimit)
            {
                num2 = thing.def.stackLimit;
            }

            if (num2 * thing.def.BaseMarketValue > num)
            {
                num2 = Mathf.FloorToInt(num / thing.def.BaseMarketValue);
            }

            if (num2 == 0)
            {
                num2 = 1;
            }

            thing.stackCount = num2;
            list.Add(thing);
            num -= num2 * thingDef.BaseMarketValue;
        } while (list.Count < MaxStacks && num > thingDef.BaseMarketValue);


        var intVec = DropCellFinder.RandomDropSpot(map);
        var intVec2 = DropCellFinder.RandomDropSpot(map);
        var intVec3 = DropCellFinder.RandomDropSpot(map);
        var faction = Find.FactionManager.RandomNonHostileFaction(true, true, true, TechLevel.Spacer);
        var pawnGenerationRequest =
            new PawnGenerationRequest(PawnKindDefOf.SpaceRefugee, faction, PawnGenerationContext.NonPlayer, -1,
                true, false, false, false, true, 1f, true, true, true, false);
        var pawnGenerationRequest2 = new PawnGenerationRequest(PawnKindDefOf.SpaceRefugee, faction,
            PawnGenerationContext.NonPlayer, -1, true, false, false, false, true, 1f, true, true, true,
            false);
        var pawn = PawnGenerator.GeneratePawn(pawnGenerationRequest);
        var pawn2 = PawnGenerator.GeneratePawn(pawnGenerationRequest2);

        HealthUtility.DamageUntilDowned(pawn);
        var corpse = (Corpse)ThingMaker.MakeThing(pawn2.RaceProps.corpseDef);
        corpse.InnerPawn = pawn2;
        HealthUtility.DamageUntilDead(pawn2);
        DropPodUtility.MakeDropPodAt(intVec, map, new ActiveDropPodInfo
        {
            SingleContainedThing = pawn,
            openDelay = 180,
            leaveSlag = true
        });
        DropPodUtility.MakeDropPodAt(intVec2, map, new ActiveDropPodInfo
        {
            SingleContainedThing = corpse,
            openDelay = 180,
            leaveSlag = true
        });

        DropPodUtility.DropThingsNear(intVec3, map, list, 110, false, true);
        Find.LetterStack.ReceiveLetter("MO_CargoRain".Translate(), "MO_CargoRainDesc".Translate(),
            LetterDefOf.PositiveEvent,
            new TargetInfo(intVec,
                map));
        return true;
    }

    public static void DebugLogPodContentsChoices()
    {
        var stringBuilder = new StringBuilder();
        int num;
        for (var i = 0; i < 100; i = num + 1)
        {
            stringBuilder.AppendLine(RandomPodContentsDef().LabelCap);
            num = i;
        }

        Log.Message(stringBuilder.ToString());
    }
}
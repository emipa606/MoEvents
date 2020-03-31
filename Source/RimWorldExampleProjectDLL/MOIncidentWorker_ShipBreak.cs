using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreIncidents
{
	public class MOIncidentWorker_ShipBreak : IncidentWorker
	{
		private static ThingDef RandomPodContentsDef()
		{
			Func<ThingDef, bool> isLeather = (ThingDef d) => d.category == ThingCategory.Item && d.thingCategories != null && d.thingCategories.Contains(ThingCategoryDefOf.Leathers);
			Func<ThingDef, bool> isMeat = (ThingDef d) => d.category == ThingCategory.Item && d.thingCategories != null && d.thingCategories.Contains(ThingCategoryDefOf.MeatRaw);
			int numLeathers = DefDatabase<ThingDef>.AllDefs.Where(isLeather).Count<ThingDef>();
			int numMeats = DefDatabase<ThingDef>.AllDefs.Where(isMeat).Count<ThingDef>();
			return GenCollection.RandomElementByWeight<ThingDef>(from d in DefDatabase<ThingDef>.AllDefs
			where d.category == ThingCategory.Item && d.tradeability == Tradeability.Buyable && d.equipmentType == EquipmentType.None && d.BaseMarketValue >= 20f && d.BaseMarketValue < 200f && !d.HasComp(typeof(CompHatcher))
			select d, delegate(ThingDef d)
			{
				float num = 100f;
				bool flag = isLeather(d);
				if (flag)
				{
					num *= 5f / (float)numLeathers;
				}
				bool flag2 = isMeat(d);
				if (flag2)
				{
					num *= 5f / (float)numMeats;
				}
				return num;
			});
		}

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
			ThingDef thingDef = MOIncidentWorker_ShipBreak.RandomPodContentsDef();
			List<Thing> list = new List<Thing>();
			List<Thing> list2 = new List<Thing>();
			float num = (float)Rand.Range(150, 900);
			do
			{
				Thing thing = ThingMaker.MakeThing(thingDef, null);
				int num2 = Rand.Range(20, 40);
				bool flag = num2 > thing.def.stackLimit;
				if (flag)
				{
					num2 = thing.def.stackLimit;
				}
				bool flag2 = (float)num2 * thing.def.BaseMarketValue > num;
				if (flag2)
				{
					num2 = Mathf.FloorToInt(num / thing.def.BaseMarketValue);
				}
				bool flag3 = num2 == 0;
				if (flag3)
				{
					num2 = 1;
				}
				thing.stackCount = num2;
				list.Add(thing);
				list2.Add(thing);
				num -= (float)num2 * thingDef.BaseMarketValue;
			}
			while (list.Count < 25 && num > thingDef.BaseMarketValue);
			while (list2.Count < 25 && num > thingDef.BaseMarketValue)
			{
			}
			IntVec3 intVec = DropCellFinder.RandomDropSpot(map);
			IntVec3 intVec2 = DropCellFinder.RandomDropSpot(map);
			IntVec3 intVec3 = DropCellFinder.RandomDropSpot(map);
            Faction faction = Find.FactionManager.RandomNonHostileFaction(true, true, true, TechLevel.Spacer);
            PawnGenerationRequest pawnGenerationRequest =
                            new PawnGenerationRequest(PawnKindDefOf.SpaceRefugee, faction, PawnGenerationContext.NonPlayer, -1, true, false, false, false, true, false, 1f, true, true, true, false, false, false, false, false, 0f,
null, 1f, null, null, null, null, null, null);
			PawnGenerationRequest pawnGenerationRequest2 = new PawnGenerationRequest(PawnKindDefOf.SpaceRefugee, faction, PawnGenerationContext.NonPlayer, -1, true, false, false, false, true, false, 1f, true, true, true, false, false, false, false, false, 0f,
null, 1f, null, null, null, null, null, null);
            PawnGenerationRequest pawnGenerationRequest3 = new PawnGenerationRequest(PawnKindDefOf.SpaceRefugee, faction, PawnGenerationContext.NonPlayer, -1, true, false, false, false, true, false, 1f, true, true, true, false, false, false, false, false, 0f,
null, 1f, null, null, null, null, null, null);
            Pawn pawn = PawnGenerator.GeneratePawn(pawnGenerationRequest);
			Pawn pawn2 = PawnGenerator.GeneratePawn(pawnGenerationRequest2);
			Pawn pawn3 = PawnGenerator.GeneratePawn(pawnGenerationRequest3);
			HealthUtility.DamageUntilDowned(pawn);
			HealthUtility.DamageUntilDead(pawn2);
			HealthUtility.DamageUntilDowned(pawn3);
			DropPodUtility.MakeDropPodAt(intVec, map, new ActiveDropPodInfo
			{
				SingleContainedThing = pawn,
				openDelay = 180,
				leaveSlag = true
			});
			DropPodUtility.MakeDropPodAt(intVec, map, new ActiveDropPodInfo
			{
				SingleContainedThing = pawn2,
				openDelay = 180,
				leaveSlag = true
			});
			DropPodUtility.DropThingsNear(intVec, map, list, 110, false, true, true);
			Find.LetterStack.ReceiveLetter("MO_CargoRain".Translate(), "MO_CargoRainDesc".Translate(), LetterDefOf.PositiveEvent, new TargetInfo(intVec, map, false), null); //"A chunk of your crashed ship fell into the atmosphere, watch out for falling cargo and rescue pods.\n\nIt looks like this chunk came from one of the richer parts of the ship."
			return true;
		}

		public static void DebugLogPodContentsChoices()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			for (int i = 0; i < 100; i = num + 1)
			{
				stringBuilder.AppendLine(MOIncidentWorker_ShipBreak.RandomPodContentsDef().LabelCap);
				num = i;
			}
			Log.Message(stringBuilder.ToString());
		}

		private const int MaxStacks = 25;

		private const float MaxMarketValue = 100000f;
	}
}


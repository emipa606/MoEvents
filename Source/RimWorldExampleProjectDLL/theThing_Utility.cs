using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreIncidents
{
	public static class theThing_Utility
	{
		public static IEnumerable<Thing> ButcherCorpseProducts(Corpse corpse, Pawn butcher)
		{
			bool flag = corpse.def.butcherProducts != null;
			bool flag6 = flag;
			if (flag6)
			{
				IEnumerator<Thing> enumerator = corpse.InnerPawn.ButcherProducts(butcher, 1f).GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Thing thing2 = enumerator.Current;
						yield return thing2;
					}
				}
				finally
				{
					enumerator.Dispose();
				}
				enumerator = null;
				enumerator = null;
			}
			else
			{
				bool isFlesh = corpse.InnerPawn.RaceProps.IsFlesh;
				bool flag7 = isFlesh;
				if (flag7)
				{
					FilthMaker.TryMakeFilth(butcher.Position, butcher.Map, ThingDefOf.Filth_Blood, corpse.InnerPawn.LabelCap, 1);
				}
			}
			bool flag2 = corpse.InnerPawn.RaceProps.meatDef != null;
			bool flag8 = flag2;
			if (flag8)
			{
				FilthMaker.TryMakeFilth(butcher.Position, butcher.Map, ThingDefOf.Filth_Blood, corpse.InnerPawn.LabelCap, 1);
				int num = GenMath.RoundRandom(StatExtension.GetStatValue(corpse.InnerPawn, StatDefOf.MeatAmount, true) * 0f);
			}
			bool flag3 = corpse.InnerPawn.def.race.leatherDef != null;
			bool flag9 = flag3;
			if (flag9)
			{
				int num2 = GenMath.RoundRandom(StatExtension.GetStatValue(corpse.InnerPawn, StatDefOf.LeatherAmount, true) * 0f);
				bool flag4 = num2 > 0;
				bool flag10 = flag4;
				if (flag10)
				{
					Thing thing = ThingMaker.MakeThing(corpse.InnerPawn.def.race.leatherDef, null);
					bool flag5 = thing != null;
					bool flag11 = flag5;
					if (flag11)
					{
						thing.stackCount = num2;
						yield return thing;
					}
					thing = null;
					thing = null;
				}
			}
			yield break;
		}

		public static void StripDeteriorate(this Corpse corpse)
		{
			foreach (Apparel apparel in corpse.InnerPawn.apparel.WornApparel)
			{
				int num = Mathf.RoundToInt((float)apparel.HitPoints * Rand.Range(0.25f, 0.75f));
				apparel.TakeDamage(new DamageInfo(DamageDefOf.Deterioration, num, 0f, -1f, null, null, null));
			}
			corpse.Strip();
		}
	}
}


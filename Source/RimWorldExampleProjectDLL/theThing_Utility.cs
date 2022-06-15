using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreIncidents;

public static class theThing_Utility
{
    public static IEnumerable<Thing> ButcherCorpseProducts(Corpse corpse, Pawn butcher)
    {
        if (corpse.def.butcherProducts != null)
        {
            var enumerator = corpse.InnerPawn.ButcherProducts(butcher, 1f).GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var thing2 = enumerator.Current;
                    yield return thing2;
                }
            }
            finally
            {
                enumerator.Dispose();
            }
        }
        else
        {
            if (corpse.InnerPawn.RaceProps.IsFlesh)
            {
                FilthMaker.TryMakeFilth(butcher.Position, butcher.Map, ThingDefOf.Filth_Blood,
                    corpse.InnerPawn.LabelCap);
            }
        }

        if (corpse.InnerPawn.RaceProps.meatDef != null)
        {
            FilthMaker.TryMakeFilth(butcher.Position, butcher.Map, ThingDefOf.Filth_Blood,
                corpse.InnerPawn.LabelCap);
            var num = GenMath.RoundRandom(corpse.InnerPawn.GetStatValue(StatDefOf.MeatAmount) * 0f);
            if (num > 0)
            {
                var thing = ThingMaker.MakeThing(corpse.InnerPawn.def.race.meatDef);
                if (thing != null)
                {
                    thing.stackCount = num;
                    yield return thing;
                }
            }
        }

        if (corpse.InnerPawn.def.race.leatherDef == null)
        {
            yield break;
        }

        {
            var num2 = GenMath.RoundRandom(corpse.InnerPawn.GetStatValue(StatDefOf.LeatherAmount) * 0f);
            if (num2 <= 0)
            {
                yield break;
            }

            var thing = ThingMaker.MakeThing(corpse.InnerPawn.def.race.leatherDef);
            if (thing == null)
            {
                yield break;
            }

            thing.stackCount = num2;
            yield return thing;
        }
    }

    public static void StripDeteriorate(this Corpse corpse)
    {
        foreach (var apparel in corpse.InnerPawn.apparel.WornApparel)
        {
            var num = Mathf.RoundToInt(apparel.HitPoints * Rand.Range(0.25f, 0.75f));
            apparel.TakeDamage(new DamageInfo(DamageDefOf.Deterioration, num));
        }

        corpse.Strip();
    }
}
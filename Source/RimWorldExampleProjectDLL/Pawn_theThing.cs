using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace MoreIncidents;

public class Pawn_theThing : Pawn
{
    private const int TicksBetweenRepairs = 15;

    private readonly int updateStatusEveryXTicks = 45;

    private int ticksSinceStatusUpdate;

    private int ticksToNextRepair;

    public override void Tick()
    {
        base.Tick();
        if (Find.TickManager.TicksGame % 250 == 0)
        {
            TickRare();
        }

        mindState.mentalStateHandler.neverFleeIndividual = true;

        if (health.hediffSet.HasTendableHediff())
        {
            TickHeal(true);
        }

        if (!Dead || !Downed)
        {
            var num = ticksSinceStatusUpdate;
            ticksSinceStatusUpdate = num + 1;
            if (ticksSinceStatusUpdate >= updateStatusEveryXTicks)
            {
                var intVec = CellFinder.RandomClosewalkCellNear(Position, Map, 1);
                var named = DefDatabase<ThingDef>.GetNamed("Filth_Blood");
                FilthMaker.TryMakeFilth(intVec, Map, named);
                ticksSinceStatusUpdate = 0;
            }
        }

        if (!Downed)
        {
            return;
        }

        var intVec2 = CellFinder.RandomClosewalkCellNear(Position, Map, 2);
        var named2 = DefDatabase<ThingDef>.GetNamed("Filth_Blood");
        FilthMaker.TryMakeFilth(intVec2, Map, named2, 3);
        HealthUtility.DamageUntilDead(this);
    }

    private void TickHeal(bool allowMiracles)
    {
        var num = ticksToNextRepair;
        ticksToNextRepair = num - 1;
        if (num <= 0)
        {
            return;
        }

        ticksToNextRepair = 15;
        if (allowMiracles)
        {
            def.repairEffect?.Spawn();
        }

        var hediffs = GetHediffs(allowMiracles);
        if (hediffs.Length != 0)
        {
            return;
        }

        var hediff = hediffs.RandomElement();
        if (!hediff.TendableNow())
        {
            return;
        }

        Mo_Utility.HealHediff(this, hediff, 1);
        if (allowMiracles && Rand.Value < 0.05f)
        {
            health.RemoveHediff(hediff);
        }
        else
        {
            Mo_Utility.HealHediff(this, hediff, 1);
        }
    }

    private Hediff[] GetHediffs(bool allowMiracles)
    {
        Hediff[] result;
        if (!allowMiracles)
        {
            IEnumerable<Hediff> hediffs = health.hediffSet.hediffs;
            Func<Hediff, bool> predicate;
            if ((predicate = thing.isInjury) == null)
            {
                predicate = thing.isInjury = thing.IncidentThing.getHediffsInjury;
            }

            result = hediffs.Where(predicate).ToArray();
        }
        else
        {
            IEnumerable<Hediff> hediffs2 = health.hediffSet.hediffs;
            Func<Hediff, bool> predicate2;
            if ((predicate2 = thing.isHeal) == null)
            {
                predicate2 = thing.isHeal = thing.IncidentThing.getHediffsHeal;
            }

            result = hediffs2.Where(predicate2).ToArray();
        }

        return result;
    }

    private sealed class thing
    {
        public static readonly thing IncidentThing = new thing();

        public static Func<Hediff, bool> isInjury;

        public static Func<Hediff, bool> isHeal;

        public bool getHediffsInjury(Hediff h)
        {
            return h is Hediff_Injury;
        }

        public bool getHediffsHeal(Hediff h)
        {
            return h is not Hediff_AddedPart;
        }
    }
}
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace MoreIncidents;

public class JobDriver_Kill : JobDriver
{
    private int numMeleeAttacksLanded;

    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        //Log.Message("this " + this.pawn.Label + " try to reserve " + this.job.targetA.Label);
        return true;
/*
            return pawn.Reserve(job.targetA, job, 1, -1, null, errorOnFailed);
*/
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        var followAndAttack = new Toil();
        followAndAttack.tickAction = delegate
        {
            var actor = followAndAttack.actor;
            var curJob = actor.jobs.curJob;
            var thing = curJob.GetTarget(TargetIndex.A).Thing;
            var targetPawn = thing as Pawn;
            if (pawn.Faction != Faction.OfPlayer)
            {
                actor.mindState.mentalStateHandler.TryStartMentalState(
                    DefDatabase<MentalStateDef>.GetNamed("MurderousRage"));
            }

            if (thing != actor.pather.Destination.Thing ||
                !pawn.pather.Moving && !pawn.Position.AdjacentTo8WayOrInside(thing))
            {
                actor.pather.StartPath(thing, PathEndMode.Touch);
            }
            else
            {
                if (!pawn.Position.AdjacentTo8WayOrInside(thing))
                {
                    return;
                }

                if (thing is Pawn && targetPawn.Downed && !curJob.killIncappedTarget)
                {
                    EndJobWith(JobCondition.Succeeded);
                }

                if (!actor.meleeVerbs.TryMeleeAttack(thing))
                {
                    return;
                }

                numMeleeAttacksLanded += 1;
                if (numMeleeAttacksLanded >= curJob.maxNumMeleeAttacks)
                {
                    EndJobWith(JobCondition.Succeeded);
                }
            }
        };
        followAndAttack.defaultCompleteMode = ToilCompleteMode.Never;
        followAndAttack.EndOnDespawnedOrNull(TargetIndex.A, JobCondition.Succeeded);
        followAndAttack.FailOn(hunterIsKilled);
        yield return followAndAttack;
    }

    private bool hunterIsKilled()
    {
        return pawn.Dead || pawn.HitPoints == 0;
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref numMeleeAttacksLanded, "numMeleeAttacksMade");
    }
}
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace MoreIncidents;

public class JobDriver_CrushDoor : JobDriver
{
    private int numMeleeAttacksMade;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref numMeleeAttacksMade, "numMeleeAttacksMade");
    }

    public override bool TryMakePreToilReservations(bool errorOnFailed)
    {
        return pawn.Reserve(job.targetA, job, 1, -1, null, errorOnFailed);
    }

    protected override IEnumerable<Toil> MakeNewToils()
    {
        var toil = Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.Touch);
        toil.FailOnDestroyedOrNull(TargetIndex.B);
        yield return toil;
        yield return BashIt();
    }

    private Toil BashIt()
    {
        var bashIt = new Toil();
        bashIt.tickAction = delegate
        {
            var actor = bashIt.actor;
            var curJob = actor.jobs.curJob;
            var thing = curJob.GetTarget(TargetIndex.B).Thing;
            if (!actor.meleeVerbs.TryMeleeAttack(thing))
            {
                return;
            }

            numMeleeAttacksMade++;
            if (numMeleeAttacksMade >= curJob.maxNumMeleeAttacks)
            {
                EndJobWith(JobCondition.Succeeded);
            }
        };
        bashIt.defaultCompleteMode = ToilCompleteMode.Never;
        bashIt.EndOnDespawnedOrNull(TargetIndex.B, JobCondition.Succeeded);
        bashIt.FailOn(hunterIsKilled);
        return bashIt;
    }

    private bool hunterIsKilled()
    {
        return pawn.Dead || pawn.HitPoints == 0;
    }
}
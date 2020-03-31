using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace MoreIncidents
{
	public class JobDriver_CrushDoor : JobDriver
	{
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.numMeleeAttacksMade, "numMeleeAttacksMade", 0, false);
        }
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return this.pawn.Reserve(this.job.targetA, this.job, 1, -1, null, errorOnFailed);
        }
        protected override IEnumerable<Toil> MakeNewToils()
		{
			Toil toil = Toils_Goto.GotoCell(TargetIndex.A, PathEndMode.Touch);
			ToilFailConditions.FailOnDestroyedOrNull<Toil>(toil, TargetIndex.B);
			yield return toil;
			yield return this.BashIt();
			yield break;
		}

		public Toil BashIt()
		{
			Toil bashIt = new Toil();
			bashIt.tickAction = delegate()
			{
				Pawn actor = bashIt.actor;
				Job curJob = actor.jobs.curJob;
				Thing thing = curJob.GetTarget(TargetIndex.B).Thing;
				bool flag = actor.meleeVerbs.TryMeleeAttack(thing, null, false);
				bool flag2 = flag;
				if (flag2)
				{
					this.numMeleeAttacksMade++;
					bool flag3 = this.numMeleeAttacksMade >= curJob.maxNumMeleeAttacks;
					bool flag4 = flag3;
					if (flag4)
					{
						this.EndJobWith(JobCondition.Succeeded);
					}
				}
			};
			bashIt.defaultCompleteMode = ToilCompleteMode.Never;
			ToilFailConditions.EndOnDespawnedOrNull<Toil>(bashIt, TargetIndex.B, JobCondition.Succeeded);
			ToilFailConditions.FailOn<Toil>(bashIt, new Func<bool>(this.hunterIsKilled));
			return bashIt;
		}

		private bool hunterIsKilled()
		{
			return this.pawn.Dead || this.pawn.HitPoints == 0;
		}

		private int numMeleeAttacksMade;
	}
}


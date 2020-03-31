using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace MoreIncidents
{
	public class JobDriver_Kill : JobDriver
	{
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            //Log.Message("this " + this.pawn.Label + " try to reserve " + this.job.targetA.Label);
            return true;
            return this.pawn.Reserve(this.job.targetA, this.job, 1, -1, null, errorOnFailed);
        }
        protected override IEnumerable<Toil> MakeNewToils()
		{
			Toil followAndAttack = new Toil();
			followAndAttack.tickAction = delegate()
			{
				Pawn actor = followAndAttack.actor;
				Job curJob = actor.jobs.curJob;
				Thing thing = curJob.GetTarget(TargetIndex.A).Thing;
				Pawn pawn = thing as Pawn;
				bool flag = this.pawn.Faction != Faction.OfPlayer;
				bool flag2 = flag;
				if (flag2)
				{
					actor.mindState.mentalStateHandler.TryStartMentalState(DefDatabase<MentalStateDef>.GetNamed("MurderousRage", true), null, false, false, null);
				}
				bool flag3 = thing != actor.pather.Destination.Thing || (!this.pawn.pather.Moving && !GenAdj.AdjacentTo8WayOrInside(this.pawn.Position, thing));
				bool flag4 = flag3;
				if (flag4)
				{
					actor.pather.StartPath(thing, PathEndMode.Touch);
				}
				else
				{
					bool flag5 = GenAdj.AdjacentTo8WayOrInside(this.pawn.Position, thing);
					bool flag6 = flag5;
					if (flag6)
					{
						bool flag7 = thing is Pawn && pawn.Downed && !curJob.killIncappedTarget;
						bool flag8 = flag7;
						if (flag8)
						{
							this.EndJobWith(JobCondition.Succeeded);
						}
						bool flag9 = actor.meleeVerbs.TryMeleeAttack(thing, null, false);
						bool flag10 = flag9;
						if (flag10)
						{
                            this.numMeleeAttacksLanded += 1;
							bool flag11 = this.numMeleeAttacksLanded >= curJob.maxNumMeleeAttacks;
							bool flag12 = flag11;
							if (flag12)
							{
								this.EndJobWith(JobCondition.Succeeded);
							}
						}
					}
				}
			};
			followAndAttack.defaultCompleteMode = ToilCompleteMode.Never;
			ToilFailConditions.EndOnDespawnedOrNull<Toil>(followAndAttack, TargetIndex.A, JobCondition.Succeeded);
			ToilFailConditions.FailOn<Toil>(followAndAttack, new Func<bool>(this.hunterIsKilled));
			yield return followAndAttack;
			yield break;
		}

		private bool hunterIsKilled()
		{
			return this.pawn.Dead || this.pawn.HitPoints == 0;
		}

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.numMeleeAttacksLanded, "numMeleeAttacksMade", 0, false);
        }

        private int numMeleeAttacksLanded;
	}
}


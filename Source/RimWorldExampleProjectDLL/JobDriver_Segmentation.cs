using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace MoreIncidents
{
	public class JobDriver_Segmentation : JobDriver
	{
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return this.pawn.Reserve(this.job.targetA, this.job, 1, -1, null, errorOnFailed);
        }
        protected override IEnumerable<Toil> MakeNewToils()
		{
			ToilFailConditions.FailOnDestroyedOrNull<JobDriver_Segmentation>(this, TargetIndex.A);
			ToilFailConditions.FailOnDespawnedOrNull<JobDriver_Segmentation>(this, TargetIndex.A);
			ToilFailConditions.FailOn<JobDriver_Segmentation>(this, new Func<bool>(this.eaterIsKilled));
			Toil resCorpse = new Toil();
			resCorpse.initAction = delegate()
			{
				Pawn actor = resCorpse.actor;
				Thing thing = resCorpse.actor.CurJob.GetTarget(TargetIndex.A).Thing;
				bool flag = !thing.Spawned || !this.Map.reservationManager.CanReserve(actor, thing, 1);
				bool flag2 = flag;
				if (flag2)
				{
					actor.jobs.EndCurrentJob(JobCondition.Incompletable, true);
				}
				else
				{
					this.Map.reservationManager.Reserve(actor, actor.CurJob, thing, 1);
				}
			};
			resCorpse.defaultCompleteMode = ToilCompleteMode.Instant;
			yield return resCorpse;
			yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
			yield return Toils_General.Wait(400);
			Toil toil = new Toil
			{
				defaultCompleteMode = ToilCompleteMode.Instant
            };
			toil.initAction = new Action(this.doStripCorpse);
			yield return toil;
			yield return Toils_General.Wait(60);
			Toil toil2 = new Toil
			{
				defaultCompleteMode = ToilCompleteMode.Instant
            };
			toil2.initAction = new Action(this.doChewCorpse);
			ToilEffects.WithEffect(toil2, EffecterDefOf.EatMeat, TargetIndex.A);
			ToilFailConditions.EndOnDespawnedOrNull<Toil>(toil2, TargetIndex.A, JobCondition.Incompletable);
			yield return toil2;
			yield break;
		}

		private bool eaterIsKilled()
		{
			return this.pawn.Dead || this.pawn.Downed || this.pawn.HitPoints == 0;
		}

		private void doStripCorpse()
		{
			Thing thing = this.pawn.CurJob.targetA.Thing;
			Corpse corpse = thing as Corpse;
			bool flag = corpse != null && corpse.AnythingToStrip();
			bool flag2 = flag;
			if (flag2)
			{
				corpse.Strip();
			}
		}

		private void doChewCorpse()
		{
			Corpse corpse = base.TargetThingA as Corpse;
			bool flag = corpse != null;
			bool flag2 = flag;
			if (flag2)
			{
				IntVec3 position = corpse.Position;
				List<Thing> list = theThing_Utility.ButcherCorpseProducts(corpse, this.pawn).ToList<Thing>();
				Thing thing = null;
				int num;
				for (int i = 0; i < list.Count; i = num + 1)
				{
					bool flag3 = !GenPlace.TryPlaceThing(list[i], position, base.Map, ThingPlaceMode.Near, out thing, null);
					bool flag4 = flag3;
					if (flag4)
					{
						this.pawn.jobs.EndCurrentJob(JobCondition.Incompletable, true);
					}
					bool flag5 = thing != null;
					bool flag6 = flag5;
					if (flag6)
					{
						ForbidUtility.SetForbidden(thing, true, true);
					}
					num = i;
				}
				bool flag7 = this.pawn.needs.mood != null;
				bool flag8 = flag7;
				if (flag8)
				{
					this.pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("AteCorpse"), null);
				}
				corpse.Destroy(0);
				IEnumerable<Faction> allFactions = Find.FactionManager.AllFactions;
				Func<Faction, bool> predicate;
				bool flag9 = (predicate = JobDriver_Segmentation.segment.letSegment) == null;
				if (flag9)
				{
					predicate = (JobDriver_Segmentation.segment.letSegment = new Func<Faction, bool>(JobDriver_Segmentation.segment.JobSegment.doChewCorpse));
				}
				Faction faction = GenCollection.RandomElement<Faction>(allFactions.Where(predicate));
				this.abom = PawnGenerator.GeneratePawn(MODefOf.MO_AbominationPawnKind, faction);
				GenSpawn.Spawn(this.abom, position, base.Map);
			}
			this.pawn.jobs.EndCurrentJob(JobCondition.Succeeded, true);
		}

		private Pawn abom;

		private sealed class segment
		{
			public bool doChewCorpse(Faction fac)
			{
				return fac.def.defName == "MO_AbominationFaction";
			}

			public static readonly JobDriver_Segmentation.segment JobSegment = new JobDriver_Segmentation.segment();

			public static Func<Faction, bool> letSegment;
		}
	}
}


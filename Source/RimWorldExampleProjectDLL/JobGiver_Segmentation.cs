using System;
using Verse;
using Verse.AI;

namespace MoreIncidents
{
	public class JobGiver_Segmentation : ThinkNode_JobGiver
	{
		protected override Job TryGiveJob(Pawn pawn)
		{
			IntVec3 position = pawn.Position;
			bool flag = !pawn.Map.fogGrid.IsFogged(position);
			bool flag2 = flag;
			Job result;
			if (flag2)
			{
				TraverseParms traverseParams = TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, true);
				JobDef named = DefDatabase<JobDef>.GetNamed("MO_Segmentation", true);
				bool flag3 = pawn.jobs.curJob == null || (pawn.jobs.curJob.def != named && pawn.jobs.curJob.checkOverrideOnExpire);
				bool flag4 = flag3;
				if (flag4)
				{
					Corpse corpse = JobGiver_Segmentation.FindMeatyCorpse(pawn, traverseParams);
					bool flag5 = corpse != null;
					bool flag6 = flag5;
					if (flag6)
					{
						bool humanlike = corpse.InnerPawn.def.race.Humanlike;
						bool flag7 = humanlike;
						if (flag7)
						{
							return new Job(named, corpse);
						}
					}
				}
				result = null;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static Pawn FindMeatyPrey(Pawn pawn, TraverseParms traverseParams)
		{
			ThingRequest thingRequest = ThingRequest.ForGroup(ThingRequestGroup.Pawn);
			Predicate<Thing> predicate = delegate(Thing p)
			{
				Pawn prey = p as Pawn;
				return JobGiver_Segmentation.isPossiblePrey(prey, pawn);
			};
			return GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, thingRequest, PathEndMode.Touch, traverseParams, 100f, predicate, null, -1) as Pawn;
		}

		public static Corpse FindMeatyCorpse(Pawn pawn, TraverseParms traverseParams)
		{
			ThingRequest thingRequest = ThingRequest.ForGroup(ThingRequestGroup.Corpse);
			Predicate<Thing> predicate = (Thing corpse) => pawn.Map.reservationManager.CanReserve(pawn, corpse, 1);
			return GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, thingRequest, PathEndMode.Touch, traverseParams, 100f, predicate, null, -1) as Corpse;
		}

		private static bool isPossiblePrey(Pawn prey, Pawn hunter)
		{
			return hunter != prey && JobGiver_Segmentation.isNearby(hunter, prey);
		}

		private static bool isNearby(Pawn pawn, Thing thing)
		{
			return (pawn.Position - thing.Position).LengthHorizontalSquared <= 225f;
		}

		public const int SEARCH_DISTANCE = 225;
	} 
}


using Verse;
using Verse.AI;

namespace MoreIncidents;

public class JobGiver_Segmentation : ThinkNode_JobGiver
{
    public const int SEARCH_DISTANCE = 225;

    protected override Job TryGiveJob(Pawn pawn)
    {
        var position = pawn.Position;
        if (pawn.Map.fogGrid.IsFogged(position))
        {
            return null;
        }

        var traverseParams = TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, true);
        var named = DefDatabase<JobDef>.GetNamed("MO_Segmentation");
        if (pawn.jobs.curJob != null &&
            (pawn.jobs.curJob.def == named || !pawn.jobs.curJob.checkOverrideOnExpire))
        {
            return null;
        }

        var corpse = FindMeatyCorpse(pawn, traverseParams);
        if (corpse == null)
        {
            return null;
        }

        return corpse.InnerPawn.def.race.Humanlike ? new Job(named, corpse) : null;
    }

    public static Pawn FindMeatyPrey(Pawn pawn, TraverseParms traverseParams)
    {
        var thingRequest = ThingRequest.ForGroup(ThingRequestGroup.Pawn);

        return GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, thingRequest, PathEndMode.Touch,
            traverseParams, 100f, Predicate, null, -1) as Pawn;

        bool Predicate(Thing p)
        {
            var prey = p as Pawn;
            return isPossiblePrey(prey, pawn);
        }
    }

    private static Corpse FindMeatyCorpse(Pawn pawn, TraverseParms traverseParams)
    {
        var thingRequest = ThingRequest.ForGroup(ThingRequestGroup.Corpse);

        return GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, thingRequest, PathEndMode.Touch,
            traverseParams, 100f, Predicate, null, -1) as Corpse;

        bool Predicate(Thing corpse)
        {
            return pawn.Map.reservationManager.CanReserve(pawn, corpse);
        }
    }

    private static bool isPossiblePrey(Pawn prey, Pawn hunter)
    {
        return hunter != prey && isNearby(hunter, prey);
    }

    private static bool isNearby(Pawn pawn, Thing thing)
    {
        return (pawn.Position - thing.Position).LengthHorizontalSquared <= 225f;
    }
}
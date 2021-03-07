using RimWorld;
using Verse;
using Verse.AI;

namespace MoreIncidents
{
    public class JobGiver_Kill : ThinkNode_JobGiver
    {
        public const int SEARCH_DISTANCE = 4225;

        protected override Job TryGiveJob(Pawn pawn)
        {
            var position = pawn.Position;
            if (pawn.Map.fogGrid.IsFogged(position))
            {
                return null;
            }

            var traverseParams = TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, true);
            var named = DefDatabase<JobDef>.GetNamed("MO_Kill");
            if (pawn.jobs.curJob != null &&
                (pawn.jobs.curJob.def == named || !pawn.jobs.curJob.checkOverrideOnExpire))
            {
                return null;
            }

            var pawn2 = FindMeatyPrey(pawn, traverseParams);
            if (pawn2 == null)
            {
                return null;
            }

            var pawnPath = pawn.Map.pathFinder.FindPath(pawn.Position, pawn2, TraverseParms.For(pawn));
            var building_Door = pawnPath.FirstBlockingBuilding(out var intVec, pawn) as Building_Door;
            pawnPath.ReleaseToPool();
            if (building_Door == null)
            {
                return new Job(named)
                {
                    targetA = pawn2,
                    maxNumMeleeAttacks = 4,
                    killIncappedTarget = true,
                    expiryInterval = 500
                };
            }

            if (!building_Door.Open)
            {
                return new Job(DefDatabase<JobDef>.GetNamed("MO_CrushDoor"), intVec, building_Door)
                {
                    maxNumMeleeAttacks = 4,
                    expiryInterval = 500
                };
            }

            // error
            return new Job(named)
            {
                targetA = pawn2,
                maxNumMeleeAttacks = 4,
                killIncappedTarget = true,
                expiryInterval = 500
            };
        }

        private static Pawn FindMeatyPrey(Pawn pawn, TraverseParms traverseParams)
        {
            var thingRequest = ThingRequest.ForGroup(ThingRequestGroup.Pawn);

            bool Predicate(Thing p)
            {
                var prey = p as Pawn;
                return isPossiblePrey(prey, pawn);
            }

            return GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, thingRequest, PathEndMode.Touch,
                traverseParams, 100f, Predicate, null, -1) as Pawn;
        }

        private static bool isPossiblePrey(Pawn prey, Pawn hunter)
        {
            return hunter != prey && !prey.Dead && !prey.Downed && isHumanlike(hunter, prey) &&
                   !isFriendly(hunter, prey) && !isAlly(hunter, prey) && !isMech(hunter, prey) &&
                   !isOwnRace(hunter, prey) && isNearby(hunter, prey);
        }

        private static bool isFriendly(Pawn hunter, Pawn prey)
        {
            bool result;
            if (hunter.Faction == Faction.OfPlayer)
            {
                if (prey == null)
                {
                    result = false;
                }
                else
                {
                    result = prey.Faction == Faction.OfPlayer || prey.Faction == Faction.OfPlayer ||
                             prey.IsPrisonerOfColony || prey.Faction.HostileTo(Faction.OfPlayer);
                }
            }
            else
            {
                result = prey.def == hunter.def;
            }

            return result;
        }

        private static bool isAlly(Pawn hunter, Pawn prey)
        {
            bool result;
            if (hunter.Faction != null)
            {
                if (hunter.Faction == prey.Faction)
                {
                    result = hunter.Faction == prey.Faction;
                }
                else
                {
                    result = prey.def == hunter.def;
                }
            }
            else
            {
                result = prey.def == hunter.def;
            }

            return result;
        }

        private static bool isMech(Pawn hunter, Pawn prey)
        {
            bool result;
            if (prey.Faction == Faction.OfMechanoids)
            {
                result = prey.Faction == Faction.OfMechanoids;
            }
            else
            {
                result = prey.def == hunter.def;
            }

            return result;
        }

        private static bool isOwnRace(Pawn hunter, Pawn prey)
        {
            bool result;
            if (hunter.RaceProps.Animal == prey.RaceProps.Animal)
            {
                result = prey.RaceProps.Animal != hunter.RaceProps.Animal;
            }
            else
            {
                result = prey.def == hunter.def;
            }

            return result;
        }

        private static bool isHumanlike(Pawn hunter, Pawn prey)
        {
            bool result;
            if (prey.RaceProps.Humanlike)
            {
                result = prey.RaceProps.Humanlike || prey.RaceProps.Humanlike;
            }
            else
            {
                result = prey.def == hunter.def;
            }

            return result;
        }

        private static bool isNearby(Pawn pawn, Thing thing)
        {
            return (pawn.Position - thing.Position).LengthHorizontalSquared <= 4225f;
        }
    }
}
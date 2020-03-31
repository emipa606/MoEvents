using System;
using RimWorld;
using Verse;
using Verse.AI;

namespace MoreIncidents
{
    public class JobGiver_Kill : ThinkNode_JobGiver
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
                JobDef named = DefDatabase<JobDef>.GetNamed("MO_Kill", true);
                bool flag3 = pawn.jobs.curJob == null || (pawn.jobs.curJob.def != named && pawn.jobs.curJob.checkOverrideOnExpire);
                bool flag4 = flag3;
                if (flag4) 
                {
                    Pawn pawn2 = JobGiver_Kill.FindMeatyPrey(pawn, traverseParams);
                    bool flag5 = pawn2 != null;
                    bool flag6 = flag5;
                    if (flag6)
                    {
                        PawnPath pawnPath = pawn.Map.pathFinder.FindPath(pawn.Position, pawn2, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), PathEndMode.OnCell);
                        IntVec3 intVec = new IntVec3();
                        Building_Door building_Door = PawnPathUtility.FirstBlockingBuilding(pawnPath, out intVec, pawn) as Building_Door;
                        pawnPath.ReleaseToPool();
                        bool flag7 = building_Door != null;
                        bool flag8 = flag7;
                        if (flag8)
                        {
                            bool flag9 = !building_Door.Open;
                            bool flag10 = flag9;
                            if (flag10)
                            {
                                return new Job(DefDatabase<JobDef>.GetNamed("MO_CrushDoor", true), intVec, building_Door)
                                {
                                    maxNumMeleeAttacks = 4,
                                    expiryInterval = 500
                                };
                            }
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
            Predicate<Thing> predicate = delegate (Thing p)
            {
                Pawn prey = p as Pawn;
                return JobGiver_Kill.isPossiblePrey(prey, pawn);
            };
            return GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, thingRequest, PathEndMode.Touch, traverseParams, 100f, predicate, null, -1) as Pawn;
        }

        private static bool isPossiblePrey(Pawn prey, Pawn hunter)
        {
            return hunter != prey && !prey.Dead && !prey.Downed && JobGiver_Kill.isHumanlike(hunter, prey) && !JobGiver_Kill.isFriendly(hunter, prey) && !JobGiver_Kill.isAlly(hunter, prey) && !JobGiver_Kill.isMech(hunter, prey) && !JobGiver_Kill.isOwnRace(hunter, prey) && JobGiver_Kill.isNearby(hunter, prey);
        }

        private static bool isFriendly(Pawn hunter, Pawn prey)
        {
            bool flag = hunter.Faction == Faction.OfPlayer;
            bool flag2 = flag;
            bool result;
            if (flag2)
            {
                bool flag3 = prey == null;
                bool flag4 = flag3;
                if (flag4)
                {
                    result = (prey.Faction == Faction.OfPlayer || prey.def == hunter.def || prey.IsPrisonerOfColony || FactionUtility.HostileTo(prey.Faction, Faction.OfPlayer));
                }
                else
                {
                    result = (prey.Faction == Faction.OfPlayer || prey.Faction == Faction.OfPlayer || prey.IsPrisonerOfColony || FactionUtility.HostileTo(prey.Faction, Faction.OfPlayer));
                }
            }
            else
            {
                result = (prey.def == hunter.def);
            }
            return result;
        }

        private static bool isAlly(Pawn hunter, Pawn prey)
        {
            bool flag = hunter.Faction != null;
            bool flag2 = flag;
            bool result;
            if (flag2)
            {
                bool flag3 = hunter.Faction == prey.Faction;
                bool flag4 = flag3;
                if (flag4)
                {
                    bool flag5 = prey == null;
                    bool flag6 = flag5;
                    if (flag6)
                    {
                        result = (prey.def != hunter.def);
                    }
                    else
                    {
                        result = (hunter.Faction == prey.Faction);
                    }
                }
                else
                {
                    result = (prey.def == hunter.def);
                }
            }
            else
            {
                result = (prey.def == hunter.def);
            }
            return result;
        }

        private static bool isMech(Pawn hunter, Pawn prey)
        {
            bool flag = prey.Faction == Faction.OfMechanoids;
            bool flag2 = flag;
            bool result;
            if (flag2)
            {
                bool flag3 = prey == null;
                bool flag4 = flag3;
                if (flag4)
                {
                    result = (prey.def != hunter.def);
                }
                else
                {
                    result = (prey.Faction == Faction.OfMechanoids);
                }
            }
            else
            {
                result = (prey.def == hunter.def);
            }
            return result;
        }

        private static bool isOwnRace(Pawn hunter, Pawn prey)
        {
            bool flag = hunter.RaceProps.Animal == prey.RaceProps.Animal;
            bool flag2 = flag;
            bool result;
            if (flag2)
            {
                bool flag3 = prey == null;
                bool flag4 = flag3;
                if (flag4)
                {
                    result = (prey.def == hunter.def);
                }
                else
                {
                    result = (prey.RaceProps.Animal != hunter.RaceProps.Animal);
                }
            }
            else
            {
                result = (prey.def == hunter.def);
            }
            return result;
        }

        private static bool isHumanlike(Pawn hunter, Pawn prey)
        {
            bool humanlike = prey.RaceProps.Humanlike;
            bool flag = humanlike;
            bool result;
            if (flag)
            {
                bool flag2 = prey == null;
                bool flag3 = flag2;
                if (flag3)
                {
                    result = (prey.def == hunter.def);
                }
                else
                {
                    result = (prey.RaceProps.Humanlike || prey.RaceProps.Humanlike);
                }
            }
            else
            {
                result = (prey.def == hunter.def);
            }
            return result;
        }

        private static bool isNearby(Pawn pawn, Thing thing)
        {
            return (pawn.Position - thing.Position).LengthHorizontalSquared <= 4225f;
        }

        public const int SEARCH_DISTANCE = 4225;
    }
}


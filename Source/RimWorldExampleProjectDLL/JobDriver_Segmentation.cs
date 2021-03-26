using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;

namespace MoreIncidents
{
    public class JobDriver_Segmentation : JobDriver
    {
        private Pawn abom;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(job.targetA, job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedOrNull(TargetIndex.A);
            this.FailOnDespawnedOrNull(TargetIndex.A);
            this.FailOn(eaterIsKilled);
            var resCorpse = new Toil();
            resCorpse.initAction = delegate
            {
                var actor = resCorpse.actor;
                var thing = resCorpse.actor.CurJob.GetTarget(TargetIndex.A).Thing;
                if (!thing.Spawned || !Map.reservationManager.CanReserve(actor, thing))
                {
                    actor.jobs.EndCurrentJob(JobCondition.Incompletable);
                }
                else
                {
                    Map.reservationManager.Reserve(actor, actor.CurJob, thing);
                }
            };
            resCorpse.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return resCorpse;
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            yield return Toils_General.Wait(400);
            var toil = new Toil {defaultCompleteMode = ToilCompleteMode.Instant, initAction = doStripCorpse};
            yield return toil;
            yield return Toils_General.Wait(60);
            var toil2 = new Toil {defaultCompleteMode = ToilCompleteMode.Instant, initAction = doChewCorpse};
            toil2.WithEffect(EffecterDefOf.EatMeat, TargetIndex.A);
            toil2.EndOnDespawnedOrNull(TargetIndex.A);
            yield return toil2;
        }

        private bool eaterIsKilled()
        {
            return pawn.Dead || pawn.Downed || pawn.HitPoints == 0;
        }

        private void doStripCorpse()
        {
            var thing = pawn.CurJob.targetA.Thing;
            var corpse = thing as Corpse;
            var flag = corpse != null && corpse.AnythingToStrip();
            var flag2 = flag;
            if (flag2)
            {
                corpse.Strip();
            }
        }

        private void doChewCorpse()
        {
            if (TargetThingA is Corpse corpse)
            {
                var position = corpse.Position;
                var list = theThing_Utility.ButcherCorpseProducts(corpse, pawn).ToList();
                int num;
                for (var i = 0; i < list.Count; i = num + 1)
                {
                    if (!GenPlace.TryPlaceThing(list[i], position, Map, ThingPlaceMode.Near, out var thing))
                    {
                        pawn.jobs.EndCurrentJob(JobCondition.Incompletable);
                    }

                    thing?.SetForbidden(true);

                    num = i;
                }

                pawn.needs.mood?.thoughts.memories.TryGainMemory(ThoughtDef.Named("AteCorpse"));

                corpse.Destroy();

                var abominationFaction =
                    Find.FactionManager.AllFactions.InRandomOrder().FirstOrDefault(faction =>
                        faction.def.defName == "MO_AbominationFaction");
                abom = PawnGenerator.GeneratePawn(MODefOf.MO_AbominationPawnKind, abominationFaction);
                GenSpawn.Spawn(abom, position, Map);
            }

            pawn.jobs.EndCurrentJob(JobCondition.Succeeded);
        }
    }
}
using System.Linq;
using RimWorld;
using Verse;

namespace MoreIncidents;

public class Ticker_RTWorker : Building
{
    private bool doOnce;

    private bool doononce;

    private Pawn Face;

    private bool facemutated;

    private int intervalcut = 3000;

    private IntVec3 intVec;

    private bool mutjustspawned = true;

    private IntVec3 mutplace;

    private Pawn newThing;

    private Pawn theThing;

    private int timemut = Rand.Range(16000, 28000);

    private float timer = 700f;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref intVec, "intVec");
        Scribe_Values.Look(ref timer, "timer");
        Scribe_Values.Look(ref doOnce, "doOnce");
        Scribe_References.Look(ref newThing, "newThing");
        Scribe_Values.Look(ref mutplace, "mutplace");
        Scribe_Values.Look(ref facemutated, "facemutated");
        Scribe_Values.Look(ref doononce, "doononce");
        Scribe_Values.Look(ref mutjustspawned, "mutjustspawned");
        Scribe_Values.Look(ref intervalcut, "intervalcut");
        Scribe_Values.Look(ref timemut, "timemut");
        Scribe_References.Look(ref theThing, "MO_theThing");
        Scribe_References.Look(ref Face, "Face");
    }

    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);
        mutjustspawned = true;
    }

    public override void Tick()
    {
        base.Tick();
        if (mutjustspawned)
        {
            if (timer > 0f)
            {
                timer -= 1f;
            }

            if (timer == 690f)
            {
                intVec = CellFinderLoose.RandomCellWith(
                    sq => sq.Standable(Map) && !Map.roofGrid.Roofed(sq) && !Map.fogGrid.IsFogged(sq) &&
                          !sq.CloseToEdge(Map, 20) && intVec.InBounds(Map) && !Map.areaManager.Home[sq], Map);
                var faction = Find.FactionManager.RandomNonHostileFaction(false, false, true, TechLevel.Spacer);
                Face = newThing = PawnGenerator.GeneratePawn(new PawnGenerationRequest(PawnKindDefOf.SpaceRefugee,
                    faction, PawnGenerationContext.NonPlayer, -1, true, false, false, false, true, 1f, true,
                    true, true, false));
                Face.RaceProps.thinkTreeMain = DefDatabase<ThinkTreeDef>.GetNamed("HumanlikeTheThing");
            }

            if (timer == 0f)
            {
                if (!doOnce)
                {
                    DropPodUtility.MakeDropPodAt(intVec, Map, new ActiveDropPodInfo
                    {
                        SingleContainedThing = Face,
                        openDelay = 180,
                        leaveSlag = true
                    });
                    HealthUtility.DamageUntilDowned(Face);
                    string text = "LetterLabelRefugeePodCrash".Translate();
                    string text2 = "RefugeePodCrash".Translate(Face.Named("PAWN")).AdjustedFor(Face);
                    Find.LetterStack.ReceiveLetter(text, text2, LetterDefOf.NeutralEvent,
                        new TargetInfo(intVec, Map));
                    doOnce = true;
                }
            }
        }

        var num = timemut;
        timemut = num - 1;
        num = intervalcut;
        intervalcut = num - 1;
        if (Face is { InContainerEnclosed: false } && Face.InBed() && !doononce)
        {
            doononce = true;
        }

        if (timemut != 0)
        {
            return;
        }

        if (Face == null)
        {
            if (!Destroyed)
            {
                Destroy();
            }

            return;
        }

        if (!facemutated)
        {
            if (!Face.Dead)
            {
                mutplace = Face.Position;
                var randomClosewalkCellNear = CellFinder.RandomClosewalkCellNear(mutplace, Map, 4);
                var named = DefDatabase<ThingDef>.GetNamed("Filth_Blood");
                FilthMaker.TryMakeFilth(randomClosewalkCellNear, Map, named, 4);
                var abominationFaction =
                    Find.FactionManager.AllFactions.InRandomOrder().FirstOrDefault(faction =>
                        faction.def.defName == "MO_AbominationFaction");

                var request = new PawnGenerationRequest(MODefOf.MO_AbominationPawnKind, abominationFaction,
                    PawnGenerationContext.NonPlayer, -1, true, false, false, false, true, 1f, true,
                    true, true, false);
                theThing = PawnGenerator.GeneratePawn(request);

                //theThing.mindState.mentalStateHandler.TryStartMentalState(
                //    MentalStateDefOf.ManhunterPermanent, null, true);
                GenSpawn.Spawn(theThing, mutplace, Map);
                Find.LetterStack.ReceiveLetter("MO_Trojan".Translate(),
                    "MO_TrojanDesc".Translate(Face.LabelShort, Face.Named("PAWN")), LetterDefOf.ThreatBig,
                    theThing); //this.Face.LabelShort + "has turned into an Abomination!"
                if (!Face.Destroyed)
                {
                    Face.Destroy();
                }

                if (!Destroyed)
                {
                    Destroy();
                }

                facemutated = true;
            }
            else
            {
                facemutated = true;
            }

            if (!Face.Destroyed)
            {
                Face.Destroy();
            }

            if (!Destroyed)
            {
                Destroy();
            }
        }

        if (!Face.Destroyed)
        {
            Face.Destroy();
        }

        if (!Destroyed)
        {
            Destroy();
        }

        if (!Destroyed)
        {
            Destroy();
        }
    }
}
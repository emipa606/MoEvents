using System;
using System.Collections.Generic;
using RimWorld;
using Verse;

namespace MoreIncidents
{
    public class Ticker_RTWorker : Building
    {
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<IntVec3>(ref this.intVec, "intVec", new IntVec3(), false);
            Scribe_Values.Look<float>(ref this.timer, "timer", 0f, false);
            Scribe_Values.Look<bool>(ref this.doOnce, "doOnce", false, false);
            Scribe_References.Look<Pawn>(ref this.newThing, "newThing", false);
            Scribe_Values.Look<IntVec3>(ref this.mutplace, "mutplace", new IntVec3(), false);
            Scribe_Values.Look<bool>(ref this.facemutated, "facemutated", false, false);
            Scribe_Values.Look<bool>(ref this.doononce, "doononce", false, false);
            Scribe_Values.Look<bool>(ref this.mutjustspawned, "mutjustspawned", false, false);
            Scribe_Values.Look<int>(ref this.intervalcut, "intervalcut", 0, false);
            Scribe_Values.Look<int>(ref this.timemut, "timemut", 0, false);
            Scribe_References.Look<Pawn>(ref this.theThing, "MO_theThing", false);
            Scribe_References.Look<Pawn>(ref this.Face, "Face", false);
        }
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            this.mutjustspawned = true;
        }

        public override void Tick()
        {
            base.Tick();
            bool flag = this.mutjustspawned;
            bool flag2 = flag;
            if (flag2)
            {
                bool flag3 = this.timer > 0f;
                bool flag4 = flag3;
                if (flag4)
                {
                    this.timer -= 1f;
                }
                bool flag5 = this.timer == 690f;
                bool flag6 = flag5;
                if (flag6)
                {
                    this.intVec = CellFinderLoose.RandomCellWith((IntVec3 sq) => GenGrid.Standable(sq, base.Map) && !base.Map.roofGrid.Roofed(sq) && !base.Map.fogGrid.IsFogged(sq) && !GenGrid.CloseToEdge(sq, base.Map, 20) && GenGrid.InBounds(this.intVec, base.Map) && !base.Map.areaManager.Home[sq], base.Map, 1000);
                    Faction faction = Find.FactionManager.RandomNonHostileFaction(false, false, true, TechLevel.Spacer);
                    this.Face = (this.newThing = PawnGenerator.GeneratePawn(new PawnGenerationRequest(PawnKindDefOf.SpaceRefugee, faction, PawnGenerationContext.NonPlayer, -1, true, false, false, false, true, false, 1f, true, true, true, false, false, false, false, false, 0f,
        null, 1f, null, null, null, null, null, null)));
                    this.Face.RaceProps.thinkTreeMain = DefDatabase<ThinkTreeDef>.GetNamed("HumanlikeTheThing");
                }
                bool flag7 = this.timer == 0f;
                bool flag8 = flag7;
                if (flag8)
                {
                    bool flag9 = !this.doOnce;
                    bool flag10 = flag9;
                    if (flag10)
                    {
                        DropPodUtility.MakeDropPodAt(this.intVec, base.Map, new ActiveDropPodInfo
                        {
                            SingleContainedThing = this.Face,
                            openDelay = 180,
                            leaveSlag = true
                        });
                        HealthUtility.DamageUntilDowned(this.Face);
                        string text = Translator.Translate("LetterLabelRefugeePodCrash");
                        string text2 = "RefugeePodCrash".Translate(this.Face.Named("PAWN")).AdjustedFor(this.Face, "PAWN", true);
                        Find.LetterStack.ReceiveLetter(text, text2, LetterDefOf.NeutralEvent, new TargetInfo(this.intVec, base.Map, false), null);
                        this.doOnce = true;
                    }
                }
            }
            int num = this.timemut;
            this.timemut = num - 1;
            num = this.intervalcut;
            this.intervalcut = num - 1;
            bool flag11 = this.Face != null && !this.Face.InContainerEnclosed && RestUtility.InBed(this.Face) && !this.doononce;
            bool flag12 = flag11;
            if (flag12)
            {
                this.doononce = true;
            }
            bool flag13 = this.timemut == 0;
            bool flag14 = flag13;
            if (flag14)
            {
                bool flag15 = this.Face != null;
                bool flag16 = flag15;
                if (flag16)
                {
                    bool flag17 = !this.facemutated;
                    bool flag18 = flag17;
                    if (flag18)
                    {
                        bool flag19 = !this.Face.Dead;
                        bool flag20 = flag19;
                        if (flag20)
                        {
                            this.mutplace = this.Face.Position;
                            IntVec3 intVec = CellFinder.RandomClosewalkCellNear(this.mutplace, base.Map, 4);
                            ThingDef named = DefDatabase<ThingDef>.GetNamed("Filth_Blood", true);
                            FilthMaker.TryMakeFilth(intVec, base.Map, named, 4);
                            IEnumerable<Faction> allFactions = Find.FactionManager.AllFactions;
                            bool flag21 = Ticker_RTWorker.rt.isPawn == null;
                            if (flag21)
                            {
                                Ticker_RTWorker.rt.isPawn = new Func<Faction, bool>(Ticker_RTWorker.rt.IncidentRT.whichFac);
                            }
                            PawnGenerationRequest request = new PawnGenerationRequest(MODefOf.MO_AbominationPawnKind, null, PawnGenerationContext.NonPlayer, -1, true, false, false, false, true, false, 1f, true, true, true, false, false, false, false, false, 0f,
    null, 1f, null, null, null, null, null, null);
                            this.theThing = PawnGenerator.GeneratePawn(request);
                            this.theThing.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent, null, true, false, null);
                            GenSpawn.Spawn(this.theThing, this.mutplace, base.Map);
                            Find.LetterStack.ReceiveLetter("MO_Trojan".Translate(), "MO_TrojanDesc".Translate(this.Face.LabelShort, this.Face.Named("PAWN")), LetterDefOf.ThreatBig, this.theThing, null); //this.Face.LabelShort + "has turned into an Abomination!"
                            bool flag22 = !this.Face.Destroyed;
                            bool flag23 = flag22;
                            if (flag23)
                            {
                                this.Face.Destroy(0);
                            }
                            bool flag24 = !base.Destroyed;
                            bool flag25 = flag24;
                            if (flag25)
                            {
                                this.Destroy(0);
                            }
                            this.facemutated = true;
                        }
                        else
                        {
                            this.facemutated = true;
                        }
                        bool flag26 = !this.Face.Destroyed;
                        bool flag27 = flag26;
                        if (flag27)
                        {
                            this.Face.Destroy(0);
                        }
                        bool flag28 = !base.Destroyed;
                        bool flag29 = flag28;
                        if (flag29)
                        {
                            this.Destroy(0);
                        }
                    }
                    bool flag30 = !this.Face.Destroyed;
                    bool flag31 = flag30;
                    if (flag31)
                    {
                        this.Face.Destroy(0);
                    }
                    bool flag32 = !base.Destroyed;
                    bool flag33 = flag32;
                    if (flag33)
                    {
                        this.Destroy(0);
                    }
                }
                else
                {
                    bool flag34 = this.Face == null;
                    bool flag35 = flag34;
                    if (flag35)
                    {
                        bool flag36 = !base.Destroyed;
                        bool flag37 = flag36;
                        if (flag37)
                        {
                            this.Destroy(0);
                        }
                    }
                }
                bool flag38 = !base.Destroyed;
                bool flag39 = flag38;
                if (flag39)
                {
                    this.Destroy(0);
                }
            }
        }

        private Pawn Face;

        private Pawn theThing;

        private int timemut = Rand.Range(16000, 28000);

        private bool mutjustspawned = true;

        private int intervalcut = 3000;

        private bool doononce = false;

        private bool facemutated = false;

        private IntVec3 mutplace;

        private Pawn newThing;

        private bool doOnce = false;

        private float timer = 700f;

        private IntVec3 intVec;

        private sealed class rt
        {
            internal bool whichFac(Faction fac)
            {
                return fac.def.defName == "MO_AbominationFaction";
            }

            public static readonly Ticker_RTWorker.rt IncidentRT = new Ticker_RTWorker.rt();

            public static Func<Faction, bool> isPawn;
        }
    }
}


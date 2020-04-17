using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace MoreIncidents.Settings
{
    // Token: 0x0200000D RID: 13
    internal class ME_ModSettings : ModSettings
    {
        internal static float StrokeBaseChance = 1.5f;
        internal static float SurvivalPodBaseChance = 1.5f;
        internal static float NauseaBaseChance = 3f;
        internal static float InsectsBaseChance = 1.5f;
        internal static float PodCrashBaseChance = 2f;
        internal static float ThanksgivingBaseChance = 2f;
        internal static float ShipBreakBaseChance = 1f;
        internal static float AmnesiaBaseChance = 2f;
        internal static float RescueTraitorBaseChance = 2f;
        internal static float MigrationBaseChance = 5f;
        internal static int MinRefireDaysRescueTraitor = 20;
        internal static int EarliestDayRescueTraitor = 25;

        public void ChangeDef()
        {
            List<IncidentDef> list = DefDatabase<IncidentDef>.AllDefs.ToList<IncidentDef>();
            foreach (IncidentDef incidentDef in list)
            {
                switch (incidentDef.defName)
                {
                    case "MO_Stroke":
                        incidentDef.baseChance = StrokeBaseChance;
                        break;
                    case "MO_SurvivalPod":
                        incidentDef.baseChance = SurvivalPodBaseChance;
                        break;
                    case "MO_Nausea":
                        incidentDef.baseChance = NauseaBaseChance;
                        break;
                    case "MO_Insects":
                        incidentDef.baseChance = InsectsBaseChance;
                        break;
                    case "MO_PodCrash":
                        incidentDef.baseChance = PodCrashBaseChance;
                        break;
                    case "MO_Thanksgiving":
                        incidentDef.baseChance = ThanksgivingBaseChance;
                        break;
                    case "MO_ShipBreak":
                        incidentDef.baseChance = ShipBreakBaseChance;
                        break;
                    case "MO_Amnesia":
                        incidentDef.baseChance = AmnesiaBaseChance;
                        break;
                    case "MO_RescueTraitor":
                        incidentDef.baseChance = RescueTraitorBaseChance;
                        break;
                    case "MO_Migration":
                        incidentDef.baseChance = MigrationBaseChance;
                        break;
                }
            }
        }

        public static void ChangeDefPost()
        {
            List<IncidentDef> list = DefDatabase<IncidentDef>.AllDefs.ToList<IncidentDef>();
            foreach (IncidentDef incidentDef in list)
            {
                switch (incidentDef.defName)
                {
                    case "MO_Stroke":
                        incidentDef.baseChance = StrokeBaseChance;
                        break;
                    case "MO_SurvivalPod":
                        incidentDef.baseChance = SurvivalPodBaseChance;
                        break;
                    case "MO_Nausea":
                        incidentDef.baseChance = NauseaBaseChance;
                        break;
                    case "MO_Insects":
                        incidentDef.baseChance = InsectsBaseChance;
                        break;
                    case "MO_PodCrash":
                        incidentDef.baseChance = PodCrashBaseChance;
                        break;
                    case "MO_Thanksgiving":
                        incidentDef.baseChance = ThanksgivingBaseChance;
                        break;
                    case "MO_ShipBreak":
                        incidentDef.baseChance = ShipBreakBaseChance;
                        break;
                    case "MO_Amnesia":
                        incidentDef.baseChance = AmnesiaBaseChance;
                        break;
                    case "MO_RescueTraitor":
                        incidentDef.baseChance = RescueTraitorBaseChance;
                        incidentDef.minRefireDays = MinRefireDaysRescueTraitor;
                        incidentDef.earliestDay = EarliestDayRescueTraitor;
                        break;
                    case "MO_Migration":
                        incidentDef.baseChance = MigrationBaseChance;
                        break;
                }
            }
        }

        // Token: 0x06000024 RID: 36 RVA: 0x0000345C File Offset: 0x0000165C
        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<float>(ref ME_ModSettings.StrokeBaseChance, "StrokeBaseChance", 1.5f, false);
            Scribe_Values.Look<float>(ref ME_ModSettings.SurvivalPodBaseChance, "SurvivalPodBaseChance", 1.5f, false);
            Scribe_Values.Look<float>(ref ME_ModSettings.NauseaBaseChance, "NauseaBaseChance", 3f, false);
            Scribe_Values.Look<float>(ref ME_ModSettings.InsectsBaseChance, "InsectsBaseChance", 1.5f, false);
            Scribe_Values.Look<float>(ref ME_ModSettings.PodCrashBaseChance, "PodCrashBaseChance", 2f, false);
            Scribe_Values.Look<float>(ref ME_ModSettings.ThanksgivingBaseChance, "ThanksgivingBaseChance", 2f, false);
            Scribe_Values.Look<float>(ref ME_ModSettings.ShipBreakBaseChance, "ShipBreakBaseChance", 1f, false);
            Scribe_Values.Look<float>(ref ME_ModSettings.AmnesiaBaseChance, "AmnesiaBaseChance", 2f, false);
            Scribe_Values.Look<float>(ref ME_ModSettings.RescueTraitorBaseChance, "RescueTraitorBaseChance", 2f, false);
            Scribe_Values.Look<float>(ref ME_ModSettings.MigrationBaseChance, "MigrationBaseChance", 5f, false);
            Scribe_Values.Look<int>(ref ME_ModSettings.MinRefireDaysRescueTraitor, "minRefireDaysRescueTraitor", 20, false);
            Scribe_Values.Look<int>(ref ME_ModSettings.EarliestDayRescueTraitor, "earliestDayRescueTraitor", 25, false);
        }


    }
}

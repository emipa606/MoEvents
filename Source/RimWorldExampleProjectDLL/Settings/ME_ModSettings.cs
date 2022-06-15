using System.Linq;
using RimWorld;
using Verse;

namespace MoreIncidents.Settings;

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
        var list = DefDatabase<IncidentDef>.AllDefs.ToList();
        foreach (var incidentDef in list)
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
        var list = DefDatabase<IncidentDef>.AllDefs.ToList();
        foreach (var incidentDef in list)
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

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref StrokeBaseChance, "StrokeBaseChance", 1.5f);
        Scribe_Values.Look(ref SurvivalPodBaseChance, "SurvivalPodBaseChance", 1.5f);
        Scribe_Values.Look(ref NauseaBaseChance, "NauseaBaseChance", 3f);
        Scribe_Values.Look(ref InsectsBaseChance, "InsectsBaseChance", 1.5f);
        Scribe_Values.Look(ref PodCrashBaseChance, "PodCrashBaseChance", 2f);
        Scribe_Values.Look(ref ThanksgivingBaseChance, "ThanksgivingBaseChance", 2f);
        Scribe_Values.Look(ref ShipBreakBaseChance, "ShipBreakBaseChance", 1f);
        Scribe_Values.Look(ref AmnesiaBaseChance, "AmnesiaBaseChance", 2f);
        Scribe_Values.Look(ref RescueTraitorBaseChance, "RescueTraitorBaseChance", 2f);
        Scribe_Values.Look(ref MigrationBaseChance, "MigrationBaseChance", 5f);
        Scribe_Values.Look(ref MinRefireDaysRescueTraitor, "minRefireDaysRescueTraitor", 20);
        Scribe_Values.Look(ref EarliestDayRescueTraitor, "earliestDayRescueTraitor", 25);
    }
}
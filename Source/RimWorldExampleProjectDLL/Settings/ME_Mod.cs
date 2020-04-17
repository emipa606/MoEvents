using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace MoreIncidents.Settings
{
    // Token: 0x0200000E RID: 14
    internal class ME_Mod : Mod
    {
        // Token: 0x06000027 RID: 39 RVA: 0x00003737 File Offset: 0x00001937
        public ME_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<ME_ModSettings>();
        }

        // Token: 0x06000028 RID: 40 RVA: 0x00003758 File Offset: 0x00001958
        public override string SettingsCategory()
        {
            return "Mo Events";
        }

        // Token: 0x06000029 RID: 41 RVA: 0x0000376C File Offset: 0x0000196C
        public void ResetSettings()
        {
            ME_ModSettings.StrokeBaseChance = 1.5f;
            ME_ModSettings.SurvivalPodBaseChance = 1.5f;
            ME_ModSettings.NauseaBaseChance = 3f;
            ME_ModSettings.InsectsBaseChance = 1.5f;
            ME_ModSettings.PodCrashBaseChance = 2f;
            ME_ModSettings.ThanksgivingBaseChance = 2f;
            ME_ModSettings.ShipBreakBaseChance = 1f;
            ME_ModSettings.AmnesiaBaseChance = 2f;
            ME_ModSettings.RescueTraitorBaseChance = 2f;
            ME_ModSettings.MigrationBaseChance = 5f;
            ME_ModSettings.MinRefireDaysRescueTraitor = 20;
            ME_ModSettings.EarliestDayRescueTraitor = 25;
            settings.Write();
            settings.ChangeDef();
        }

        // Token: 0x0600002C RID: 44 RVA: 0x000039CC File Offset: 0x00001BCC
        public override void DoSettingsWindowContents(Rect rect)
        {
            settings.ChangeDef();
            Rect rect2 = new Rect(rect.x, rect.y, rect.width - 30f, rect.height);
            Listing_Standard listing_Standard = new Listing_Standard();
            Widgets.BeginScrollView(rect, ref this.scrollPosition, rect2, true);
            listing_Standard.Begin(rect2);
            listing_Standard.Gap(10f);
            Rect rect3 = listing_Standard.GetRect(Text.LineHeight);
            bool flag = Widgets.ButtonText(rect3, "Reset Settings", true, true, true);
            if (flag)
            {
                ResetSettings();
            }
            listing_Standard.Gap(10f);
            
            Rect rect7 = listing_Standard.GetRect(Text.LineHeight);
            Widgets.Label(rect7, "MO_SettingHeader".Translate());
            listing_Standard.Gap(10f);
            Rect rect8 = listing_Standard.GetRect(Text.LineHeight);
            Rect rect9 = rect8.LeftHalf().Rounded();
            Rect rect10 = rect8.RightHalf().Rounded();
            Rect rect11 = rect9.LeftHalf().Rounded();
            Rect rect12 = rect9.RightHalf().Rounded();
            rect11.Overlaps(rect12);
            Rect rect13 = rect12.RightHalf().Rounded();
            Widgets.Label(rect11, "MO_Stroke".Translate());
            Widgets.Label(rect13, ME_ModSettings.StrokeBaseChance.ToString());
            ME_ModSettings.StrokeBaseChance = Widgets.HorizontalSlider(new Rect(rect10.xMin + rect10.height + 10f, rect10.y, rect10.width - (rect10.height * 2f + 20f), rect10.height), ME_ModSettings.StrokeBaseChance, 0f, 10f, true, null, null, null, -1f);
            listing_Standard.Gap(10f);
            Rect rect14 = listing_Standard.GetRect(Text.LineHeight);
            Rect rect15 = rect14.LeftHalf().Rounded();
            Rect rect16 = rect14.RightHalf().Rounded();
            Rect rect17 = rect15.LeftHalf().Rounded();
            Rect rect18 = rect15.RightHalf().Rounded();
            rect17.Overlaps(rect18);
            Rect rect19 = rect18.RightHalf().Rounded();
            Widgets.Label(rect17, "MO_SurvivalPods".Translate());
            Widgets.Label(rect19, ME_ModSettings.SurvivalPodBaseChance.ToString());
            ME_ModSettings.SurvivalPodBaseChance = Widgets.HorizontalSlider(new Rect(rect16.xMin + rect16.height + 10f, rect16.y, rect16.width - (rect16.height * 2f + 20f), rect16.height), ME_ModSettings.SurvivalPodBaseChance, 0f, 10f, true, null, null, null, -1f);
            listing_Standard.Gap(10f);
            Rect rect20 = listing_Standard.GetRect(Text.LineHeight);
            Rect rect21 = rect20.LeftHalf().Rounded();
            Rect rect22 = rect20.RightHalf().Rounded();
            Rect rect23 = rect21.LeftHalf().Rounded();
            Rect rect24 = rect21.RightHalf().Rounded();
            rect23.Overlaps(rect24);
            Rect rect25 = rect24.RightHalf().Rounded();
            Widgets.Label(rect23, "MO_Nausea".Translate());
            Widgets.Label(rect25, ME_ModSettings.NauseaBaseChance.ToString());
            ME_ModSettings.NauseaBaseChance = Widgets.HorizontalSlider(new Rect(rect22.xMin + rect22.height + 10f, rect22.y, rect22.width - (rect22.height * 2f + 20f), rect22.height), ME_ModSettings.NauseaBaseChance, 0f, 10f, true, null, null, null, -1f);
            listing_Standard.Gap(10f);
            Rect rect26 = listing_Standard.GetRect(Text.LineHeight);
            Rect rect27 = rect26.LeftHalf().Rounded();
            Rect rect28 = rect26.RightHalf().Rounded();
            Rect rect29 = rect27.LeftHalf().Rounded();
            Rect rect30 = rect27.RightHalf().Rounded();
            rect29.Overlaps(rect30);
            Rect rect31 = rect30.RightHalf().Rounded();
            Widgets.Label(rect29, "MO_Insects".Translate());
            Widgets.Label(rect31, ME_ModSettings.InsectsBaseChance.ToString());
            ME_ModSettings.InsectsBaseChance = Widgets.HorizontalSlider(new Rect(rect28.xMin + rect28.height + 10f, rect28.y, rect28.width - (rect28.height * 2f + 20f), rect28.height), ME_ModSettings.InsectsBaseChance, 0f, 10f, true, null, null, null, -1f);
            listing_Standard.Gap(10f);
            Rect rect32 = listing_Standard.GetRect(Text.LineHeight);
            Rect rect33 = rect32.LeftHalf().Rounded();
            Rect rect34 = rect32.RightHalf().Rounded();
            Rect rect35 = rect33.LeftHalf().Rounded();
            Rect rect36 = rect33.RightHalf().Rounded();
            rect35.Overlaps(rect36);
            Rect rect37 = rect36.RightHalf().Rounded();
            Widgets.Label(rect35, "MO_TribalAid".Translate());
            Widgets.Label(rect37, ME_ModSettings.PodCrashBaseChance.ToString());
            ME_ModSettings.PodCrashBaseChance = Widgets.HorizontalSlider(new Rect(rect34.xMin + rect34.height + 10f, rect34.y, rect34.width - (rect34.height * 2f + 20f), rect34.height), ME_ModSettings.PodCrashBaseChance, 0f, 10f, true, null, null, null, -1f);
            listing_Standard.Gap(10f);
            Rect rect38 = listing_Standard.GetRect(Text.LineHeight);
            Rect rect39 = rect38.LeftHalf().Rounded();
            Rect rect40 = rect38.RightHalf().Rounded();
            Rect rect41 = rect39.LeftHalf().Rounded();
            Rect rect42 = rect39.RightHalf().Rounded();
            rect41.Overlaps(rect42);
            Rect rect43 = rect42.RightHalf().Rounded();
            Widgets.Label(rect41, "MO_Thanksgiving".Translate());
            Widgets.Label(rect43, ME_ModSettings.ThanksgivingBaseChance.ToString());
            ME_ModSettings.ThanksgivingBaseChance = Widgets.HorizontalSlider(new Rect(rect40.xMin + rect40.height + 10f, rect40.y, rect40.width - (rect40.height * 2f + 20f), rect40.height), ME_ModSettings.ThanksgivingBaseChance, 0f, 10f, true, null, null, null, -1f);
            listing_Standard.Gap(10f);
            Rect rect44 = listing_Standard.GetRect(Text.LineHeight);
            Rect rect45 = rect44.LeftHalf().Rounded();
            Rect rect46 = rect44.RightHalf().Rounded();
            Rect rect47 = rect45.LeftHalf().Rounded();
            Rect rect48 = rect45.RightHalf().Rounded();
            rect47.Overlaps(rect48);
            Rect rect49 = rect48.RightHalf().Rounded();
            Widgets.Label(rect47, "MO_CargoRain".Translate());
            Widgets.Label(rect49, ME_ModSettings.ShipBreakBaseChance.ToString());
            ME_ModSettings.ShipBreakBaseChance = Widgets.HorizontalSlider(new Rect(rect46.xMin + rect46.height + 10f, rect46.y, rect46.width - (rect46.height * 2f + 20f), rect46.height), ME_ModSettings.ShipBreakBaseChance, 0f, 10f, true, null, null, null, -1f);
            listing_Standard.Gap(10f);
            Rect rect50 = listing_Standard.GetRect(Text.LineHeight);
            Rect rect51 = rect50.LeftHalf().Rounded();
            Rect rect52 = rect50.RightHalf().Rounded();
            Rect rect53 = rect51.LeftHalf().Rounded();
            Rect rect54 = rect51.RightHalf().Rounded();
            rect53.Overlaps(rect54);
            Rect rect55 = rect54.RightHalf().Rounded();
            Widgets.Label(rect53, "MO_Amnesia".Translate());
            Widgets.Label(rect55, ME_ModSettings.AmnesiaBaseChance.ToString());
            ME_ModSettings.AmnesiaBaseChance = Widgets.HorizontalSlider(new Rect(rect52.xMin + rect52.height + 10f, rect52.y, rect52.width - (rect52.height * 2f + 20f), rect52.height), ME_ModSettings.AmnesiaBaseChance, 0f, 10f, true, null, null, null, -1f);
            listing_Standard.Gap(10f);
            Rect rect56 = listing_Standard.GetRect(Text.LineHeight);
            Rect rect57 = rect56.LeftHalf().Rounded();
            Rect rect58 = rect56.RightHalf().Rounded();
            Rect rect59 = rect57.LeftHalf().Rounded();
            Rect rect60 = rect57.RightHalf().Rounded();
            rect59.Overlaps(rect60);
            Rect rect61 = rect60.RightHalf().Rounded();
            Widgets.Label(rect59, "MO_Migration".Translate());
            Widgets.Label(rect61, ME_ModSettings.MigrationBaseChance.ToString());
            ME_ModSettings.MigrationBaseChance = Widgets.HorizontalSlider(new Rect(rect58.xMin + rect58.height + 10f, rect58.y, rect58.width - (rect58.height * 2f + 20f), rect58.height), ME_ModSettings.MigrationBaseChance, 0f, 10f, true, null, null, null, -1f);
            listing_Standard.Gap(30f);
            Rect rect62 = listing_Standard.GetRect(Text.LineHeight);
            Rect rect63 = rect62.LeftHalf().Rounded();
            Rect rect64 = rect62.RightHalf().Rounded();
            Rect rect65 = rect63.LeftHalf().Rounded();
            Rect rect66 = rect63.RightHalf().Rounded();
            rect65.Overlaps(rect66);
            Rect rect67 = rect66.RightHalf().Rounded();
            Widgets.Label(rect65, "MO_Trojan".Translate());
            Widgets.Label(rect67, ME_ModSettings.RescueTraitorBaseChance.ToString());
            ME_ModSettings.RescueTraitorBaseChance = Widgets.HorizontalSlider(new Rect(rect64.xMin + rect64.height + 10f, rect64.y, rect64.width - (rect64.height * 2f + 20f), rect64.height), ME_ModSettings.RescueTraitorBaseChance, 0f, 10f, true, null, null, null, -1f);
            listing_Standard.Gap(10f);
            Rect rect129 = listing_Standard.GetRect(Text.LineHeight);
            Rect rect130 = rect129.LeftHalf().Rounded();
            Rect rect131 = rect129.RightHalf().Rounded();
            Rect rect132 = rect130.LeftHalf().Rounded();
            Rect rect133 = rect130.RightHalf().Rounded();
            rect132.Overlaps(rect133);
            Rect rect134 = rect133.RightHalf().Rounded();
            Widgets.Label(rect132, "MO_TrojanReFire".Translate());
            Widgets.Label(rect134, ME_ModSettings.MinRefireDaysRescueTraitor.ToString());
            ME_ModSettings.MinRefireDaysRescueTraitor = (int)Widgets.HorizontalSlider(new Rect(rect131.xMin + rect131.height + 10f, rect131.y, rect131.width - (rect131.height * 2f + 20f), rect131.height), (float)ME_ModSettings.MinRefireDaysRescueTraitor, 10f, 400f, true, null, null, null, -1f);
            listing_Standard.Gap(10f);
            Rect rect135 = listing_Standard.GetRect(Text.LineHeight);
            Rect rect136 = rect135.LeftHalf().Rounded();
            Rect rect137 = rect135.RightHalf().Rounded();
            Rect rect138 = rect136.LeftHalf().Rounded();
            Rect rect139 = rect136.RightHalf().Rounded();
            rect138.Overlaps(rect139);
            Rect rect140 = rect139.RightHalf().Rounded();
            Widgets.Label(rect138, "MO_TrojanFire".Translate());
            Widgets.Label(rect140, ME_ModSettings.EarliestDayRescueTraitor.ToString());
            ME_ModSettings.EarliestDayRescueTraitor = (int)Widgets.HorizontalSlider(new Rect(rect137.xMin + rect137.height + 10f, rect137.y, rect137.width - (rect137.height * 2f + 20f), rect137.height), (float)ME_ModSettings.EarliestDayRescueTraitor, 5f, 280f, true, null, null, null, -1f);
            listing_Standard.End();
            Widgets.EndScrollView();
            settings.Write();
            settings.ChangeDef();
        }

        // Token: 0x0400003D RID: 61
        public static ME_ModSettings settings;

        // Token: 0x0400003E RID: 62
        private Vector2 scrollPosition = Vector2.zero;
    }
}

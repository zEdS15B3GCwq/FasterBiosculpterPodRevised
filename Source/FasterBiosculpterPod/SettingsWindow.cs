using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace FasterBiosculpterPod
{
    public partial class Settings
    {
        private const float RowHeight = 30f;
        private const float FieldWidth = 100f;
        private const float FooterHeight = 40f;
        private const float ResetButtonWidth = 220f;
        private static readonly Color DisabledColor = new Color(0.55f, 0.55f, 0.55f);

        private Vector2 scrollPosition;
        private float cachedScrollHeight = 1500f;
        private readonly Dictionary<string, string> numericBuffers = new Dictionary<string, string>();

        internal void ClearNumericBuffers()
        {
            numericBuffers.Clear();
        }

        internal void DoSettingsWindowContents(Rect outerRect)
        {
            Rect scrollRect = new Rect(outerRect.x, outerRect.y, outerRect.width, outerRect.height - FooterHeight - 5f);
            Rect footerRect = new Rect(outerRect.x, outerRect.yMax - FooterHeight, ResetButtonWidth, FooterHeight - 5f);
            Rect viewRect = new Rect(0f, 0f, scrollRect.width - 16f, cachedScrollHeight);

            EnforceResearchPrerequisites();

            Widgets.BeginScrollView(scrollRect, ref scrollPosition, viewRect);
            Listing_Standard listing = new Listing_Standard();
            listing.maxOneColumn = true;
            listing.Begin(viewRect);

            DrawCycleSettings(listing);
            DrawMaintenanceSettings(listing);
            DrawBuildingCostSettings(listing);
            DrawResearchSettings(listing);

            cachedScrollHeight = listing.CurHeight + 10f;
            listing.End();
            Widgets.EndScrollView();

            if (Widgets.ButtonText(footerRect, "Inglix.Reset_To_Defaults".Translate()))
            {
                ResetToDefaults();
                ClearNumericBuffers();
            }
        }

        private void DrawCycleSettings(Listing_Standard listing)
        {
            Heading(listing, "Inglix.Cycles".Translate());
            string cycleTip = "Inglix.Zero_Instant".Translate();

            NumericField(listing, nameof(BiosculpterPodSpeedFactor), "Inglix.BiosculpterPodSpeedFactor".Translate(), ref BiosculpterPodSpeedFactor, Constants.BiosculpterPodSpeedFactorMin, Constants.BiosculpterPodSpeedFactorMax, Constants.VanillaBiosculpterPodSpeedFactor);

            SubHeading(listing, "Inglix.Medic_Cycle".Translate());
            NumericField(listing, nameof(MedicCycleDays), DaysLabel("Inglix.Cycle_Duration".Translate()), ref MedicCycleDays, 0, Constants.CycleDaysMax, Constants.VanillaMedicCycleDays, cycleTip);

            SubHeading(listing, "Inglix.Bioregeneration_Cycle".Translate());
            NumericField(listing, nameof(BioregenerationCycleDays), DaysLabel("Inglix.Cycle_Duration".Translate()), ref BioregenerationCycleDays, 0, Constants.CycleDaysMax, Constants.VanillaBioregenerationCycleDays, cycleTip);
            NumericField(listing, nameof(BioregenerationCycleMedicineUltratech), ThingDefOf.MedicineUltratech.LabelCap.ToString(), ref BioregenerationCycleMedicineUltratech, 0, Constants.MedicineUltratechMax, Constants.VanillaBioregenerationCycleMedicineUltratech);

            SubHeading(listing, "Inglix.Age_Reversal_Cycle".Translate());
            NumericField(listing, nameof(AgeReversalCycleDays), DaysLabel("Inglix.Cycle_Duration".Translate()), ref AgeReversalCycleDays, 0, Constants.CycleDaysMax, Constants.VanillaAgeReversalCycleDays, cycleTip);
            NumericField(listing, nameof(AgeReversalDays), DaysLabel("Inglix.Age_Reversed".Translate()), ref AgeReversalDays, 0, Constants.LongDaysMax, Constants.VanillaAgeReversalDays);

            SubHeading(listing, "Inglix.Pleasure_Cycle".Translate());
            NumericField(listing, nameof(PleasureCycleDays), DaysLabel("Inglix.Cycle_Duration".Translate()), ref PleasureCycleDays, 0, Constants.CycleDaysMax, Constants.VanillaPleasureCycleDays, cycleTip);
            NumericField(listing, nameof(PleasureCycleMoodDays), DaysLabel("Inglix.Mood_Duration".Translate()), ref PleasureCycleMoodDays, 0, Constants.CycleDaysMax, Constants.VanillaPleasureCycleMoodDays);
            NumericField(listing, nameof(PleasureCycleMoodEffect), "Inglix.Mood_Effect".Translate(), ref PleasureCycleMoodEffect, 0f, Constants.MoodEffectMax, Constants.VanillaPleasureCycleMoodEffect);
        }

        private void DrawMaintenanceSettings(Listing_Standard listing)
        {
            Heading(listing, "Inglix.Maintenance".Translate());

            NumericField(listing, nameof(NutritionRequired), "Inglix.Nutrition_Required".Translate(), ref NutritionRequired, 0f, Constants.NutritionRequiredMax, Constants.VanillaNutritionRequired);
            NumericField(listing, nameof(PowerConsumption), "Inglix.Power_Consumption".Translate() + " (W)", ref PowerConsumption, 0f, Constants.PowerMax, Constants.VanillaPowerConsumption);
            NumericField(listing, nameof(StandbyConsumption), "Inglix.Standby_Consumption".Translate() + " (W)", ref StandbyConsumption, 0f, Constants.PowerMax, Constants.VanillaStandbyConsumption);
            NumericField(listing, nameof(BiotuningDurationDays), DaysLabel("Inglix.Biotuning_Duration".Translate()), ref BiotuningDurationDays, 0, Constants.LongDaysMax, Constants.VanillaBiotuningDurationDays);
            NumericField(listing, nameof(BiotunedCycleSpeedFactor), "Inglix.Biotuned_Speed".Translate(), ref BiotunedCycleSpeedFactor, 0f, Constants.BiotunedCycleSpeedFactorMax, Constants.VanillaBiotunedCycleSpeedFactor);
        }

        private void DrawBuildingCostSettings(Listing_Standard listing)
        {
            Heading(listing, "Inglix.Building_Cost".Translate());

            CheckboxField(listing, "Inglix.Enable_Build_Costs".Translate(), ref EnableBuildCostSettings, false);
            string reason = EnableBuildCostSettings ? null : "Inglix.Requires_Build_Costs".Translate().ToString();

            NumericField(listing, nameof(SteelCost), ThingDefOf.Steel.LabelCap.ToString(), ref SteelCost, 0, Constants.ItemCostMax, Constants.VanillaSteelCost, null, reason);
            NumericField(listing, nameof(ComponentIndustrialCost), ThingDefOf.ComponentIndustrial.LabelCap.ToString(), ref ComponentIndustrialCost, 0, Constants.ItemCostMax, Constants.VanillaComponentIndustrialCost, null, reason);
            NumericField(listing, nameof(PlasteelCost), ThingDefOf.Plasteel.LabelCap.ToString(), ref PlasteelCost, 0, Constants.ItemCostMax, Constants.VanillaPlasteelCost, null, reason);
            NumericField(listing, nameof(ComponentSpacerCost), ThingDefOf.ComponentSpacer.LabelCap.ToString(), ref ComponentSpacerCost, 0, Constants.ItemCostMax, Constants.VanillaComponentSpacerCost, null, reason);
            NumericField(listing, nameof(UraniumCost), ThingDefOf.Uranium.LabelCap.ToString(), ref UraniumCost, 0, Constants.ItemCostMax, Constants.VanillaUraniumCost, null, reason);
            NumericField(listing, nameof(WorkToBuild), StatDefOf.WorkToBuild.LabelCap.ToString(), ref WorkToBuild, 0, Constants.WorkToBuildMax, Constants.VanillaWorkToBuild, null, reason);
        }

        private void DrawResearchSettings(Listing_Standard listing)
        {
            Heading(listing, "Inglix.Research_Prerequisites".Translate());

            SubHeading(listing, "Inglix.Biosculpting_Research".Translate());
            NumericField(listing, nameof(ResearchBaseCostBiosculpting), "Inglix.Research_Cost".Translate(), ref ResearchBaseCostBiosculpting, 0, Constants.ResearchCostMax, Constants.VanillaResearchBaseCostBiosculpting);
            TechLevelField(listing, "Inglix.Tech_Level".Translate(), ref ResearchLevelBiosculpting, Constants.VanillaResearchLevelBiosculpting, TechLevel.Industrial);

            string biosculptingBenchReason = ResearchLevelBiosculpting > TechLevel.Industrial ? "Inglix.Locked_Tech_Level".Translate().ToString() : null;
            CheckboxField(listing, "Inglix.Require_HiTechBench".Translate(), ref ResearchBiosculptingHiTechResearchBench, Constants.VanillaResearchBiosculptingHiTechResearchBench, biosculptingBenchReason);

            string biosculptingAnalyzerReason = null;
            if (ResearchLevelBiosculpting > TechLevel.Spacer)
                biosculptingAnalyzerReason = "Inglix.Locked_Tech_Level".Translate();
            else if (!ResearchBiosculptingHiTechResearchBench)
                biosculptingAnalyzerReason = "Inglix.Requires_HiTechBench_First".Translate();
            CheckboxField(listing, "Inglix.Require_MultiAnalyzer".Translate(), ref ResearchBiosculptingMultiAnalyzer, Constants.VanillaResearchBiosculptingMultiAnalyzer, biosculptingAnalyzerReason);

            SubHeading(listing, "Inglix.Bioregeneration_Research".Translate());
            NumericField(listing, nameof(ResearchBaseCostBioregeneration), "Inglix.Research_Cost".Translate(), ref ResearchBaseCostBioregeneration, 0, Constants.ResearchCostMax, Constants.VanillaResearchBaseCostBioregeneration);
            TechLevelField(listing, "Inglix.Tech_Level".Translate(), ref ResearchLevelBioregeneration, Constants.VanillaResearchLevelBioregeneration, ResearchLevelBiosculpting);

            string bioregenerationBenchReason = null;
            if (ResearchLevelBioregeneration > TechLevel.Industrial)
                bioregenerationBenchReason = "Inglix.Locked_Tech_Level".Translate();
            else if (ResearchBiosculptingHiTechResearchBench)
                bioregenerationBenchReason = "Inglix.Locked_By_Biosculpting".Translate();
            CheckboxField(listing, "Inglix.Require_HiTechBench".Translate(), ref ResearchBioregenerationHiTechResearchBench, Constants.VanillaResearchBioregenerationHiTechResearchBench, bioregenerationBenchReason);

            string bioregenerationAnalyzerReason = null;
            if (ResearchLevelBioregeneration > TechLevel.Spacer)
                bioregenerationAnalyzerReason = "Inglix.Locked_Tech_Level".Translate();
            else if (ResearchBiosculptingMultiAnalyzer)
                bioregenerationAnalyzerReason = "Inglix.Locked_By_Biosculpting".Translate();
            else if (!ResearchBioregenerationHiTechResearchBench)
                bioregenerationAnalyzerReason = "Inglix.Requires_HiTechBench_First".Translate();
            CheckboxField(listing, "Inglix.Require_MultiAnalyzer".Translate(), ref ResearchBioregenerationMultiAnalyzer, Constants.VanillaResearchBioregenerationMultiAnalyzer, bioregenerationAnalyzerReason);
        }

        private static string DaysLabel(string label)
        {
            return label + " (" + "Inglix.Unit_Days".Translate() + ")";
        }

        private static void Heading(Listing_Standard listing, string text)
        {
            listing.Gap(8f);
            Text.Font = GameFont.Medium;
            listing.Label(text);
            Text.Font = GameFont.Small;
            listing.GapLine();
        }

        private static void SubHeading(Listing_Standard listing, string text)
        {
            listing.Gap(4f);
            listing.Label(text.Colorize(ColoredText.TipSectionTitleColor));
        }

        private void NumericField<T>(Listing_Standard listing, string key, string label, ref T value, float min, float max, T defaultValue, string tooltip = null, string disabledReason = null) where T : struct
        {
            bool enabled = disabledReason == null;
            Rect row = listing.GetRect(RowHeight);
            Rect labelRect = new Rect(row.x, row.y, row.width - FieldWidth - 10f, row.height);
            Rect fieldRect = new Rect(row.xMax - FieldWidth, row.y + 2f, FieldWidth, row.height - 4f);

            bool previousEnabled = GUI.enabled;
            Color previousColor = GUI.color;
            if (!enabled)
            {
                GUI.enabled = false;
                GUI.color = DisabledColor;
            }

            Text.Anchor = TextAnchor.MiddleLeft;
            Widgets.Label(labelRect, label + " " + "Inglix.Default_Label".Translate(defaultValue.ToString()));
            Text.Anchor = TextAnchor.UpperLeft;

            numericBuffers.TryGetValue(key, out string buffer);
            Widgets.TextFieldNumeric(fieldRect, ref value, ref buffer, min, max);
            numericBuffers[key] = buffer;

            GUI.enabled = previousEnabled;
            GUI.color = previousColor;

            string tip = enabled ? "Inglix.Range_Tooltip".Translate(min.ToString(), max.ToString()).ToString() : disabledReason;
            if (enabled && tooltip != null)
                tip += "\n" + tooltip;
            TooltipHandler.TipRegion(row, tip);
        }

        private static void CheckboxField(Listing_Standard listing, string label, ref bool value, bool defaultValue, string disabledReason = null)
        {
            bool enabled = disabledReason == null;
            Rect row = listing.GetRect(RowHeight);

            Color previousColor = GUI.color;
            if (!enabled)
                GUI.color = DisabledColor;

            string defaultText = (defaultValue ? "Yes" : "No").Translate();
            Widgets.CheckboxLabeled(row, label + " " + "Inglix.Default_Label".Translate(defaultText), ref value, !enabled);
            GUI.color = previousColor;

            if (!enabled)
                TooltipHandler.TipRegion(row, disabledReason);
        }

        private static void TechLevelField(Listing_Standard listing, string label, ref TechLevel value, TechLevel defaultValue, TechLevel minimumLevel)
        {
            Rect labelRow = listing.GetRect(RowHeight);
            Text.Anchor = TextAnchor.MiddleLeft;
            Widgets.Label(labelRow, label + " " + "Inglix.Default_Label".Translate(TechLevelName(defaultValue)));
            Text.Anchor = TextAnchor.UpperLeft;

            Rect row = listing.GetRect(RowHeight);
            TechLevel[] levels = { TechLevel.Industrial, TechLevel.Spacer, TechLevel.Ultra };
            float width = row.width / levels.Length;
            for (int i = 0; i < levels.Length; i++)
            {
                Rect optionRect = new Rect(row.x + i * width, row.y, width, row.height);
                bool locked = levels[i] < minimumLevel;

                Color previousColor = GUI.color;
                if (locked)
                    GUI.color = DisabledColor;
                bool clicked = Widgets.RadioButtonLabeled(optionRect, TechLevelName(levels[i]), value == levels[i]);
                GUI.color = previousColor;

                if (locked)
                    TooltipHandler.TipRegion(optionRect, "Inglix.Locked_By_Biosculpting".Translate());
                else if (clicked)
                    value = levels[i];
            }
        }

        private static string TechLevelName(TechLevel level)
        {
            return ("TechLevel_" + level).Translate().CapitalizeFirst();
        }
    }
}

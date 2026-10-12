using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace FasterBiosculpterPod
{
    [HotSwappable]
    public partial class Settings : ModSettings
    {
        public float BiosculpterPodSpeedFactor = Constants.VanillaBiosculpterPodSpeedFactor;

        public int MedicCycleDays = Constants.VanillaMedicCycleDays;
        public int BioregenerationCycleDays = Constants.VanillaBioregenerationCycleDays;
        public int BioregenerationCycleMedicineUltratech = Constants.VanillaBioregenerationCycleMedicineUltratech;
        public int AgeReversalCycleDays = Constants.VanillaAgeReversalCycleDays;
        public int AgeReversalDays = Constants.VanillaAgeReversalDays;
        public int PleasureCycleDays = Constants.VanillaPleasureCycleDays;
        public int PleasureCycleMoodDays = Constants.VanillaPleasureCycleMoodDays;
        public float PleasureCycleMoodEffect = Constants.VanillaPleasureCycleMoodEffect;

        public float NutritionRequired = Constants.VanillaNutritionRequired;

        public int BiotuningDurationDays = Constants.VanillaBiotuningDurationDays;
        public float BiotunedCycleSpeedFactor = Constants.VanillaBiotunedCycleSpeedFactor;
        public float PowerConsumption = Constants.VanillaPowerConsumption;
        public float StandbyConsumption = Constants.VanillaStandbyConsumption;

        public bool EnableBuildCostSettings = false;
        public List<ThingDefCountClass> InitialCostList;
        public float InitialWorkToBuild;
        public int SteelCost = Constants.VanillaSteelCost;
        public int ComponentIndustrialCost = Constants.VanillaComponentIndustrialCost;
        public int PlasteelCost = Constants.VanillaPlasteelCost;
        public int ComponentSpacerCost = Constants.VanillaComponentSpacerCost;
        public int UraniumCost = Constants.VanillaUraniumCost;
        public int WorkToBuild = Constants.VanillaWorkToBuild;

        public int ResearchBaseCostBiosculpting = Constants.VanillaResearchBaseCostBiosculpting;
        public int ResearchBaseCostBioregeneration = Constants.VanillaResearchBaseCostBioregeneration;
        public TechLevel ResearchLevelBiosculpting = Constants.VanillaResearchLevelBiosculpting;
        public TechLevel ResearchLevelBioregeneration = Constants.VanillaResearchLevelBioregeneration;
        public bool ResearchBiosculptingHiTechResearchBench = Constants.VanillaResearchBiosculptingHiTechResearchBench;
        public bool ResearchBiosculptingMultiAnalyzer = Constants.VanillaResearchBiosculptingMultiAnalyzer;
        public bool ResearchBioregenerationHiTechResearchBench = Constants.VanillaResearchBioregenerationHiTechResearchBench;
        public bool ResearchBioregenerationMultiAnalyzer = Constants.VanillaResearchBioregenerationMultiAnalyzer;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref BiosculpterPodSpeedFactor, "biosculpterPodSpeedFactor", Constants.VanillaBiosculpterPodSpeedFactor, forceSave: true);

            Scribe_Values.Look(ref MedicCycleDays, "medicCycleDays", Constants.VanillaMedicCycleDays, forceSave: true);

            Scribe_Values.Look(ref BioregenerationCycleDays, "bioregenerationCycleDays", Constants.VanillaBioregenerationCycleDays, forceSave: true);
            Scribe_Values.Look(ref BioregenerationCycleMedicineUltratech, "bioregenerationCycleMedicineUltratech", Constants.VanillaBioregenerationCycleMedicineUltratech, forceSave: true);

            Scribe_Values.Look(ref AgeReversalCycleDays, "ageReversalCycleDays", Constants.VanillaAgeReversalCycleDays, forceSave: true);
            Scribe_Values.Look(ref AgeReversalDays, "ageReversalDays", Constants.VanillaAgeReversalDays, forceSave: true);

            Scribe_Values.Look(ref PleasureCycleDays, "pleasureCycleDays", Constants.VanillaPleasureCycleDays, forceSave: true);
            Scribe_Values.Look(ref PleasureCycleMoodDays, "pleasureCycleMoodDays", Constants.VanillaPleasureCycleMoodDays, forceSave: true);
            Scribe_Values.Look(ref PleasureCycleMoodEffect, "pleasureCycleMoodEffect", Constants.VanillaPleasureCycleMoodEffect, forceSave: true);

            Scribe_Values.Look(ref NutritionRequired, "nutritionRequired", Constants.VanillaNutritionRequired, forceSave: true);

            Scribe_Values.Look(ref BiotuningDurationDays, "biotuningDurationDays", Constants.VanillaBiotuningDurationDays, forceSave: true);
            Scribe_Values.Look(ref BiotunedCycleSpeedFactor, "biotunedCycleSpeedFactor", Constants.VanillaBiotunedCycleSpeedFactor, forceSave: true);

            Scribe_Values.Look(ref PowerConsumption, "powerConsumption", Constants.VanillaPowerConsumption, forceSave: true);
            Scribe_Values.Look(ref StandbyConsumption, "standbyConsumption", Constants.VanillaStandbyConsumption, forceSave: true);

            Scribe_Values.Look(ref EnableBuildCostSettings, "enableBuildCostSettings", false, forceSave: true);
            Scribe_Values.Look(ref SteelCost, "steelCost", Constants.VanillaSteelCost, forceSave: true);
            Scribe_Values.Look(ref ComponentIndustrialCost, "componentIndustrialCost", Constants.VanillaComponentIndustrialCost, forceSave: true);
            Scribe_Values.Look(ref PlasteelCost, "plasteelCost", Constants.VanillaPlasteelCost, forceSave: true);
            Scribe_Values.Look(ref ComponentSpacerCost, "componentSpacerCost", Constants.VanillaComponentSpacerCost, forceSave: true);
            Scribe_Values.Look(ref UraniumCost, "uraniumCost", Constants.VanillaUraniumCost, forceSave: true);
            Scribe_Values.Look(ref WorkToBuild, "workToBuild", Constants.VanillaWorkToBuild, forceSave: true);

            Scribe_Values.Look(ref ResearchBaseCostBiosculpting, "researchBaseCostBiosculpting", Constants.VanillaResearchBaseCostBiosculpting, forceSave: true);
            Scribe_Values.Look(ref ResearchBaseCostBioregeneration, "researchBaseCostBioregeneration", Constants.VanillaResearchBaseCostBioregeneration, forceSave: true);

            Scribe_Values.Look(ref ResearchLevelBiosculpting, "researchLevelBiosculpting", Constants.VanillaResearchLevelBiosculpting, forceSave: true);
            Scribe_Values.Look(ref ResearchLevelBioregeneration, "researchLevelBioregeneration", Constants.VanillaResearchLevelBioregeneration, forceSave: true);

            Scribe_Values.Look(ref ResearchBiosculptingHiTechResearchBench, "researchBiosculptingHiTechResearchBench", Constants.VanillaResearchBiosculptingHiTechResearchBench, forceSave: true);
            Scribe_Values.Look(ref ResearchBiosculptingMultiAnalyzer, "researchBiosculptingMultiAnalyzer", Constants.VanillaResearchBiosculptingMultiAnalyzer, forceSave: true);
            Scribe_Values.Look(ref ResearchBioregenerationHiTechResearchBench, "researchBioregenerationHiTechResearchBench", Constants.VanillaResearchBioregenerationHiTechResearchBench, forceSave: true);
            Scribe_Values.Look(ref ResearchBioregenerationMultiAnalyzer, "researchBioregenerationMultiAnalyzer", Constants.VanillaResearchBioregenerationMultiAnalyzer, forceSave: true);

            base.ExposeData();
        }

        internal void ResetToDefaults()
        {
            BiosculpterPodSpeedFactor = Constants.VanillaBiosculpterPodSpeedFactor;

            MedicCycleDays = Constants.VanillaMedicCycleDays;
            BioregenerationCycleDays = Constants.VanillaBioregenerationCycleDays;
            BioregenerationCycleMedicineUltratech = Constants.VanillaBioregenerationCycleMedicineUltratech;
            AgeReversalCycleDays = Constants.VanillaAgeReversalCycleDays;
            AgeReversalDays = Constants.VanillaAgeReversalDays;
            PleasureCycleDays = Constants.VanillaPleasureCycleDays;
            PleasureCycleMoodDays = Constants.VanillaPleasureCycleMoodDays;
            PleasureCycleMoodEffect = Constants.VanillaPleasureCycleMoodEffect;

            NutritionRequired = Constants.VanillaNutritionRequired;

            BiotuningDurationDays = Constants.VanillaBiotuningDurationDays;
            BiotunedCycleSpeedFactor = Constants.VanillaBiotunedCycleSpeedFactor;

            PowerConsumption = Constants.VanillaPowerConsumption;
            StandbyConsumption = Constants.VanillaStandbyConsumption;

            EnableBuildCostSettings = false;
            SteelCost = Constants.VanillaSteelCost;
            ComponentIndustrialCost = Constants.VanillaComponentIndustrialCost;
            PlasteelCost = Constants.VanillaPlasteelCost;
            ComponentSpacerCost = Constants.VanillaComponentSpacerCost;
            UraniumCost = Constants.VanillaUraniumCost;
            WorkToBuild = Constants.VanillaWorkToBuild;

            ResearchBaseCostBiosculpting = Constants.VanillaResearchBaseCostBiosculpting;
            ResearchBaseCostBioregeneration = Constants.VanillaResearchBaseCostBioregeneration;
            ResearchLevelBiosculpting = Constants.VanillaResearchLevelBiosculpting;
            ResearchLevelBioregeneration = Constants.VanillaResearchLevelBioregeneration;
            ResearchBiosculptingHiTechResearchBench = Constants.VanillaResearchBiosculptingHiTechResearchBench;
            ResearchBiosculptingMultiAnalyzer = Constants.VanillaResearchBiosculptingMultiAnalyzer;
            ResearchBioregenerationHiTechResearchBench = Constants.VanillaResearchBioregenerationHiTechResearchBench;
            ResearchBioregenerationMultiAnalyzer = Constants.VanillaResearchBioregenerationMultiAnalyzer;
        }

        internal void ClampValues()
        {
            BiosculpterPodSpeedFactor = Mathf.Clamp(BiosculpterPodSpeedFactor, Constants.BiosculpterPodSpeedFactorMin, Constants.BiosculpterPodSpeedFactorMax);
            MedicCycleDays = Mathf.Clamp(MedicCycleDays, 0, Constants.CycleDaysMax);
            BioregenerationCycleDays = Mathf.Clamp(BioregenerationCycleDays, 0, Constants.CycleDaysMax);
            BioregenerationCycleMedicineUltratech = Mathf.Clamp(BioregenerationCycleMedicineUltratech, 0, Constants.MedicineUltratechMax);
            AgeReversalCycleDays = Mathf.Clamp(AgeReversalCycleDays, 0, Constants.CycleDaysMax);
            AgeReversalDays = Mathf.Clamp(AgeReversalDays, 0, Constants.LongDaysMax);
            PleasureCycleDays = Mathf.Clamp(PleasureCycleDays, 0, Constants.CycleDaysMax);
            PleasureCycleMoodDays = Mathf.Clamp(PleasureCycleMoodDays, 0, Constants.CycleDaysMax);
            PleasureCycleMoodEffect = Mathf.Clamp(PleasureCycleMoodEffect, 0f, Constants.MoodEffectMax);

            NutritionRequired = Mathf.Clamp(NutritionRequired, 0f, Constants.NutritionRequiredMax);

            BiotuningDurationDays = Mathf.Clamp(BiotuningDurationDays, 0, Constants.LongDaysMax);
            BiotunedCycleSpeedFactor = Mathf.Clamp(BiotunedCycleSpeedFactor, 0f, Constants.BiotunedCycleSpeedFactorMax);

            PowerConsumption = Mathf.Clamp(PowerConsumption, 0f, Constants.PowerMax);
            StandbyConsumption = Mathf.Clamp(StandbyConsumption, 0f, Constants.PowerMax);

            SteelCost = Mathf.Clamp(SteelCost, 0, Constants.ItemCostMax);
            ComponentIndustrialCost = Mathf.Clamp(ComponentIndustrialCost, 0, Constants.ItemCostMax);
            PlasteelCost = Mathf.Clamp(PlasteelCost, 0, Constants.ItemCostMax);
            ComponentSpacerCost = Mathf.Clamp(ComponentSpacerCost, 0, Constants.ItemCostMax);
            UraniumCost = Mathf.Clamp(UraniumCost, 0, Constants.ItemCostMax);
            WorkToBuild = Mathf.Clamp(WorkToBuild, 0, Constants.WorkToBuildMax);

            ResearchBaseCostBiosculpting = Mathf.Clamp(ResearchBaseCostBiosculpting, 0, Constants.ResearchCostMax);
            ResearchBaseCostBioregeneration = Mathf.Clamp(ResearchBaseCostBioregeneration, 0, Constants.ResearchCostMax);
            ResearchLevelBiosculpting = ClampTechLevel(ResearchLevelBiosculpting);
            ResearchLevelBioregeneration = ClampTechLevel(ResearchLevelBioregeneration);

            EnforceResearchPrerequisites();
        }

        private static TechLevel ClampTechLevel(TechLevel level)
        {
            if (level < TechLevel.Industrial)
                return TechLevel.Industrial;
            if (level > TechLevel.Ultra)
                return TechLevel.Ultra;
            return level;
        }

        internal void EnforceResearchPrerequisites()
        {
            if (ResearchLevelBiosculpting > ResearchLevelBioregeneration)
                ResearchLevelBioregeneration = ResearchLevelBiosculpting;
            if (ResearchBiosculptingHiTechResearchBench)
                ResearchBioregenerationHiTechResearchBench = true;
            if (ResearchBiosculptingMultiAnalyzer)
                ResearchBioregenerationMultiAnalyzer = true;
            if (ResearchLevelBiosculpting > TechLevel.Industrial)
                ResearchBiosculptingHiTechResearchBench = true;
            if (ResearchLevelBioregeneration > TechLevel.Industrial)
                ResearchBioregenerationHiTechResearchBench = true;
            if (ResearchLevelBiosculpting > TechLevel.Spacer)
                ResearchBiosculptingMultiAnalyzer = true;
            if (ResearchLevelBioregeneration > TechLevel.Spacer)
                ResearchBioregenerationMultiAnalyzer = true;
            if (!ResearchBiosculptingHiTechResearchBench)
                ResearchBiosculptingMultiAnalyzer = false;
            if (!ResearchBioregenerationHiTechResearchBench)
                ResearchBioregenerationMultiAnalyzer = false;
        }
    }
}

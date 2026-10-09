using RimWorld;
using System.Collections.Generic;
using Verse;

namespace FasterBiosculpterPod
{
    [HotSwappable]
    public class Settings : ModSettings
    {
        public float MedicCycleDays = Constants.VanillaMedicCycleDays;
        public float BioregenerationCycleDays = Constants.VanillaBioregenerationCycleDays;
        public float BioregenerationCycleMedicineUltratech = Constants.VanillaBioregenerationCycleMedicineUltratech;
        public float AgeReversalCycleDays = Constants.VanillaAgeReversalCycleDays;
        public float AgeReversalTicks = Constants.VanillaAgeReversalTicks;
        public float AgeReversalDays = Constants.VanillaAgeReversalTicks / Constants.TicksPerDay;
        public float PleasureCycleDays = Constants.VanillaPleasureCycleDays;
        public float PleasureCycleMoodDays = Constants.VanillaPleasureCycleMoodDays;
        public float PleasureCycleMoodEffect = Constants.VanillaPleasureCycleMoodEffect;

        public float NutritionRequired = Constants.VanillaNutritionRequired;

        public float BiotuningDurationTicks = Constants.VanillaBiotuningDurationTicks;
        public float BiotuningDurationDays = Constants.VanillaBiotuningDurationTicks / Constants.TicksPerDay;
        public float BiotunedCycleSpeedFactor = Constants.VanillaBiotunedCycleSpeedFactor;

        public float PowerConsumption = Constants.VanillaPowerConsumption;
        public float StandbyConsumption = Constants.VanillaStandbyConsumption;

        public bool EnableBuildCostSettings = false;
        public List<ThingDefCountClass> InitialCostList;
        public float InitialWorkToBuild;
        public float SteelCost = Constants.VanillaSteelCost;
        public float ComponentIndustrialCost = Constants.VanillaComponentIndustrialCost;
        public float PlasteelCost = Constants.VanillaPlasteelCost;
        public float ComponentSpacerCost = Constants.VanillaComponentSpacerCost;
        public float UraniumCost = Constants.VanillaUraniumCost;
        public float WorkToBuild = Constants.VanillaWorkToBuild;

        public float ResearchBaseCostBiosculpting = Constants.VanillaResearchBaseCostBiosculpting;
        public float ResearchBaseCostBioregeneration = Constants.VanillaResearchBaseCostBioregeneration;
        public TechLevel ResearchLevelBiosculpting = Constants.VanillaResearchLevelBiosculpting;
        public TechLevel ResearchLevelBioregeneration = Constants.VanillaResearchLevelBioregeneration;
        public bool ResearchBiosculptingHiTechResearchBench = Constants.VanillaResearchBiosculptingHiTechResearchBench;
        public bool ResearchBiosculptingMultiAnalyzer = Constants.VanillaResearchBiosculptingMultiAnalyzer;
        public bool ResearchBioregenerationHiTechResearchBench = Constants.VanillaResearchBioregenerationHiTechResearchBench;
        public bool ResearchBioregenerationMultiAnalyzer = Constants.VanillaResearchBioregenerationMultiAnalyzer;

        public bool UseQuadrumsForDuration = true;
        public bool UseHoursForDuration = true;
        
        public override void ExposeData()
        {
            Scribe_Values.Look(ref MedicCycleDays, "medicCycleDays", Constants.VanillaMedicCycleDays, forceSave: true);

            Scribe_Values.Look(ref BioregenerationCycleDays, "bioregenerationCycleDays", Constants.VanillaBioregenerationCycleDays, forceSave: true);
            Scribe_Values.Look(ref BioregenerationCycleMedicineUltratech, "bioregenerationCycleMedicineUltratech", Constants.VanillaBioregenerationCycleMedicineUltratech, forceSave: true);

            Scribe_Values.Look(ref AgeReversalCycleDays, "ageReversalCycleDays", Constants.VanillaAgeReversalCycleDays, forceSave: true);
            Scribe_Values.Look(ref AgeReversalTicks, "ageReversalTicks", Constants.VanillaAgeReversalTicks, forceSave: true); // Deprecated
            Scribe_Values.Look(ref AgeReversalDays, "ageReversalDays", AgeReversalTicks / Constants.TicksPerDay, forceSave: true);

            Scribe_Values.Look(ref PleasureCycleDays, "pleasureCycleDays", Constants.VanillaPleasureCycleDays, forceSave: true);
            Scribe_Values.Look(ref PleasureCycleMoodDays, "pleasureCycleMoodDays", Constants.VanillaPleasureCycleMoodDays, forceSave: true);
            Scribe_Values.Look(ref PleasureCycleMoodEffect, "pleasureCycleMoodEffect", Constants.VanillaPleasureCycleMoodEffect, forceSave: true);

            Scribe_Values.Look(ref NutritionRequired, "nutritionRequired", Constants.VanillaNutritionRequired, forceSave: true);

            Scribe_Values.Look(ref BiotuningDurationTicks, "biotuningDurationTicks", Constants.VanillaBiotuningDurationTicks, forceSave: true); // Deprecated
            Scribe_Values.Look(ref BiotuningDurationDays, "biotuningDurationDays", BiotuningDurationTicks / Constants.TicksPerDay, forceSave: true);
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

            Scribe_Values.Look(ref UseQuadrumsForDuration, "useQuadrumsForDuration", true, forceSave: true);
            Scribe_Values.Look(ref UseHoursForDuration, "useHoursForDuration", true, forceSave: true);

            base.ExposeData();
        }
    }
}

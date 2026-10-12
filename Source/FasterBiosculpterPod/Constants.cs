using RimWorld;

namespace FasterBiosculpterPod
{
    internal static class Constants
    {
        public const int TicksPerDay = 60000;

        // Ticks in which a cycle with a configured duration of 0 completes (same as the game's dev tools)
        public const float InstantCycleTicks = 10f;

        public const int CycleDaysMax = 60;
        public const int LongDaysMax = 840;
        public const int ItemCostMax = 999;
        public const int WorkToBuildMax = 99999;
        public const int ResearchCostMax = 9999;
        public const int MedicineUltratechMax = 20;
        public const float NutritionRequiredMax = 60f;
        public const float PowerMax = 10000f;
        public const float BiotunedCycleSpeedFactorMax = 10f;
        public const float MoodEffectMax = 100f;

        public const float VanillaBiosculpterPodSpeedFactor = 1f;
        public const float BiosculpterPodSpeedFactorMin = 0.1f;
        public const float BiosculpterPodSpeedFactorMax = 999f;

        public const int VanillaMedicCycleDays = 6;

        public const int VanillaBioregenerationCycleDays = 25;
        public const int VanillaBioregenerationCycleMedicineUltratech = 2;

        public const int VanillaAgeReversalCycleDays = 8;
        public const int VanillaAgeReversalTicks = 3600000;
        public const int VanillaAgeReversalDays = VanillaAgeReversalTicks / TicksPerDay;

        public const int VanillaPleasureCycleDays = 4;
        public const int VanillaPleasureCycleMoodDays = 12;
        public const float VanillaPleasureCycleMoodEffect = 15f;

        public const float VanillaNutritionRequired = 5f;

        public const int VanillaBiotuningDurationTicks = 4800000;
        public const int VanillaBiotuningDurationDays = VanillaBiotuningDurationTicks / TicksPerDay;
        public const float VanillaBiotunedCycleSpeedFactor = 1.25f;

        public const float VanillaPowerConsumption = 200f;
        public const float VanillaStandbyConsumption = 50f;

        public const int VanillaSteelCost = 120;
        public const int VanillaComponentIndustrialCost = 4;
        public const int VanillaPlasteelCost = 0;
        public const int VanillaComponentSpacerCost = 0;
        public const int VanillaUraniumCost = 0;
        public const int VanillaWorkToBuild = 28000;

        public const int VanillaResearchBaseCostBiosculpting = 1500;
        public const int VanillaResearchBaseCostBioregeneration = 4000;
        public const TechLevel VanillaResearchLevelBiosculpting = TechLevel.Industrial;
        public const TechLevel VanillaResearchLevelBioregeneration = TechLevel.Industrial;
        public const bool VanillaResearchBiosculptingHiTechResearchBench = true;
        public const bool VanillaResearchBiosculptingMultiAnalyzer = false;
        public const bool VanillaResearchBioregenerationHiTechResearchBench = true;
        public const bool VanillaResearchBioregenerationMultiAnalyzer = true;
    }
}

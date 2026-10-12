using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Verse;

namespace FasterBiosculpterPod
{
    [StaticConstructorOnStartup]
    public static class SettingsUtils
    {
        static SettingsUtils()
        {
            DeepCopyCostList(DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).costList, out FasterBiosculpterPod.settings.InitialCostList);
            FasterBiosculpterPod.settings.InitialWorkToBuild = DefDatabase<ThingDef>.GetNamed("BiosculpterPod", true).statBases.Find(x => x.stat == StatDefOf.WorkToBuild).value;
        }

        internal static void ApplySettings(Settings settings)
        {
            Harmony harmony = new Harmony("Inglix.FasterBiosculpterPod");
            harmony.UnpatchAll("Inglix.FasterBiosculpterPod");
            harmony.PatchAll();

            var bioPod = ThingDef.Named("BiosculpterPod");
            if(bioPod == null)
            {
                Log.Error("Faster Biosculpter Pod: Could not find BiosculpterPod to patch.");
                return;
            }

            var speedFactorStat = bioPod.statBases.Find(x => x.stat == StatDefOf.BiosculpterPodSpeedFactor);
            if (speedFactorStat != null)
            {
                speedFactorStat.value = settings.BiosculpterPodSpeedFactor;
            }

            if (TryFindMedicCycle(bioPod, out var healingCycle))
            {
                healingCycle.durationDays = CycleDurationDays(settings.MedicCycleDays);

            }

            if (TryFindRegenerationCycle(bioPod, out var regenerationCycle))
            {
                regenerationCycle.durationDays = CycleDurationDays(settings.BioregenerationCycleDays);

                if (regenerationCycle.extraRequiredIngredients == null)
                    regenerationCycle.extraRequiredIngredients = new List<ThingDefCountClass>();
                //remove base medicine cost only (in case of mods)
                regenerationCycle.extraRequiredIngredients.RemoveAll(tc => tc.thingDef == ThingDefOf.MedicineUltratech);

                if (settings.BioregenerationCycleMedicineUltratech > 0f)
                {
                    ThingDefCountClass ultratechMedicine = new ThingDefCountClass(ThingDefOf.MedicineUltratech, (int)settings.BioregenerationCycleMedicineUltratech);
                    regenerationCycle.extraRequiredIngredients.Add(ultratechMedicine);
                }
            }

            if (TryFindPleasureCycle(bioPod, out var pleasureCycle))
            {
                pleasureCycle.durationDays = CycleDurationDays(settings.PleasureCycleDays);

                ThoughtDefOf.BiosculpterPleasure.durationDays = settings.PleasureCycleMoodDays;
                ThoughtDefOf.BiosculpterPleasure.stages[0].baseMoodEffect = settings.PleasureCycleMoodEffect;
            }

            if (TryFindAgeReversalCycle(bioPod, out var ageReversalCycle))
            {
                ageReversalCycle.durationDays = CycleDurationDays(settings.AgeReversalCycleDays);

                if (ModsConfig.IsActive("Troopersmith1.AgeMatters"))
                {
                    Log.Warning("Age Matters mod adds a custom version of CompProperties_BiosculpterPod_AgeReversalCycle instead of patching the original. In order to apply settings for the age reversal cycle a transpiler must be run against their custom CycleCompleted method.");
                    Type ageReversalCycleType = Type.GetType("AgeMatters.CompBiosculpterPod_AgeReversalCycle,AgeMatters");
                    Log.Message("AGE REVERSAL CYCLE TYPE FOUND: " + ageReversalCycleType.FullName);
                    Type harmonyClassType = typeof(HarmonyPatches.CycleCompletedTranspiler);
                    Log.Message("HARMONY CLASS TYPE FOUND: " + harmonyClassType.FullName);
                    MethodInfo transpiler = harmonyClassType.GetMethod("Transpiler", BindingFlags.Static | BindingFlags.NonPublic);
                    Log.Message("TRANSPILER FOUND: " + transpiler.Name);
                    harmony.Patch(ageReversalCycleType.GetMethod("CycleCompleted"), transpiler: new HarmonyMethod(transpiler));
                }
            }

            CompProperties_Power power = bioPod.comps.Find(x => x.GetType() == typeof(CompProperties_Power)) as CompProperties_Power;
            AccessTools.Field(typeof(CompProperties_Power), "basePowerConsumption").SetValue(power, settings.PowerConsumption);
            AccessTools.Field(typeof(CompProperties_Power), "idlePowerDraw").SetValue(power, settings.StandbyConsumption);

            var podComp = bioPod.comps.Find(x => x.GetType() == typeof(CompProperties_BiosculpterPod)) as CompProperties_BiosculpterPod;
            podComp.biotunedCycleSpeedFactor = settings.BiotunedCycleSpeedFactor;

            if (settings.EnableBuildCostSettings)
            {
                bioPod.costList.Clear();
                UpdateCostListItem(bioPod, settings.SteelCost, ThingDefOf.Steel);
                UpdateCostListItem(bioPod, settings.ComponentIndustrialCost, ThingDefOf.ComponentIndustrial);
                UpdateCostListItem(bioPod, settings.PlasteelCost, ThingDefOf.Plasteel);
                UpdateCostListItem(bioPod, settings.ComponentSpacerCost, ThingDefOf.ComponentSpacer);
                UpdateCostListItem(bioPod, settings.UraniumCost, ThingDefOf.Uranium);
                bioPod.statBases.Find(x => x.stat == StatDefOf.WorkToBuild).value = settings.WorkToBuild;
            }
            else
            {
                RestoreInitialCostList(bioPod, settings);
            }

            ResearchProjectDef biosculpting = DefDatabase<ResearchProjectDef>.GetNamed("Biosculpting", true);
            biosculpting.baseCost = settings.ResearchBaseCostBiosculpting;
            biosculpting.techLevel = settings.ResearchLevelBiosculpting;
            UpdateResearchPrerequisites(settings.ResearchBiosculptingHiTechResearchBench, settings.ResearchBiosculptingMultiAnalyzer, biosculpting);
            ResearchProjectDef bioregeneration = DefDatabase<ResearchProjectDef>.GetNamed("Bioregeneration", true);
            bioregeneration.baseCost = settings.ResearchBaseCostBioregeneration;
            bioregeneration.techLevel = settings.ResearchLevelBioregeneration;
            UpdateResearchPrerequisites(settings.ResearchBioregenerationHiTechResearchBench, settings.ResearchBioregenerationMultiAnalyzer, bioregeneration);
            RegenerateResearchTree();
        }

        private static void UpdateResearchPrerequisites(bool hiTechResearchBench, bool multiAnalyzer, ResearchProjectDef researchProject)
        {
            if (hiTechResearchBench)
            {
                if (researchProject.requiredResearchBuilding == null)
                {
                    researchProject.requiredResearchBuilding = ThingDef.Named("HiTechResearchBench");
                }
            }
            else
            {
                researchProject.requiredResearchBuilding = null;
            }
            if (multiAnalyzer)
            {
                if (researchProject.requiredResearchFacilities == null)
                {
                    researchProject.requiredResearchFacilities = new List<ThingDef>();
                }
                if (!researchProject.requiredResearchFacilities.Contains(ThingDef.Named("MultiAnalyzer")))
                {
                    researchProject.requiredResearchFacilities.Add(ThingDef.Named("MultiAnalyzer"));
                }
                if (!researchProject.prerequisites.Contains(ResearchProjectDef.Named("MultiAnalyzer")))
                {
                    researchProject.prerequisites.Add(ResearchProjectDef.Named("MultiAnalyzer"));
                }
            }
            else
            {
                if (!researchProject.requiredResearchFacilities.NullOrEmpty() && researchProject.requiredResearchFacilities.Contains(ThingDef.Named("MultiAnalyzer")))
                {
                    researchProject.requiredResearchFacilities.Remove(ThingDef.Named("MultiAnalyzer"));
                }
                if (researchProject.prerequisites.Contains(ResearchProjectDef.Named("MultiAnalyzer")))
                {
                    researchProject.prerequisites.Remove(ResearchProjectDef.Named("MultiAnalyzer"));
                }
            }
        }

        private static void RegenerateResearchTree()
        {
            if (ModsConfig.IsActive("VinaLx.ResearchPalForked"))
            {
                Type tree = Type.GetType("ResearchPal.Tree,ResearchTree");
                MethodInfo resetLayout = tree.GetMethod("ResetLayout");
                var task = Task.Run(() => resetLayout.Invoke(null, null));
            }
        }

        private static void UpdateCostListItem(ThingDef bioPod, float setting, ThingDef thingDef)
        {
            if (setting > 0f)
            {
                if (bioPod.costList.Exists(x => x.thingDef == thingDef))
                {
                    bioPod.costList.Find(x => x.thingDef == thingDef).count = (int)setting;
                }
                else
                {
                    bioPod.costList.Add(new ThingDefCountClass(thingDef, (int)setting));
                }
            }
            else
            {
                if (bioPod.costList.Exists(x => x.thingDef == thingDef))
                {
                    bioPod.costList.RemoveAll(x => x.thingDef == thingDef);
                }
            }
        }

        private static void RestoreInitialCostList(ThingDef bioPod, Settings settings)
        {
            bioPod.costList.Clear();
            DeepCopyCostList(settings.InitialCostList, out bioPod.costList);
            bioPod.statBases.Find(x => x.stat == StatDefOf.WorkToBuild).value = settings.InitialWorkToBuild;
        }

        public static void DeepCopyCostList(List<ThingDefCountClass> originalList, out List<ThingDefCountClass> newList)
        {
            newList = new List<ThingDefCountClass>();
            foreach (ThingDefCountClass oldCost in originalList)
            {
                ThingDefCountClass newCost = new ThingDefCountClass(oldCost.thingDef, oldCost.count);
                newList.Add(newCost);
            }
        }

        internal static int ConvertDaysToTicks(float days)
        {
            return ((int)(days * 60000f));
        }

        private static float CycleDurationDays(int days)
        {
            // A configured duration of 0 completes the cycle almost instantly (the game's dev tools use 10 ticks).
            // The small margin keeps the days-to-ticks float conversion from landing on 9 ticks.
            return days > 0 ? days : (Constants.InstantCycleTicks + 0.25f) / Constants.TicksPerDay;
        }

        public static bool TryFindMedicCycle(ThingDef bioPod, out CompProperties_BiosculpterPod_BaseCycle cycle)
        {
            var medicCycle = bioPod.comps.Find(x => typeof(CompBiosculpterPod_MedicCycle).IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_BaseCycle;
            if (medicCycle == null && ModsConfig.IsActive("sambucher.selectivebioregeneration"))
            {
                Type targetedMedicCycleType = Type.GetType("Selective_Bioregeneration.CompBiosculpterPod_TargetedMedicCycle,Selective Bioregeneration");
                if (targetedMedicCycleType != null)
                    medicCycle = bioPod.comps.Find(x => targetedMedicCycleType.IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_BaseCycle;
            }
            cycle = medicCycle;
            if (medicCycle == null)
                return false;
            else
                return true;
        }

        public static bool TryFindRegenerationCycle(ThingDef bioPod, out CompProperties_BiosculpterPod_BaseCycle cycle)
        {
            var regenerationCycle = bioPod.comps.Find(x => typeof(CompBiosculpterPod_RegenerationCycle).IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_BaseCycle;
            if (regenerationCycle == null && ModsConfig.IsActive("sambucher.selectivebioregeneration"))
            {
                Type targetedRegenCycleType = Type.GetType("Selective_Bioregeneration.CompBiosculpterPod_TargetedRegenerationCycle,Selective Bioregeneration");
                if (targetedRegenCycleType != null)
                    regenerationCycle = bioPod.comps.Find(x => targetedRegenCycleType.IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_BaseCycle;
            }
            cycle = regenerationCycle;
            if (regenerationCycle == null)
                return false;
            else
                return true;
        }

        public static bool TryFindPleasureCycle(ThingDef bioPod, out CompProperties_BiosculpterPod_BaseCycle cycle)
        {
            var pleasureCycle = bioPod.comps.Find(x => typeof(CompBiosculpterPod_PleasureCycle).IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_BaseCycle;
            cycle = pleasureCycle;
            if (pleasureCycle == null)
                return false;
            else
                return true;
        }

        public static bool TryFindAgeReversalCycle(ThingDef bioPod, out CompProperties_BiosculpterPod_BaseCycle cycle)
        {
            var ageReversalCycle = bioPod.comps.Find(x => typeof(CompBiosculpterPod_AgeReversalCycle).IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_BaseCycle;
            if (ageReversalCycle == null && ModsConfig.IsActive("Troopersmith1.AgeMatters"))
            {
                Type ageMattersAgeReversalCycleType = Type.GetType("AgeMatters.CompBiosculpterPod_AgeReversalCycle,AgeMatters");
                if (ageMattersAgeReversalCycleType != null)
                    ageReversalCycle = bioPod.comps.Find(x => ageMattersAgeReversalCycleType.IsAssignableFrom(x.compClass)) as CompProperties_BiosculpterPod_BaseCycle;
            }

            cycle = ageReversalCycle;
            if (ageReversalCycle == null)
                return false;
            else
                return true;
        }
    }
}
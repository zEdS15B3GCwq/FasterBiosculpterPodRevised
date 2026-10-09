using RimWorld;
using System;
using System.Collections.Generic;
using System.IO;
using Verse;

namespace FasterBiosculpterPod
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class HotSwappableAttribute : Attribute
    {
    }

    [HotSwappable]
    public class FasterBiosculpterPod : Mod
    {
        public static Settings settings;

        public static string glitterworldMedicineLabel;
        public static string biosculptingResearchLabel;
        public static string bioregenerationResearchLabel;
        public static string steelLabel;
        public static string plasteelLabel;
        public static string componentLabel;
        public static string advancedComponentLabel;
        public static string uraniumLabel;
        public static string workToBuildLabel;

        public static Dictionary<String, TechLevel> techLevels;

        public FasterBiosculpterPod(ModContentPack content) : base(content)
        {
            settings = GetSettings<Settings>();
            if (!File.Exists(Path.Combine(GenFilePaths.ConfigFolderPath, GenText.SanitizeFilename(string.Format("Mod_{0}_{1}.xml", Content.FolderName, GetType().Name)))))
            {
                settings.Write();
            }
        }
    }
}

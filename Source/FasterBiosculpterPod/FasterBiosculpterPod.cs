using System;
using System.IO;
using UnityEngine;
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

        public FasterBiosculpterPod(ModContentPack content) : base(content)
        {
            settings = GetSettings<Settings>();
            if (!File.Exists(Path.Combine(GenFilePaths.ConfigFolderPath, GenText.SanitizeFilename(string.Format("Mod_{0}_{1}.xml", Content.FolderName, GetType().Name)))))
            {
                settings.Write();
            }
        }

        public override string SettingsCategory()
        {
            return "Inglix.Faster_Biosculpter_Pod".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoSettingsWindowContents(inRect);
            base.DoSettingsWindowContents(inRect);
        }

        public override void WriteSettings()
        {
            settings.ClampValues();
            settings.ClearNumericBuffers();
            base.WriteSettings();
            SettingsUtils.ApplySettings(settings);
        }
    }
}

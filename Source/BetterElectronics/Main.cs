using System;
using System.Xml;
using Verse;
using RimWorld;
using HugsLib;
using HugsLib.Settings;
using HarmonyLib;

namespace BetterElectronics
{
    [EarlyInit]
    public class BetterElectronics : ModBase
    {
        public override string ModIdentifier => "BetterElectronics";

        public static SettingHandle<bool> no_breakdowns_enabled;
        public static SettingHandle<bool> no_solar_flare_enabled;
        public static SettingHandle<bool> no_short_circuits_enabled;
        
        public override void EarlyInitalize()
        {
            no_breakdowns_enabled = GetSettingHandle<bool>("no_breakdowns_enabled", "NoBreakdowns.Enabled", true);
            no_solar_flare_enabled = GetSettingHandle<bool>("no_solar_flare_enabled", "NoSolarFlare.Enabled", true);
            no_short_circuits_enabled = GetSettingHandle<bool>("no_short_circuits_enabled", "NoShortCircuits.Enabled", true);
        }
        
        public override void Initialize()
        {
            TranslateSetting(no_breakdowns_enabled);
            TranslateSetting(no_solar_flare_enabled);
            TranslateSetting(no_short_circuits_enabled);
        }

        private SettingHandle<T> GetSettingHandle<T>(string settingName, string def, T defaultValue)
        {
            return Settings.GetHandle<T>(settingName, "BetterElectronics." + def, "BetterElectronics." + def + ".Alt", defaultValue);
        }

        private void TranslateSetting<T>(SettingHandle<T> settingHandle)
        {
            settingHandle.Title = settingHandle.Title.Translate();
            settingHandle.Description = settingHandle.Description.Translate();
        }
    }

    [HarmonyPatch(typeof(CompBreakdownable), "CheckForBreakdown")]
    public static class Patch_NoBreakdown
    {
        public static bool Prefix()
        {
            return !BetterElectronics.no_breakdowns_enabled;
        }
    }

    public class IsSolarFlareEnabled : PatchOperation
    {
        protected override bool ApplyWorker(XmlDocument xml)
        {
            return !BetterElectronics.no_solar_flare_enabled;
        }
    }

    public class IsShortCircuitsEnabled : PatchOperation
    {
        protected override bool ApplyWorker(XmlDocument xml)
        {
            return !BetterElectronics.no_short_circuits_enabled;
        }
    }
}
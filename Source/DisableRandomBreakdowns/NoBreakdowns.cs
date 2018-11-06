using System;
using System.Xml;
using Verse;
using RimWorld;
using HugsLib;
using HugsLib.Settings;
using Harmony;

namespace BetterElectronics
{
    [EarlyInit]
    public class BetterElectronics : ModBase
    {
        public override string ModIdentifier => "BetterElectronics";

        public static bool no_breakdowns_enabled;
        public static bool no_solar_flare_enabled;
        public static bool no_short_circuits_enabled;
        
        public override void EarlyInitalize()
        {
            no_breakdowns_enabled = GetSetting("no_breakdowns_enabled");
            no_solar_flare_enabled = GetSetting("no_solar_flare_enabled");
            no_short_circuits_enabled = GetSetting("no_short_circuits_enabled");
        }
        
        public override void Initialize()
        {
            no_breakdowns_enabled = Settings.GetHandle<bool>("no_breakdowns_enabled", "BetterElectronics.NoBreakdowns.Enabled".Translate(), "BetterElectronics.NoBreakdowns.Enabled.Alt".Translate(), true);
            no_solar_flare_enabled = Settings.GetHandle<bool>("no_solar_flare_enabled", "BetterElectronics.NoSolarFlare.Enabled".Translate(), "BetterElectronics.NoSolarFlare.Enabled.Alt".Translate(), true);
            no_short_circuits_enabled = Settings.GetHandle<bool>("no_short_circuits_enabled", "BetterElectronics.NoShortCircuits.Enabled".Translate(), "BetterElectronics.NoShortCircuits.Enabled.Alt".Translate(), true);
        }

        private bool GetSetting(string settingName)
        {
            if (Settings.ValueExists(settingName))
                return Convert.ToBoolean(Settings.PeekValue(settingName));
            return true;
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

    public class IsNoSolarFlareEnabled : PatchOperation
    {
        protected override bool ApplyWorker(XmlDocument xml)
        {
            return !BetterElectronics.no_solar_flare_enabled;
        }
    }

    public class IsNoShortCircuitsEnabled : PatchOperation
    {
        protected override bool ApplyWorker(XmlDocument xml)
        {
            return !BetterElectronics.no_short_circuits_enabled;
        }
    }
}
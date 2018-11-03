using Verse;
using RimWorld;
using HugsLib;
using HugsLib.Settings;
using Harmony;

namespace BetterElectronics
{
    [StaticConstructorOnStartup]
    public class BetterElectronics : ModBase
    {
        public override string ModIdentifier => "BetterElectronics";

        public static SettingHandle<bool> no_breakdowns_enabled;


        public override void Initialize()
        {
            no_breakdowns_enabled = Settings.GetHandle<bool>("no_breakdown_enabled", "NoBreakdowns.Enabled".Translate(), "NoBreakdowns.Enabled.Alt".Translate(), true);
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
}
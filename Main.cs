using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace ServerAuthorityFix;

[BepInAutoPlugin(PluginGuid, PluginName, PluginVersion)]
[BepInProcess("Among Us.exe")]
public partial class Main : BasePlugin
{
    public const string PluginName = "Server Authority Fix";
    public const string PluginGuid = "top.cog.serverauthorityfix";
    public const string PluginVersion = "1.0.0";
    public Harmony harmony { get; } = new(PluginGuid);
    public static ManualLogSource? Logger { get; private set; }

    public override void Load()
    {
        Logger = BepInEx.Logging.Logger.CreateLogSource(PluginName);
        
        Logger.LogInfo("Plugin loaded successfully.");
        harmony.PatchAll();
    }
    
    [HarmonyPatch(typeof(Constants), nameof(Constants.GetBroadcastVersion))]
    class FixerPatch
    {
        static void Postfix(ref int __result)
        {
            __result = Constants.GetVersion(2222, 0, 0, 0);
        }
    }
}
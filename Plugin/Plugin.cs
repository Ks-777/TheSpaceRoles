using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace TheSpaceRoles;

[BepInPlugin(Id, name, version)]
[HarmonyPatch]
[BepInProcess("Among Us.exe")]
public class TSR : BasePlugin
{
    public const string Id = "supeshio.com.github";
    public const string name = "TheSpaceRoles";
    public const string s_name = "TSR";
    public const string version = "1.0.0";
    internal static BepInEx.Logging.ManualLogSource Logger;
    public Harmony Harmony = new(Id);


    public override void Load()
    {
        Logger = Log;
        Harmony.PatchAll();
        // Plugin startup logic
        TheSpaceRoles.Logger.Info($"Plugin {Id} is loaded!");

    }
}

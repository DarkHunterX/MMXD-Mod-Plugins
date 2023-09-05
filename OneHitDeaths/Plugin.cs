using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace OneHitDeaths;

// Do not modify this line. You can change AssemblyName, Product, and Version directly in the .csproj
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public override void Load()
    {
        // Plugin startup logic
        Plugin.Log = base.Log;
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        // This line will apply all patches in this class (Plugin) when uncommented
        Harmony.CreateAndPatchAll(typeof(Plugin));
    }

    [HarmonyPatch(typeof(BattleInfoUI), nameof(BattleInfoUI.AddOrangeCharacter))]
    [HarmonyPrefix]
    static void SetPlayerHP(ref OrangeCharacter tOC)
    {
        Log.LogWarning($"Setting Player HP to 1");
        tOC.Hp = 1;
        tOC.MaxHp = 1;
    }
}

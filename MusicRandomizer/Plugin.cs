using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System;

namespace MusicRandomizer;

// Do not modify this line. You can change AssemblyName, Product, and Version directly in the .csproj
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;
    internal static BGM.Tracks bgm;

    public override void Load()
    {
        // Plugin startup logic
        Plugin.Log = base.Log;
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        // This line will apply all patches in this class (Plugin) when uncommented
        Harmony.CreateAndPatchAll(typeof(Plugin));

        // read list of songs from json
        bgm = Utils.JsonParser.Read<BGM.Tracks>("tracklist.json");
        Log.LogInfo($"Song data loaded!");
    }

    // AudioManager.PlayBGM(string s_acb, string cueName, bool stopBGM = true, forcePlay = false)
    [HarmonyPatch(typeof(AudioManager), nameof(AudioManager.PlayBGM), new Type[] { typeof(string), typeof(string), typeof(bool), typeof(bool) })]
    [HarmonyPrefix]
    public static void PlayBGM_byStr_Random(ref string s_acb, ref string cueName)
    {
        // pick random song from list and play it
        var songInfo = BGM.GetRandomSong(bgm);
        s_acb = songInfo.acb;
        cueName = songInfo.song;
        Log.LogInfo($"Playing Audio: {s_acb},{cueName}");
    }

    // AudioManager.PlayBGM(string s_acb, string cueName, bool stopBGM = true, forcePlay = false)
    [HarmonyPatch(typeof(AudioManager), nameof(AudioManager.PlayBGM), new Type[] { typeof(string), typeof(int) })]
    [HarmonyPostfix]
    public static void PlayBGM_byInt_Random(AudioManager __instance)
    {
        __instance.StopBGM();
        __instance.PlayBGM("BGM01", "bgm_blank");
    }
}

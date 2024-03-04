using HarmonyLib;
using TMPro;
using UnityEngine;

namespace TheSpaceRoles
{
    [HarmonyPatch(typeof(VersionShower), nameof(VersionShower.Start))]
    public static class StartMenu
    {

        public static void Prefix(VersionShower __instance)
        {
        }
        public static void Postfix(VersionShower __instance)
        {

            TextMeshPro AddtionalText = new GameObject("text").AddComponent<TextMeshPro>();
            AddtionalText.text = $"<color=#5ccbff> {TSR.s_name} v{TSR.version}";
            AddtionalText.fontSize = 2;
            AddtionalText.alignment = TextAlignmentOptions.Right;
            AddtionalText.enableWordWrapping = false;
            AddtionalText.sortingOrder = 0;
            AddtionalText.sortingLayerID = 0;
            AddtionalText.transform.parent = __instance.transform;
            AddtionalText.transform.localPosition = new Vector3(0, 0, 0);
            AddtionalText.tag = "UI";
            AddtionalText.transform.localScale = Vector3.one;
        }
    }
    [HarmonyPatch(typeof(PingTracker),nameof(PingTracker.Update))]
    public static class ShowTSR{

        public static void Postfix(PingTracker __instance) 
        {
            __instance.text.text = __instance.text.text + $"\n<color=#5ccbff> {TSR.s_name} v{TSR.version}";
        }
    }
}

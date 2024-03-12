using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HarmonyLib;
using AmongUs.Data.Legacy;
using Il2CppSystem.Collections.Generic.Enumerable;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace TheSpaceRoles {
    public class CustomColor
    {
        public string colorName;
        public Color mainColor;
        public Color shadowColor;
    }
    [HarmonyPatch(typeof(PlayerTab),nameof(PlayerTab.OnEnable))]
    public static class CustomColors 
    {
        public static void Postfix(PlayerTab __instance)
        {

        }
    }
}
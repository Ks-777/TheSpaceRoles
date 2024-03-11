using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSpaceRoles
{
    [HarmonyPatch(typeof(KillAnimation), nameof(KillAnimation.CoPerformKill))]
    class KillAnimationPatch
    {
        public static bool AnimCancel = false;

        public static void Prefix(KillAnimation __instance, [HarmonyArgument(0)] ref PlayerControl source, [HarmonyArgument(1)] ref PlayerControl target)
        {
            if (AnimCancel)
                source = target;
            AnimCancel = false;
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Exiled))]
    public static class PCExiled
    {
        public static void Prefix(PlayerControl __instance)
        {

            Logger.Info("P,Exiled?");
            if (__instance.PlayerId == PlayerControl.LocalPlayer.PlayerId)
            {
                DataBase.buttons.Do(x => x.Death());
            }
        }
    }
}

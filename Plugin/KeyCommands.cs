using HarmonyLib;
using UnityEngine;

namespace TheSpaceRoles.Plugin
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public class KeyCommands
    {
        public static void Postfix(global::PlayerControl __instance)
        {
            if (__instance?.Collider?.offset != null)
            {

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    if (__instance?.Collider?.offset == null) return;
                    __instance.Collider.offset = new Vector2(0, 65536f);
                }
                else
                {
                    __instance.Collider.offset = new Vector2(0, 0f);
                }
            }

        }
    }
}

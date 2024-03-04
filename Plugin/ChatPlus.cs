using HarmonyLib;
using UnityEngine;

namespace TheSpaceRoles
{
    [HarmonyPatch(typeof(ChatController), nameof(ChatController.Update))]
    public class ChatPlus
    {
        public static void Prefix(ChatController __instance)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    GUIUtility.systemCopyBuffer = __instance.freeChatField.textArea.text;
                }
                if (Input.GetKeyDown(KeyCode.V))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        AddChat(__instance, GUIUtility.systemCopyBuffer);
                    }
                    else
                    {
                        __instance.freeChatField.textArea.text = GUIUtility.systemCopyBuffer;

                    }
                }


            }
        }


        public static void OtherPlayerAddChat(ChatController __instance, global::PlayerControl sourceplayer, string str)
        {
            __instance.AddChat(sourceplayer, str);
        }
        public static void AddChat(ChatController __instance, string str)
        {
            __instance.AddChat(global::PlayerControl.LocalPlayer, str);
        }

    }
}

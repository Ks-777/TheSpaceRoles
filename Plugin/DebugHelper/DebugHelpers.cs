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


        public static void OtherPlayerAddChat(ChatController __instance, PlayerControl sourceplayer, string str)
        {
            __instance.AddChat(sourceplayer, str);
        }
        public static void AddChat(ChatController __instance, string str)
        {
            __instance.AddChat(PlayerControl.LocalPlayer, str);
        }

    }
    [HarmonyPatch(typeof(HudManager), "Update")]
    public static class EnableChat
    {
        public static void Postfix(HudManager __instance)
        {
            //IL_001a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0020: Invalid comparison between Unknown and I4
            if (!__instance.Chat.isActiveAndEnabled)
            {
                AmongUsClient instance = AmongUsClient.Instance;
                if (instance != null && (int)instance.NetworkMode == 2)
                {
                    __instance.Chat.SetVisible(true);
                }
            }
        }
    }

}

using HarmonyLib;

namespace TheSpaceRoles
{
    [HarmonyPatch]
    public class ChatPlus
    {
        public static string chattext = "";
        [HarmonyPatch(typeof(ChatController), nameof(ChatController.Update))]
        public static class Chatconst
        {

            public static void Postfix(ChatController __instance)
            {
                __instance.freeChatField.textArea.characterLimit = 120;
                chattext = __instance.freeChatField.textArea.text;
            }
        }


    }
}

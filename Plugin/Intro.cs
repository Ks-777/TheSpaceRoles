using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;

namespace TheSpaceRoles
{
    [HarmonyPatch(typeof(IntroCutscene),nameof(IntroCutscene.ShowTeam))]
    public static class IntroShowTeam
    {
        public static void Prefix(IntroCutscene __instance, ref Il2CppSystem.Collections.IEnumerator __result)
        {

        }
        public static void Postfix(IntroCutscene __instance)
        {

            __instance.TeamTitle.text = "TeamTitle";
        }
    }
    [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.ShowRole))]
    public static class IntroShowRole
    {
        public static void Prefix(IntroCutscene __instance)
        {

            __instance.RoleBlurbText.gameObject.SetActive(false);
        }
        public static void Postfix(IntroCutscene __instance)
        {
            __instance.RoleBlurbText.color = DataBase.AllPlayerRoles[PlayerControl.LocalPlayer.PlayerId][0].Color;
            __instance.RoleBlurbText.text = "RoleBlurbText";
            __instance.RoleText.text = "RoleText";

        }
    }

    public static class Tmpro
    {
        public static void text(IntroCutscene __instance,TextMeshPro tmp,string str) 
        {
            __instance.StartCoroutine(Effects.Lerp(0.1f, new Action<float>((p) =>
            {
                SetRoleTexts(__instance);
            })));
        }
        public static void SetRoleTexts(IntroCutscene __instance)
        {

            __instance.RoleBlurbText.color = DataBase.AllPlayerRoles[PlayerControl.LocalPlayer.PlayerId][0].Color;
            __instance.RoleBlurbText.text = "RoleBlurbText";
            __instance.RoleText.text = "RoleText";
        }
    }
}

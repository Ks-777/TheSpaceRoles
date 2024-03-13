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


            __instance.StartCoroutine(Effects.Lerp(0.1f, new Action<float>((p) =>
            {

                __instance.TeamTitle.color = RoleLink.ColorFromTeams[DataBase.AllPlayerTeams[PlayerControl.LocalPlayer.PlayerId]];
                __instance.TeamTitle.text = Translation.GetString($"team.{DataBase.AllPlayerTeams[PlayerControl.LocalPlayer.PlayerId]}.name");

            })));
        }
    }
    [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.ShowRole))]
    public static class IntroShowRole
    {
        public static void Prefix(IntroCutscene __instance)
        {

        }
        public static void Postfix(IntroCutscene __instance)
        {
            __instance.StartCoroutine(Effects.Lerp(0.1f, new Action<float>((p) =>
            {

                __instance.RoleBlurbText.color = DataBase.AllPlayerRoles[PlayerControl.LocalPlayer.PlayerId][0].Color;
                __instance.RoleBlurbText.text = Translation.GetString($"role.{DataBase.AllPlayerRoles[PlayerControl.LocalPlayer.PlayerId][0].Role}.intro");
                __instance.RoleText.color = DataBase.AllPlayerRoles[PlayerControl.LocalPlayer.PlayerId][0].Color;
                __instance.RoleText.text = string.Join("×", DataBase.AllPlayerRoles[PlayerControl.LocalPlayer.PlayerId].Select(x => Translation.GetString($"role.{x.Role}.name")));
                __instance.YouAreText.color = DataBase.AllPlayerRoles[PlayerControl.LocalPlayer.PlayerId][0].Color;
            })));
        }

    }
}

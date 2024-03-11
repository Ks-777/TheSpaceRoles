using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TheSpaceRoles.Helper;

namespace TheSpaceRoles
{
    public abstract class RoleMaster
    {
        public Teams[] teamsSupported = Enum.GetValues(typeof(Teams)).Cast<Teams>().ToArray();
        public Roles Role;
        public Color Color;
        public bool HasKillButton = false;
        public bool HasAbilityButton = false;
        public int[] AbilityButtonType = [];
        public bool CanUseVent = true;
        public bool CanUseAdmin = true;
        public bool CanUseCamera = true;
        public bool CanUseVital = true;
        public bool CanUseDoorlog = true;
        public bool CanUseBinoculars = true;
        public bool CanRepairSabotage = true;
        public bool HasTask = true;
        public abstract void HudManagerStart(HudManager hudManager);
        public virtual void Killed() { }
        public virtual void WasKilled() { }
        public string ColoredRoleName()
        {
            return ColoredText(Color, Role.ToString());
        }

    }
    [HarmonyPatch(typeof(HudManager))]
    public static class HudManagerGame
    {
        [HarmonyPatch(nameof(HudManager.OnGameStart)),HarmonyPostfix]
        public static void ButtonCreate(HudManager __instance)
        {
            ButtonCooldownEnabled = false;
            ButtonCooldown = 10f;
            DataBase.buttons.Clear();
            Logger.Info("hudmanager start");
            if(DataBase.AllPlayerRoles.ContainsKey(PlayerControl.LocalPlayer.PlayerId))
            {
                var k = DataBase.AllPlayerRoles[PlayerControl.LocalPlayer.PlayerId].Select(x => x.Role.ToString()).ToArray();
                Logger.Info(string.Join(",", k));

                DataBase.AllPlayerRoles[PlayerControl.LocalPlayer.PlayerId].Do(x => x.HudManagerStart(__instance));
            }
            


        }
        public static float ButtonCooldown;
        public static bool ButtonCooldownEnabled;
    }



    public static class RoleMasterLink
    {

        public static List<RoleMaster> RolesMasterLink = new()
        {
            { new Sheriff()}
        };


        public static RoleMaster GetRoleMaster(Roles roles)
        {
            return RolesMasterLink.ToArray().First(x => x.Role == roles);
        }
    }
}
using HarmonyLib;
using InnerNet;
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
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.OnGameStart))]
    public static class RoleMaster_GameStart
    {

        public static void Postfix(HudManager __instance)
        {


            
                Logger.Info("hudmanager start");
            if(DataBase.AllPlayerRoles.Any(x => x.Key == PlayerControl.LocalPlayer.PlayerId))
            {
                Logger.Info(DataBase.AllPlayerRoles.First(x => x.Key == PlayerControl.LocalPlayer.PlayerId).Value.Select(x=>x.Role).ToString());

                DataBase.AllPlayerRoles.First(x => x.Key == PlayerControl.LocalPlayer.PlayerId).Value.Do(x => x.HudManagerStart(__instance));
            }
                

            


        }

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
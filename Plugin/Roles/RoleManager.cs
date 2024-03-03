using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheSpaceRoles
{
    public abstract class RoleMaster
    {
        protected Teams[] teamsSupported = Enum.GetValues(typeof(Teams)).Cast<Teams>().ToArray();
        protected Roles Role;
        protected Color Color;
        protected bool HasKillButton = false;
        protected bool HasAbilityButton = false;
        protected int[] AbilityButtonType = [];
        protected bool CanUseVent = true;
        protected bool CanUseAdmin = true;
        protected bool CanUseCamera = true;
        protected bool CanUseVital = true;
        protected bool CanUseDoorlog = true;
        protected bool CanUseBinoculars = true;
        protected bool CanRepairSabotage = true;
        protected bool HasTask = true;
        protected abstract void Start();
        protected virtual void Killed() { }
        protected virtual void WasKilled() { }
    }

    [HarmonyPatch(typeof(GameStartManager),nameof(GameStartManager.BeginGame))]
    public static class GameStart{
        public static void Prefix() 
        {
            


        }

    }
}

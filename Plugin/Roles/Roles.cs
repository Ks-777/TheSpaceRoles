using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Color = UnityEngine.Color;

namespace TheSpaceRoles
{
    public static class RolesAssets
    {


        public static void SetRole(int playerId,int roleId)
        {
            RoleInfo info = RoleInfo.CreateAsCrewmate((RoleData.Roles)roleId/*ほんとはここにデータ変換後が入る*/);
            //ここにどのroleIdがどのロールに対応するかを判定して
            //ここにinfoを作る

            //var p = PlayerControl.AllPlayerControls.ToArray().First(x => x.PlayerId == playerId).PlayerId;
            Data_change.AllData.Add(playerId,[info]);
        }
    }
    public class RoleInfo{
        public RoleData.Roles Role;
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
        private RoleInfo(
            RoleData.Roles role,
            bool hasKillButton = false,
            int[] abilityButtonType = null,
            bool canUseVent = false,
            bool canUseAdmin = true,
            bool canUseCamera = true,
            bool canUseVital = true,
            bool canUseDoorlog = true,
            bool canUseBinoculars = true,
            bool canRepairSabotage = true,
            bool hasTask = true){
            //crewmatesetting
             Role = role;
             Color = RoleData.roleColor[role];
             HasKillButton = hasKillButton;
             HasAbilityButton = abilityButtonType.Length > 0;
             AbilityButtonType = abilityButtonType;
             CanUseVent = canUseVent;
             CanUseAdmin = canUseAdmin;
             CanUseCamera = canUseCamera;
             CanUseVital = canUseVital;
             CanUseDoorlog = canUseDoorlog;
             CanUseBinoculars = canUseBinoculars;
             CanRepairSabotage= canRepairSabotage;
             HasTask= hasTask;
        }
        public static RoleInfo CreateAsCrewmate(
            RoleData.Roles role,
            bool hasKillButton = false,
            int[] abilityButtonType = null,
            bool canUseVent = false,
            bool canUseAdmin = true,
            bool canUseCamera = true,
            bool canUseVital = true,
            bool canUseDoorlog = true,
            bool canUseBinoculars = true,
            bool canRepairSabotage = true,
            bool hasTask = true
            )
        {
            return new RoleInfo(role, hasKillButton, abilityButtonType, canUseVent, canUseAdmin, canUseCamera, canUseVital, canUseDoorlog, canUseBinoculars, canRepairSabotage, hasTask);
        }
    }
    

}

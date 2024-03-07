using System;
using UnityEngine;
using static TheSpaceRoles.Helper;

namespace TheSpaceRoles
{
    public class Sheriff : RoleMaster
    {
        public static CustomButton SheriffKillButton;
        public Sheriff()
        {
            teamsSupported = [Teams.Crewmate];
            Role = Roles.Sheriff;
            Color = ColorFromColorcode("#ffd700");
            HasKillButton = true;
        }
        public override void HudManagerStart(HudManager __instance)
        {
            SheriffKillButton = new CustomButton(__instance,CustomButton.SelectButtonPos(4),KeyCode.Q,30,null,new KillButton().usesRemainingSprite.sprite,null,null,"キル",false);
        }
    }
}

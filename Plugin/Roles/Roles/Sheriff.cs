using System.Linq;
using TheSpaceRoles.Plugin.Roles;
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
            SheriffKillButton = new CustomButton(__instance, CustomButton.SelectButtonPos(4), KeyCode.Q, 30,
                () => {
                    var p = PlayerControlButtonControls.SetTarget(2.5f,Color);



                    return p;
                      



                }





            ,__instance.KillButton.graphic.sprite, ()=> { }, () => { }, "キル", false);

        }
    }
}

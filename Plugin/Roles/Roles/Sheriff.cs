using Hazel;
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
            SheriffKillButton = new CustomButton(
                __instance,
                CustomButton.SelectButtonPos(0),
                KeyCode.Q,
                30,
                () =>  PlayerControlButtonControls.SetTarget(2.5f, Color),
                __instance.KillButton.graphic.sprite,
                ()=> {
                    var pc = GetPlayerControlFromId(PlayerControlButtonControls.SetTarget(2.5f, Color));
                    MessageWriter writer = Rpc.SendRpc(Rpcs.RpcMurderPlayer);
                    if (DataBase.AllPlayerTeams[pc.PlayerId] != Teams.Crewmate)
                    {
                        RpcMurderPlayer.RpcMurder(PlayerControl.LocalPlayer,pc,DeathReason.SheriffKill);


                    }
                    else
                    {
                        RpcMurderPlayer.RpcMurder(pc,PlayerControl.LocalPlayer,DeathReason.Suicide);
                    }

                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                },
                () => SheriffKillButton.Timer = SheriffKillButton.maxTimer ,
                "Kill",
                false);

        }
    }
}

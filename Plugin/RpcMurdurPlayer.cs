using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheSpaceRoles
{
    public static class RpcMurderPlayer
    {
        public static void Murder(int id1,int id2,DeathReason reason)
        {

            
            //if (reason==DeathReason.Suicide)
            //{
            //    KillAnimationPatch.AnimCancel = true;
            //    PlayerControl.LocalPlayer.MurderPlayer(Helper.GetPlayerControlFromId(id2), MurderResultFlags.Succeeded);

            //}
            //else
            //{
            //    Helper.GetPlayerControlFromId(id1).MurderPlayer(Helper.GetPlayerControlFromId(id2), MurderResultFlags.Succeeded);

            //}
            DataBase.AllPlayerDeathReasons.Add(id2, reason);
            if (id2 == PlayerControl.LocalPlayer.PlayerId)
            {
                DataBase.buttons.Do(x => x.Death());

                Logger.Info($"Death, reason:{reason}");
            }
        }
        public static void RpcMurder(PlayerControl source,PlayerControl target,DeathReason reason)
        {
            if(reason==DeathReason.Suicide)
            {
                target.RpcMurderPlayer(target,true);
                KillAnimationPatch.AnimCancel = true;
            }
            else
            {
                source.RpcMurderPlayer(target, true);
            }
            
            Murder(source.PlayerId,target.PlayerId,reason);
            MessageWriter writer = Rpc.SendRpc(Rpcs.RpcMurderPlayer);
            writer.Write((int)source.PlayerId);
            writer.Write((int)target.PlayerId);
            writer.Write((int)reason);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
    }
}

using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TheSpaceRoles
{
    [HarmonyPatch]
    public static class Rpc
    {



        [HarmonyPatch(typeof(PlayerControl),nameof(PlayerControl.HandleRpc))]
        public static class Hud
        {
            public static void Prefix(PlayerControl __instance) 
            {

            }
            public static void Postfix(byte callId, MessageReader reader)
            {
                switch(callId) 
                {
                    case (byte)Rpcs.SetRole:
                        int playerId = reader.ReadInt32();
                        int roleId = reader.ReadInt32();
                        RolesAssets.SetRole(playerId, roleId);
                        break;
                }
            }
        }
        public static MessageWriter SendRpc(Rpcs rpc)
        {

            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)rpc, SendOption.Reliable);
            return writer;
        }
        
    }
}

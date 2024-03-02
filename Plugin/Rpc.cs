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
        public enum Rpcs:byte{
            SetRole=80,
            ChangeRole,
            GameEnd,
            SendSetting,
            UseAbility,
        }



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
                        break;
                }
            }
        }
    }
}

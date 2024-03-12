using HarmonyLib;
using Hazel;

namespace TheSpaceRoles
{
    [HarmonyPatch]
    public static class Rpc
    {



        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
        public static class Hud
        {
            public static void Prefix(PlayerControl __instance)
            {

            }
            public static void Postfix(byte callId, MessageReader reader)
            {
                if(callId >= 60)
                {
                    Logger.Info($"{(Rpcs)callId}");
                }
                switch (callId)
                {
                    case (byte)Rpcs.SetRole:
                        int r1 = reader.ReadInt32();
                        int r2 = reader.ReadInt32();
                        GameStarter.SetRole(r1, r2);
                        break;
                    case (byte)Rpcs.SetTeam:
                        int t1 = reader.ReadInt32();
                        int t2 = reader.ReadInt32();
                        GameStarter.SetTeam(t1, t2);
                        break;
                    case (byte)Rpcs.RpcMurderPlayer:
                        RpcMurderPlayer.Murder(reader.ReadInt32(),reader.ReadInt32(),(DeathReason)reader.ReadInt32());
                        break;
                    case (byte)Rpcs.DataBaseReset:
                        DataBase.Reset();
                        break;
                    case (byte)Rpcs.SendRoomTimer:
                        
                        break;
                }
            }
        }
        public static MessageWriter SendRpc(Rpcs rpc)
        {

            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(global::PlayerControl.LocalPlayer.NetId, (byte)rpc, SendOption.Reliable);
            return writer;
            
        }

    }

    public enum Rpcs : int
    {
        SetRole = 80,
        SetTeam,
        ChangeRole,
        DataBaseReset,
        SendRoomTimer,
        GameEnd,
        SendSetting,
        UseAbility,
        RpcMurderPlayer,//A kill B : A/B/Reasons
    }
    public enum DeathReason : int
    {
        Disconnected,
        VotedOut,
        ImpostorKill,
        SheriffKill,
        SheriffSuicide,
    }
    
}

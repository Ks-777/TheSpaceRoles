using HarmonyLib;
using Il2CppSystem.CodeDom;
using System;
using System.Collections.Generic;
using System.Linq;
using static TheSpaceRoles.Helper;

namespace TheSpaceRoles
{
    [HarmonyPatch(typeof(RoleManager), nameof(RoleManager.SelectRoles))]
    public static class GameStarter
    {
        public static void Prefix()
        {
            AmongUsClient.Instance.FinishRpcImmediately(Rpc.SendRpc(Rpcs.DataBaseReset));
            DataBase.Reset();
        }
        public static void Postfix()
        {
            //Resetするべ
            //今回はC3 I1
            //Sheriff 1
            //です
            if (PlayerControl.LocalPlayer.AmOwner)
            {

                Dictionary<Teams, int> roles = new Dictionary<Teams, int>() { { Teams.Impostor, 1 } };
                SendRpcSetTeam(roles);
                SendRpcSetRole(Roles.Sheriff, DataBase.AllPlayerTeams.Where(x => new Sheriff().teamsSupported.Contains(x.Value)).Select(x => x.Key).ToArray());
            }


            //いったんシェリフだけ自動的に一人に割り振ろうと思う
        }
        public static void SendRpcSetRole(Roles roles, int[] players)
        {
            if (players.Length == 0) { Logger.Warning("players ないよおおおおおお"); return; }
            //設定作ってないけどここでほんとは分岐
            //ここではmainとして扱う
            var a = players[Random(0, players.Length - 1)];
            SetRole(a, (int)roles);

            //Rpc
            var writer = Rpc.SendRpc(Rpcs.SetRole);
            writer.Write(a);
            writer.Write((int)roles);
            AmongUsClient.Instance.FinishRpcImmediately(writer);

        }

        public static void SetRole(int playerId, int roleId)
        {

            //ここにどのroleIdがどのロールに対応するかを判定して

            //var p = PlayerControl.AllPlayerControls.ToArray().First(x => x.PlayerId == playerId).PlayerId;
            if (DataBase.AllPlayerRoles.ContainsKey(playerId))
            {
                var list = DataBase.AllPlayerRoles[playerId];
                list.AddItem(RoleMasterLink.GetRoleMaster((Roles)roleId));
                DataBase.AllPlayerRoles[playerId] = list;
            }
            else
            {

                DataBase.AllPlayerRoles.Add(playerId, [RoleMasterLink.GetRoleMaster((Roles)roleId)]);

            }
            Logger.Info($"Player:{DataBase.AllPlayerControls().First(x => x.PlayerId == playerId).cosmetics.nameText.text}({playerId}) -> Role:{(Roles)roleId}");


        }
        public static void SendRpcSetTeam(Dictionary<Teams, int> teams)
        {
            List<byte> ImpIds = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Data.Role.TeamType == RoleTeamTypes.Impostor).Select(x => x.PlayerId).ToList();
            List<byte> CrewIds = PlayerControl.AllPlayerControls.ToArray().Where(x => x.Data.Role.TeamType != RoleTeamTypes.Impostor).Select(x => x.PlayerId).ToList();
            Logger.Info(string.Join("\n", PlayerControl.AllPlayerControls.ToArray().Select(x => x.Data.Role.TeamType).ToArray()));
            Logger.Info($"imp:{ImpIds.Count}  c:{CrewIds.Count}");

            foreach ((Teams teams1, int count) in teams)
            {


                if (teams1 != Teams.Crewmate && teams1 != Teams.Impostor)
                {
                    for (int i = 0; i < count; i++)
                    {

                        int r = Random(0, CrewIds.Count - 1);

                        SetTeam(CrewIds[r], (int)teams1);
                        CrewIds.RemoveAt(r);
                    }
                }
                else if (teams1 == Teams.Impostor)
                {
                    if (ImpIds.Count > count)
                    {
                        for (int i = 0; i < count - ImpIds.Count; i++)
                        {

                            int r = Random(0, ImpIds.Count - 1);

                            SetTeam(ImpIds[r], (int)teams1);

                            ImpIds.RemoveAt(r);
                        }
                    }
                    else if
                     (ImpIds.Count < count)
                    {

                        for (int i = 0; i < ImpIds.Count - count; i++)
                        {

                            int r = Random(0, ImpIds.Count - 1);

                            SetTeam(CrewIds[r], (int)teams1);

                            ImpIds.RemoveAt(r);
                        }
                    }
                }

            }
            foreach (var pId in CrewIds)
            {
                SetTeam(pId, (int)Teams.Crewmate);

            }
            foreach (var pId in ImpIds)
            {
                SetTeam(pId, (int)Teams.Impostor);

            }
            foreach (var database in DataBase.AllPlayerTeams)
            {

                var writer = Rpc.SendRpc(Rpcs.SetTeam);
                writer.Write(database.Key);
                writer.Write((int)database.Value);
                AmongUsClient.Instance.FinishRpcImmediately(writer);

            }

        }
        public static void SetTeam(int playerId, int teamId)
        {



            //ここにどのroleIdがどのロールに対応するかを判定して

            //var p = PlayerControl.AllPlayerControls.ToArray().First(x => x.PlayerId == playerId).PlayerId;
            Logger.Info($"Player:{DataBase.AllPlayerControls().First(x => x.PlayerId == playerId).cosmetics.nameText.text}({playerId}) -> Team:{(Teams)teamId}");

            DataBase.AllPlayerTeams.Add(playerId, (Teams)teamId);
        }

        public static void GameStartAndPrepare()
        {
            //RoleMaster_GameStart.GameStart();
        }
    }
}

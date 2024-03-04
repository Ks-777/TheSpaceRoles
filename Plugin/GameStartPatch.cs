using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using static TheSpaceRoles.Helper;

namespace TheSpaceRoles
{
    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.StartGame))]
    public static class GameStarter
    {
        public static void Prefix()
        {
            //Resetするべ
            DataBase.AllPlayerTeams.Clear();
            DataBase.AllPlayerRoles.Clear();
            //今回はC3 I1
            //Sheriff 1
            //です
            if (global::PlayerControl.LocalPlayer.AmOwner)
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
            List<byte> pIds = DataBase.AllPlayerControls().Select(x => x.PlayerId).ToList();

            foreach ((Teams teams1, int v) in teams)
            {
                {

                    if (teams1 != Teams.Crewmate) for (int i = 0; i < v; i++)
                        {
                            if (pIds.Count <= 0) return;//これ設定で変えたいよね
                            int r = Random(0, pIds.Count - 1);
                            SetTeam(pIds[r], (int)teams1);

                            var writer = Rpc.SendRpc(Rpcs.SetTeam);
                            writer.Write(r);
                            writer.Write((int)Teams.Crewmate);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            pIds.RemoveAt(r);
                        }
                }
            }
            foreach (var pId in pIds)
            {
                SetTeam(pId, (int)Teams.Crewmate);

                var writer = Rpc.SendRpc(Rpcs.SetTeam);
                writer.Write(pId);
                writer.Write((int)Teams.Crewmate);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
            }


        }
        public static void SetTeam(int playerId, int teamId)
        {
            //ここにどのroleIdがどのロールに対応するかを判定して

            //var p = PlayerControl.AllPlayerControls.ToArray().First(x => x.PlayerId == playerId).PlayerId;
            DataBase.AllPlayerTeams.Add(playerId, (Teams)teamId);
            Logger.Info($"Player:{DataBase.AllPlayerControls().First(x => x.PlayerId == playerId).cosmetics.nameText.text}({playerId}) -> Team:{(Teams)teamId}");
        }
    }

}

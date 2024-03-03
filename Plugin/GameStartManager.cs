using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheSpaceRoles.Helper;

namespace TheSpaceRoles
{
    [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.BeginGame))]
    public static class GameStarter
    {
        public static void Prefix(GameStartManager __instance)
        {
            //Resetするべ
            DataBase.AllPlayerTeams.Clear();
            DataBase.AllPlayerRoles.Clear();
            //今回はC3 I1
            //Sheriff 1
            //です


            //いったんシェリフだけ自動的に一人に割り振ろうと思う
            
        }
        public static void SendRpcSetRole(Roles roles, int[] players)
        {
            //設定作ってないけどここでほんとは分岐
            //ここではmainとして扱う
            var a = players[Random(0, players.Length-1)];
            SetRole(a, (int)roles);
            
        }

        public static void SetRole(int playerId, int roleId)
        {

            //ここにどのroleIdがどのロールに対応するかを判定して

            //var p = PlayerControl.AllPlayerControls.ToArray().First(x => x.PlayerId == playerId).PlayerId;
            if (DataBase.AllPlayerRoles.ContainsKey(playerId))
            {
                var list = DataBase.AllPlayerRoles[playerId];
                list.AddItem(RoleLink.RoleLinks[(Roles)roleId]);
                DataBase.AllPlayerRoles[playerId] = list;
            }
            else
            {

                DataBase.AllPlayerRoles.Add(playerId, [RoleLink.RoleLinks[(Roles)roleId]]);

            }


        }
        public static void SendRpcSetTeam(List<Tuple<Teams,int>> teams)
        {
            List<Teams> k = new(PlayerControl.AllPlayerControls.Count);
            teams.ForEach(t => {
                if (t.Item1 != Teams.Crewmate)
                {
                    if (k.Count <= 0) return;//これ設定で変えたいよね
                    int r = Random(0, k.Count - 1);
                    SetTeam(r, (int)t.Item1);
                    k.RemoveAt(r);
                }
                
                });


        }
        public static void SetTeam(int playerId, int teamId)
        {
            //ここにどのroleIdがどのロールに対応するかを判定して

            //var p = PlayerControl.AllPlayerControls.ToArray().First(x => x.PlayerId == playerId).PlayerId;
            if (DataBase.AllPlayerRoles.ContainsKey(playerId))
            {
                var list = DataBase.AllPlayerRoles[playerId];
                list.AddItem(RoleLink.RoleLinks[(Roles)teamId]);
                DataBase.AllPlayerRoles[playerId] = list;
            }
            else
            {

                DataBase.AllPlayerRoles.Add(playerId, [RoleLink.RoleLinks[(Roles)teamId]]);

            }
        }
    }

}

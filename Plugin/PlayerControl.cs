using AmongUs.GameOptions;
using HarmonyLib;
using System;
using System.Linq;

namespace TheSpaceRoles
{

    [HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
    public static class GameManegerStart
    {
        public static void Prefix(GameManager __instance)
        {
            if (AmongUsClient.Instance.AmHost) PlayerControl.AllPlayerControls.ToArray().Do(x => x.RpcSetRole(RoleTypes.Crewmate));

            foreach ((int i, RoleMaster[] rolemaster) in DataBase.AllPlayerRoles)
            {

                var t = DataBase.AllPlayerControls().First(x => x.PlayerId == i).cosmetics.nameText.text;
                Array.Sort(rolemaster);

                DataBase.AllPlayerControls().First(x => x.PlayerId == i).cosmetics.nameText.text = t + $"\n <size=80%>{string.Join("×", rolemaster.Select(x => x.ColoredRoleName()))}";
            }
            PlayerControl p = PlayerControl.LocalPlayer.GetComponent<PlayerControl>();
            p.RpcSetRole(RoleTypes.Impostor);

            PlayerControl.AllPlayerControls.Add(p);

        }
    }
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public static class PlayerControlUpdatePatch
    {
        public static void Prefix(PlayerControl __instance)
        {

        }
    }
}

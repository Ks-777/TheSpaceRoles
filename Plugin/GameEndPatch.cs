using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSpaceRoles.Plugin
{
    [HarmonyPatch(typeof(GameManager),nameof(GameManager.EndGame))]
    public static class GameEnd
    {
        public static void Prefix()
        {
            DataBase.buttons.Clear();
            HudManagerGame.
            IsGameStarting = false;
        }
    }
}

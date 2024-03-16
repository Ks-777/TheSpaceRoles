using Hazel;
using System.Linq;
using TheSpaceRoles.Plugin.Roles;
using UnityEngine;
using static TheSpaceRoles.Helper;

namespace TheSpaceRoles
{
    public class Impostor : CustomRole
    {
        public Impostor()
        {

            teamsSupported = [Teams.Impostor];
            Role = Roles.Impostor;
            Color = Palette.ImpostorRed;
            HasKillButton = true;
        }
    }
}

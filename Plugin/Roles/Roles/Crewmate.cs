using Hazel;
using System.Linq;
using TheSpaceRoles.Plugin.Roles;
using UnityEngine;
using static TheSpaceRoles.Helper;

namespace TheSpaceRoles
{
    public class Crewmate : RoleMaster
    {
        public Crewmate()
        {
            teamsSupported = [Teams.Crewmate];
            Role = Roles.Crewmate;
            Color = Palette.CrewmateBlue;
        }
    }
}

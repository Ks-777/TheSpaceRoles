using static TheSpaceRoles.Helper;

namespace TheSpaceRoles
{
    public class Sheriff : RoleMaster
    {

        public Sheriff()
        {
            teamsSupported = [Teams.Crewmate];
            Role = Roles.Sheriff;
            Color = ColorFromColorcode("#ffd700");
            HasKillButton = true;
        }
        public override void Start()
        {

        }
    }
}

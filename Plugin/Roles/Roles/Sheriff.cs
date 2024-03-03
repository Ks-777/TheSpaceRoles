using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TheSpaceRoles.Helper;

namespace TheSpaceRoles
{
    public class Sheriff : RoleMaster
    {
        
        public Sheriff() 
        {
            teamsSupported = [Teams.Crewmate];
            Color = ColorFromColorcode("#ffd700");
            HasKillButton = true;
        }
        protected override void Start()
        {
            
        }
    }
}

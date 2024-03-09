using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSpaceRoles
{
    public static class RpcMurderPlayer
    {
        public static void Murder(int id1,int id2,DeathReason reason)
        {


            Helper.GetPlayerControlFromId(id1).MurderPlayer(Helper.GetPlayerControlFromId(id2), MurderResultFlags.Succeeded);
        }
        
    }
}

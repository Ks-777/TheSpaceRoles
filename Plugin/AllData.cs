using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Color = UnityEngine.Color;

namespace TheSpaceRoles
{
    public static class Data_change
    {
        public static Dictionary<int,RoleInfo[]> AllData = new();
    }
    public static class RoleData
    {
        /*all すべてのteam

        c crew
        i impostor
        m madmate
        j jackal
        je jester
        ar arsonist
        vu vulture

        */
        public enum Roles : int {
            //normal
            None = 0,
            Crewmate,
            Impostor,
            Jackal,
            Jester,
            Arsonist,
            Vulture,
            lawyer,//弁護士
            Prosecutor,//検察官?
            Pursuer,//追跡者?
            Thief,//泥棒?

            //all or other
            Mayor,//all
            Engineer,//c
            Sheriff,//c 
            Deputy,//c,m?
            Lighter,//all
            Detective,//all
            TimeMaster,//all, iとかいらねえだろ
            Medic,//all,cだけだろこれぇ 
            Swapper,//all
            Seer,//all
            Hacker,//all
            Tracker,//all
            Snitch,
            Spy,
            Portalmaker,
            Securityguard,
            Guesser,
            Medium,
            Trapper,

        }
        public enum Teams : int
        {
            Crewmate = 0,
            Madmate,
            Impostor,
            Jackal,
            //Oppotunist,
            Jester,
            Arsonist,
            Vulture,
            lawyer,//弁護士
            Prosecutor,//検察官?
            Pursuer,//追跡者?
            Thief//泥棒?
        }

        public static Dictionary<Roles, Color> roleColor = new()
        {   {Roles.Crewmate,Palette.CrewmateBlue },
            {Roles.Impostor,Palette.ImpostorRed },
            
        };
    }

    public enum Rpcs : int
    {
        SetRole = 80,
        ChangeRole,
        GameEnd,
        SendSetting,
        UseAbility,
    }
}

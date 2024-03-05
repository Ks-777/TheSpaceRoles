namespace TheSpaceRoles
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
    public enum Roles : int
    {
        //normal(これが当てはまることはおそらくないかと
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

        //all or other(ここから　
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
        Snitch,//c,m,? 仕様変更入るけど
        Spy,//c,j?
        Portalmaker,//c ?
        Securityguard,//c
        Guesser,//all
        Medium,//c
        Trapper,//all

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

}

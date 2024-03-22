using System.Collections.Generic;
using static TheSpaceRoles.CustomOption;


namespace TheSpaceRoles
{
    public static class CustomOptionsHolder
    {
        public static void CreateCustomOptions()
        {


            Create("use_admin", true);
            Create("use_recodes_admin", true, "use_admin", OnOff);
            List<string> o = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                o.Add(i.ToString());
            }
            Create("use", [..o], "0");

            Create("user", [.. o], "0");
        }
    }
}

using LibCpp2IL.Elf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheSpaceRoles
{
    public static class Helper{
        public static UnityEngine.Color ColorFromColorcode (string colorcode)
        {

            if (ColorUtility.TryParseHtmlString(colorcode, out Color color))
            {
                return color;
            }
            else
            {
                return Color.magenta;

            }
        }
        public static UnityEngine.Color ColorFromColorcode(int colorcode)
        {
            ColorUtility.TryParseHtmlString("#"+colorcode.ToString(), out Color color);
            return color;
        }
        public static int Random(int a ,int b)
        {

            System.Random r = new System.Random();
            return r.Next(a, b+1);
        }
    }
}

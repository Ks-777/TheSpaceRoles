using UnityEngine;

namespace TheSpaceRoles
{
    public static class Helper
    {
        public static UnityEngine.Color ColorFromColorcode(string colorcode)
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
            ColorUtility.TryParseHtmlString("#" + colorcode.ToString(), out Color color);
            return color;
        }
        public static string ColoredText(Color color,string text) 
        {
            return "<color=#" + ColorUtility.ToHtmlStringRGB(color) +">"+text;
        }

        public static int Random(int a, int b)
        {

            System.Random r = new System.Random();
            return r.Next(a, b + 1);
        }
    }
}

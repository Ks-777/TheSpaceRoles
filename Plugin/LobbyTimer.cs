using HarmonyLib;
using InnerNet;
using TMPro;
using UnityEngine;

namespace TheSpaceRoles;

public class LobbyTimer
{
	[HarmonyPatch(typeof(GameStartManager), "Start")]
	public class GameStartManagerStartPatch
	{
		public static void Postfix(GameStartManager __instance)
		{
			timer = 600f;
			update = GameData.Instance.PlayerCount != __instance.LastPlayerCount;
			tmpro = __instance.PlayerCounter;

        }
	}

	[HarmonyPatch(typeof(GameStartManager), "Update")]
	public static class GameStartManagerUpdatePatch
	{
		public static void Prefix(GameStartManager __instance)
		{
            __instance.PlayerCounter.alignment = TextAlignmentOptions.Left;

            AmongUsClient instance = AmongUsClient.Instance;
			
			update = GameData.Instance.PlayerCount != __instance.LastPlayerCount;
			
		}

		public static void Postfix(GameStartManager __instance)
		{
            if (update)
			{
				playercounter = __instance.PlayerCounter.text;
			}
            __instance.PlayerCounter.autoSizeTextContainer = true;

            timer = Mathf.Max(0f, timer -= Time.deltaTime);
			AmongUsClient instance = AmongUsClient.Instance;
			if (instance == null || instance.NetworkMode != 0)
			{
				if (AmongUsClient.Instance.AmHost)
				{

                    if (/*‚ ‚Æ‚Å‚±‚Ì&&‘O‚Ü‚Åíœ*/TSR.LobbyTimer.Value)
                    {
                        string text = $" ({(int)(timer / 60f):00}:{(int)(timer % 60f):00})";
						if(timer > 60f)
						{ 
                        __instance.PlayerCounter.text =  playercounter +"<color=#00FF00>"+ text;

						}
						else
						{
							__instance.PlayerCounter.text = playercounter + "<color=#FF0000>" + text;
						}

						
                        __instance.PlayerCounter.autoSizeTextContainer = true;
						__instance.PlayerCounter.enableWordWrapping = false; 
					}
					else
					{

                        __instance.PlayerCounter =  tmpro;
						__instance.PlayerCounter.text =   playercounter;
                    }
                }



			}
		}
	}
	public static TextMeshPro tmpro;

    public static float timer = 600f;

	public static string playercounter = "";

	public static bool update = false;

}

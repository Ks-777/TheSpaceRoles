﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace TheSpaceRoles.Plugin.Roles
{
    public static class PlayerControlButtonControls
    {
        /// <summary>
        /// ターゲットに対しての距離を測って一番近いplayerのidを出力
        /// </summary>
        /// <param name="targetdistance">ターゲットの許容距離</param>
        /// <param name="notIncludeTeamIds">含まないチーム</param>
        /// <param name="notIncludeIds">含まないPlayerID</param>
        /// <param name="target">誰基準か</param>
        /// <param name="canBeTargetInVentPlayer">ベント内のプレイヤーを含むか</param>
        /// <returns>一番近いplayerのid  もし-1が帰ってきたらターゲットいないです</returns>
        public static int SetTarget( float  targetdistance,Color color,Teams[] notIncludeTeamIds = null,byte[] notIncludeIds = null,int target = -1,bool canBeTargetInVentPlayer = false)
        {
            DataBase.AllPlayerControls().Do(x => x.cosmetics.currentBodySprite.BodySprite.material.SetFloat("_Outline", 0f));
            DataBase.AllPlayerControls().Do(x => x.cosmetics.currentBodySprite.BodySprite.material.SetColor("_OutlineColor", color));
            int id = -1;
            float distance = 100000000;
            if(target == -1)
            {
                target = PlayerControl.LocalPlayer.PlayerId;
            }
            foreach (var x in PlayerControl.AllPlayerControls)
            {
                if (notIncludeTeamIds != null && notIncludeIds.Length > 0)
                {
                    if (notIncludeIds.Contains(x.PlayerId))
                    {
                        continue;
                    }
                }
                if (notIncludeTeamIds != null && notIncludeTeamIds.Length > 0)
                {
                    if (notIncludeTeamIds.Contains(DataBase.AllPlayerTeams[x.PlayerId]))
                    {
                        continue;
                    }

                }
                if (x.inVent)
                {
                    if (!canBeTargetInVentPlayer) { continue; }
                }
                if (x.Data.IsDead)
                {
                    continue;
                }
                if (target == x.PlayerId) continue;
                PlayerControl p =Helper.GetPlayerControlFromId(target);
                Vector2 vec = p.transform.position;
                Vector2 vec2 = x.transform.position;
                vec -= vec2;
                float dis = vec.magnitude;

                if (distance > dis) 
                {
                    id = x.PlayerId;
                    distance = dis;
                }
            }
            if(targetdistance >= distance) 
            {
                Helper.GetPlayerControlFromId(id).cosmetics.currentBodySprite.BodySprite.material.SetFloat("_Outline", 1f);
                return id;

                
            }
            return -1;



        }
    }

}
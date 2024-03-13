﻿using AmongUs.GameOptions;
using HarmonyLib;
using Il2CppSystem.Data;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TheSpaceRoles
{

    [HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
    public static class GameManegerStart
    {
        public static void Prefix(GameManager __instance)
        {
            if (AmongUsClient.Instance.AmHost) DataBase.AllPlayerControls().Do(x => x.RpcSetRole(RoleTypes.Crewmate));

            foreach ((int i, RoleMaster[] rolemaster) in DataBase.AllPlayerRoles)
            {
                //var t = DataBase.AllPlayerControls().First(x => x.PlayerId == i).cosmetics.nameText.text;
                Array.Sort(rolemaster);

                if (i == PlayerControl.LocalPlayer.PlayerId)
                {

                    //DataBase.AllPlayerControls().First(x => x.PlayerId == i).cosmetics.nameText.text = t + $"\n <size=80%>{string.Join("×", rolemaster.Select(x => x.ColoredRoleName()))}";
                    var d = DataBase.AllPlayerControls().First(x => x.PlayerId == i).cosmetics.nameText;
                    GameObject gameObject = new GameObject("roletext");
                    TextMeshPro RoleText = gameObject.AddComponent<TextMeshPro>();
                    RoleText.transform.parent = d.transform.parent;
                    RoleText.transform.localPosition = new Vector3(d.transform.localPosition.x, d.transform.localPosition.y-0.25f, d.transform.localPosition.z);
                    RoleText.alignment = TextAlignmentOptions.Center;
                    RoleText.tag=d.tag;
                    RoleText.fontSize = d.fontSize;
                    RoleText.sortingOrder = d.sortingOrder;
                    RoleText.sortingLayerID = d.sortingLayerID;
                    RoleText.text = $"<size=85%>{string.Join("</color>×", rolemaster.Select(x => x.ColoredRoleName())/*+"</color>"*/)}";
                    RoleText.transform.localScale = Vector3.one;
                    RoleText.m_sharedMaterial = d.m_sharedMaterial;
                    RoleText.fontStyle = d.fontStyle;
                }
            }

        }
    }
}

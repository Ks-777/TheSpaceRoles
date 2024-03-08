using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheSpaceRoles
{

    public class CustomButton
    {
        public static Vector2 SelectButtonPos(int c) => c switch
        {
            0 => new Vector2(0, 0),
            1 => new Vector2(-1, 0),
            2 => new Vector2(-2, 0),
            3 => new Vector2(0, 1),
            4 => new Vector2(-1, 1),
            5 => new Vector2(-2, 1),
            6 => new Vector2(-8, 0),
            _ => new Vector2(-3, 3),
        };
        public static List<CustomButton> buttons = new();


        public ActionButton actionButton;
        public HudManager hudManager;
        public float maxTimer;
        public float Timer;
        public Vector2 position;


        public bool hasEffect;
        public bool canEffectCancel;
        public float maxEffectTimer;
        public float effectTimer;
        public Func<int, bool> canUse;
        public string buttonText = "";


        public Sprite sprite;
        public KeyCode keyCode;
        public Action OnClick;
        public Action OnMeetingEnds;
        public Action OnEffectEnds;
        public CustomButton(
            HudManager hudManager,
            Vector2 pos,
            KeyCode keycode,
            float maxTimer,
            Func<int, bool> canUse,
            Sprite sprite,
            Action Onclick,
            Action OnMeetingEnds,
            string buttonText,
            bool HasEffect,
            bool canEffectCancel = false,
            float EffectDuration = 0,
            Action OnEffectEnds = null
            )
        {
            this.hudManager = hudManager;
            this.keyCode = keycode;
            this.OnClick = Onclick;
            this.OnMeetingEnds = OnMeetingEnds;
            this.canUse = canUse;
            this.sprite = sprite;
            this.position = pos;
            this.buttonText = buttonText;
            this.hasEffect = HasEffect;
            this.maxTimer = maxTimer;
            Timer = 16.2f;
            if (hasEffect) this.canEffectCancel = canEffectCancel;
            if (hasEffect) this.maxEffectTimer = EffectDuration;
            if (hasEffect) this.effectTimer = EffectDuration;
            actionButton = UnityEngine.Object.Instantiate(hudManager.KillButton, hudManager.KillButton.transform.parent);
            actionButton.buttonLabelText.text = buttonText;
            actionButton.graphic.sprite = sprite;
            actionButton.transform.position.Set(position.x, position.y, -9);
            actionButton.cooldownTimerText.text = ((int)Timer).ToString();

            buttons.Add(this);
            Logger.Info(buttonText + " button Is created");
        }
    }
}

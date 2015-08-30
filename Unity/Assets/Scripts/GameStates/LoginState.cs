﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.GameStates
{
    public class LoginState : App.GameState
    {
        public override void OnEnter()
        {
            base.OnEnter();
            UI.UIControllor.Singleton.HideAllUI();
            App.GameAppliaction.Singleton.JoinCastle();
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override void OnTick()
        {
            base.OnTick();
        }
    }
}

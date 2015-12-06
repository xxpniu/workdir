﻿using Assets.Scripts.Combat.Simulate;
using ExcelConfig;
using Proto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Combat.Battle.States
{
    public class BattleState :GState
    {
        public BattleState(int battleGroupID, IBattleRender render, EndBattleCallBack callBack = null)
        {
            Render = render;
            CallBack = callBack;
            var perception = new BattlePerception(this);
            this.Perception = perception;
            var battle = new Elements.BattleEl(new Controllors.BattleControllor(this.Perception), battleGroupID);
            AddElement(battle);
            GroupConfig = ExcelConfig.ExcelToJSONConfigManager.Current.GetConfigByID<ExcelConfig.BattleGroupConfig>(battleGroupID);
            Render.SetPerception(Perception as BattlePerception);
        }

        public BattleGroupConfig GroupConfig { private set; get; }

        public IBattleRender Render { private set; get; }

        public EndBattleCallBack CallBack;

        public override void OnEnter()
        {
          
        }
        public override void OnExit()
        {
            //throw new NotImplementedException();
        }

        public void End(BattleResult result)
        {
            this.Enable = false;
            if (CallBack == null) return;
            CallBack(result);
        }

        

    }

    public class BattleResult
    {
       public ArmyCamp Winner{set;get;}
    }

    public delegate void EndBattleCallBack(BattleResult result);
}

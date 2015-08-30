﻿using Assets.Scripts.Combat.Simulate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Combat.Battle.States
{
    public class BattleState :GState
    {
        public BattleState(int battleGroupID)
        {
            var perception = new BattlePerception(this);
            this.Perception = perception;

            var battle = new Elements.BattleEl(new Controllors.BattleControllor(this.Perception),battleGroupID);
            var controllor = new Controllors.ArmyControllor(this.Perception);
            //var playerArmy = new Elements.BattleArmy(controllor, player);
            //var monsterArmy = new Elements.BattleArmy(controllor, monster);

            //AddElement(playerArmy);
            //AddElement(monsterArmy);

            //var battle = new Elements.Battle(new Controllors.BattleControllor(Perception));
            AddElement(battle);
        }
        public override void OnEnter()
        {
            //throw new NotImplementedException();
        }
        public override void OnExit()
        {
            //throw new NotImplementedException();
        }
    }
}

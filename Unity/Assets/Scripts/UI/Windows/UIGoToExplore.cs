using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Tools;

namespace Assets.Scripts.UI.Windows
{
    partial class UIGoToExplore
    {

        public override void InitModel()
        {
            base.InitModel();
            bt_close.OnMouseClick((s, e) => {
                HideWindow();
            });

            bt_heroSet.OnMouseClick((s, e) => {
                var ui = UIManager.Singleton.CreateOrGetWindow<UIBattleHero>();
                ui.ShowWindow();
            });

            this.bt_armySet.OnMouseClick((s, e) => {
                UIExploreResources.Show();
            });
            //Write Code here

            bt_go.OnMouseClick((s, e) => {
                App.GameAppliaction.Singleton.GoToExplore(1);
            });
        }
        public override void OnShow()
        {
            base.OnShow();
            
        }
        public override void OnHide()
        {
            base.OnHide();
        }
    }
}
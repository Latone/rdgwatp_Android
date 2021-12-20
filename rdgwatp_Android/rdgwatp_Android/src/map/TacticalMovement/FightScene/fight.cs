using ConsoleApp1.src.creatures.builders;
using ConsoleApp1.src.items;
using ConsoleApp1.src.map.TacticalMovement.GraphCollection.FightGraph;
using ConsoleApp1.src.map.TacticalMovement.LocalVars;
using ConsoleApp1.src.map.TacticalMovement.MinPath;
using ConsoleApp1.src.map.TacticalMovement.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using rdgwatp_Android.src.map.Log;

namespace ConsoleApp1.src.map.TacticalMovement.FightScene
{
    //Сам бой
    public static class fight
    {
        public static AutoResetEvent waitHandler = new AutoResetEvent(true);
        public static CreatureBuilder cb;
        public static CreatureBuilder cbP;
        public static void stepFight(string Act)
        {
            
            switch (Act)
            {
                case "STATS":
                    //CreatureStats.ShowStats(cb, cbP);
                    //ShowScreen();
                    break;

                //Атака
                case "ATTACK_MOVE":
                    
                    CreatureFight.RaiseYerHand(ref cb, ref cbP);
                    break;

                //Просмотр инветаря
                case "INVENTORY_USE":

                    CreatureFight.EnemyRaisedABiggerOne(ref cb, ref cbP);
                    break;

                //Special Secret Joestar technique
                case "RUN_AWAY":
                    cb.setDisableTime(3);
                    cbP.setIsFighting(false);
                    cbP.setZeroTriggerFighting(false);
                    waitHandler.Set();
                    return;
            }
            //Переброс предметов из карманов противника в грязные карманцы хобиттса
            if (cb.getHP() < 1 && cb.getIsDead() == false)
            {
                cb.setIsDead(true);
                cbP.setIsFighting(false);
                cbP.setZeroTriggerFighting(false);
                
                foreach (ItemType it in cb.getInventory())
                {
                    cbP.getInventory().Add(it);
                    Logger.AddMessage(("{0} added to your inventory", it.name).ToString());
                }
                Logger.AddMessage(("{0} been killed", cb.getType()).ToString());
                //cb = null;
                cbP.setIsFighting(false);
                cbP.setZeroTriggerFighting(false);
                waitHandler.Set();
                return;
            }
        }
        public static void Start(CreatureBuilder cbL, ref CreatureBuilder cbPL)
        {
            waitHandler.WaitOne();
            cb = cbL;
            cbP = cbPL;
            //cbP.setIsFighting(true);
        }

    }
}

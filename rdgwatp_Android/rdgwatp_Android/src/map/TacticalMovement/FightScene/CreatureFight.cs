using ConsoleApp1.src.creatures.builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using rdgwatp_Android.src.map.Log;
using ConsoleApp1.src.map.TacticalMovement.LocalVars;
using rdgwatp_Android.src.map.Score;

namespace ConsoleApp1.src.map.TacticalMovement.FightScene
{
    class CreatureFight
    {
        private static readonly object syncObj = new object();
        //RaiseYerHand - бьёт игрок
        public static void RaiseYerHand(ref CreatureBuilder cb, ref CreatureBuilder  cbP)
        {
            Logger.AddMessage("Your turn");
            short sumDMGP = cbP.getDMG(); 
            Random rand = new Random();

            //Шанс на крит игрока, шанс на Evade/Block противника
            if (rand.Next(0, 100) <= cbP.getCh_Crit_Attack())
            {
                sumDMGP *= (short)(1 + cbP.getPower_Crit_Attack() / 100);
                Logger.AddMessage("IT'S CRITICAL!~");
            }
            if (rand.Next(0, 100) <= cb.getCh_Evade() ||
                rand.Next(0, 100) <= cb.getCh_Block())
            {
                sumDMGP = 0;
                Logger.AddMessage(String.Format("{0} blocked/evaded your attack", cb.getType()).ToString());
            }

            //Поглощение урона бронёй
            short reminder = sumDMGP;
            sumDMGP = (short)(sumDMGP - cb.getARMOR());
            cb.setARMOR((short)(cb.getARMOR() - reminder));

            if (sumDMGP < 1)
                sumDMGP = 0;
            if (cb.getARMOR() < 1)
            {
                cb.setARMOR(0);
                Logger.AddMessage("The enemy's armor been shattered");
            }

            cb.setHP((short)(cb.getHP() - sumDMGP));
            cbP.setDMG(cbP.getDefaultDMG());
            if(sumDMGP!=0)
                Logger.AddMessage(String.Format("You dealt {0} dmg", sumDMGP).ToString());
            //Переписывание шкалы здоровья в момент срабатывания (плохая идея, т.к. там ещё и броня)
            //Console.SetCursorPosition(3, 0);
            //for (int i = 0; i < cb.getHP().ToString().Length; i++)
            //  Console.Write(" ");
            //Console.SetCursorPosition(3 + 2 - cb.getHP().ToString().Length, 0);
            // Console.Write(cb.getHP());

            //Фрейм врага на поглощение удара (Анимация)
            //Console.SetCursorPosition(0, 1);

            string temp = cb.getEmotions(1,true)[1];
            
            
            Thread.Sleep(500);

            //Если враг откинулся
            //Ток 1 тред может здесь быть - lock
            lock (syncObj)
            {
                if (cb.getHP() < 1)
                {
                    foreach (var thread in EnemyMoves.threads)
                    {
                        if (thread.ManagedThreadId == Thread.CurrentThread.ManagedThreadId)
                            continue;
                        thread.Abort();
                    }
                    Scorer.addToScore(250);
                    return;
                }
            }
            //Очередь врага
            EnemyRaisedABiggerOne(ref cb, ref cbP);
        }
        public static void EnemyRaisedABiggerOne(ref CreatureBuilder cb, ref CreatureBuilder cbP)
        {
            Logger.AddMessage("Enemy's turn");
            //Фрейм врага на базовое состояние (Анимация)
            //Console.SetCursorPosition(0, 1);
            string temp = cb.getEmotions(0,true)[0];
            Thread.Sleep(400);

            //Фрейм врага на подготовку удара (Анимация)
            //Console.SetCursorPosition(0, 1);
            temp = cb.getEmotions(2,true)[2];
            Thread.Sleep(500);

            //Шанс на крит врага, шанс на Evade/Block игрока
            short sumDMGE = cb.getDMG();
            Random rand = new Random();
            if (rand.Next(0, 100) <= cb.getCh_Crit_Attack())
            {
                sumDMGE *= (short)(1 + cb.getPower_Crit_Attack() / 100);
                Logger.AddMessage("IT'S CRITICAL!~");
            }
            if (rand.Next(0, 100) <= cbP.getCh_Evade() ||
                rand.Next(0, 100) <= cbP.getCh_Block())
            {
                sumDMGE = 0;
                Logger.AddMessage("You evaded/blocked an attack!");
            }

            //Поглощение урона бронёй
            short reminder = sumDMGE;
            sumDMGE = (short)(sumDMGE - cb.getARMOR());
            cbP.setARMOR((short)(cbP.getARMOR() - reminder));

            if (sumDMGE < 1)
                sumDMGE = 0;
            if (cbP.getARMOR() < 1)
            {
                cbP.setARMOR(0);
                Logger.AddMessage("Your armor depleed");
            }

            cbP.setHP((short)(cbP.getHP() - sumDMGE));
            if(sumDMGE!=0)
                Logger.AddMessage(String.Format("{0} dealt {1} dmg",cb.getType(), sumDMGE).ToString());
            Thread.Sleep(500);
            //Console.SetCursorPosition(3, 16);
            //Переписывание шкалы здоровья в момент срабатывания (плохая идея, т.к. там ещё и броня)
            //for (int i = 0; i <= cbP.getHP().ToString().Length; i++)
            //    Console.Write(" ");
            // Console.SetCursorPosition(3 + 3 - cbP.getHP().ToString().Length, 8);
            //Console.Write(cbP.getHP());
            //Фрейм врага на нанесение удара (Анимация)
            //Console.SetCursorPosition(0, 1);
            temp = cb.getEmotions(3,true)[3];
            Thread.Sleep(1100);
            temp = cb.getEmotions(0, true)[0];
            Thread.Sleep(400);
            temp = cb.getEmotions(0, true)[0];
        }
    }
}

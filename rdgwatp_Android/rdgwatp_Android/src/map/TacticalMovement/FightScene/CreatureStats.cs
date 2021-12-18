using ConsoleApp1.src.creatures.builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.src.map.TacticalMovement.FightScene
{
    //Вывод статов врага и игрока в бою
    class CreatureStats
    {
        public static void ShowStats(CreatureBuilder cb, CreatureBuilder cbP)
        {
            Console.Clear();
            Console.WriteLine("Enemy STATS:              Player STATS:\n"+
                              "HP: " + cb.getHP()+"                    "+"HP: "+cbP.getHP()+"\n"+
                              "Armor: " + cb.getARMOR() + "                  " + "Armor: " + cbP.getARMOR() + "\n" +
                              "Attack: " +cb.getDMG() + "                 " + "Attack:" + cbP.getDMG() + "\n" +
                              "Block %: " + cb.getCh_Block() + "                " + "Block %: " + cbP.getCh_Block() + "\n" +
                              "Evade %: " + cb.getCh_Evade() + "                " + "Evade %: " + cbP.getCh_Evade() + "\n" +
                              "Critical attack ⚠: " + cb.getPower_Crit_Attack() + "    " + "Critical attack ⚠: " + cbP.getPower_Crit_Attack() + "\n" +
                              "Critical attack %: " + cb.getCh_Crit_Attack() + "      " + "Critical attack %: " + cbP.getCh_Crit_Attack() + "\n");
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            do {
            }  while (keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.Enter);
        }
    }
}

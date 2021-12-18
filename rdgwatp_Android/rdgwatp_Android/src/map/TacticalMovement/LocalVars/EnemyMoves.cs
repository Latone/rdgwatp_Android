using ConsoleApp1.src.creatures.builders;
using ConsoleApp1.src.map.TacticalMovement.MinPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using ConsoleApp1.src.map.TacticalMovement.FightScene;
using System.Threading;

namespace ConsoleApp1.src.map.TacticalMovement.LocalVars
{
    public static class EnemyMoves
    {
        //Move - рандомное хождение по карте
        public static void Move(CreatureBuilder cb, CreatureBuilder cbP,short PlayerXcoor, short PlayerYcoor)
        {
            //DebuffTime - Пока только для существа Ghost (только он может вешать дебафф)
            //Далее идёт время (в шагах) на снятие
            //Чем больше врагов - тем быстрее спадёт
            if (cbP.getDebuffTime() > 0)
                cbP.setDebuffTime(cbP.getDebuffTime() - 1);
            else if (cbP.getDebuffTime() == 0)
                cbP.NullDebuffStats();
            //DisableTime - заморозка существ 
            if (cb.getDisableTime() > 0)
            {
                cb.setDisableTime((byte)(cb.getDisableTime() - 1));
                return;
            }
            //Далее идёт проверка - видит ли враг игрока, если true, Запускается RunningThreat (ПРОВЕРКА ДО ШАГА)
            //То есть начинается погоня существа за игроком
            if (((Math.Abs(PlayerYcoor - cb.getY()) <= cb.getNoticeRange()) &&
                (Math.Abs(PlayerXcoor - cb.getX()) <= cb.getNoticeRange()) )&& cb.getNoticeRange()!=0)
            {
                cb.setChase(true);
                RunningThreat(cb,cbP, PlayerXcoor, PlayerYcoor);
                return;
            }
            cb.RememberLastCoordinates();
            //Выбор рандомного направления хода:
            Random rnd = new Random();
            //PercentageOfTimeThinks - сделано для того, чтобы игрок смог убежать от врага, 
            //иначе бы существо вело себя как хвостик
            byte Number = (byte)rnd.Next(1, cb.getPercentageOfTimeThinks()); 
            switch (Number)
            {
                case 1://UP
                    if (cb.getY() - 1 >= 0 &&  VisualCharacters.FloorPattern.Contains(CreatedMap.testMap[cb.getX(), cb.getY() - 1]))
                        cb.setY((byte)(cb.getY() - 1));
                    break;
                case 2://DOWN
                    if (cb.getY() + 1 < CreatedMap.Mheight && VisualCharacters.FloorPattern.Contains(CreatedMap.testMap[cb.getX(), cb.getY() + 1]))
                        cb.setY((byte)(cb.getY() + 1));
                    break;
                case 3://LEFT
                    if (cb.getX() - 1 >= 0 && VisualCharacters.FloorPattern.Contains(CreatedMap.testMap[cb.getX() - 1, cb.getY()]))
                        cb.setX((byte)(cb.getX() - 1));
                    break;
                case 4://RIGHT
                    if (cb.getX() + 1 < CreatedMap.Mwidth && VisualCharacters.FloorPattern.Contains(CreatedMap.testMap[cb.getX() + 1, cb.getY()]))
                        cb.setX((byte)(cb.getX() + 1));
                    break;
                default://DO_NOTHING
                    break;
            }
            //Ещё одна проверка на досягаемость поля зрения -> true - Run RunningThreat method (ПРОВЕРКА ПОСЛЕ ШАГА)
            //То есть начинается погоня существа за игроком
            if (((Math.Abs(PlayerXcoor - cb.getY()) <= cb.getNoticeRange()) &&
                (Math.Abs(PlayerYcoor - cb.getX()) <= cb.getNoticeRange()))&& cb.getNoticeRange() != 0) 
            {
                cb.setChase(true);
            }
        }
        //В RunningThreat - идёт погоня (существо двигается к игроку по кратчайшему пути в графе)
        public static void RunningThreat(CreatureBuilder cb,CreatureBuilder cbP, short PlayerXcoor, short PlayerYcoor)
        {
            //DebuffTime - Пока только для существа Ghost (только он может вешать дебафф)
            //Далее идёт время (в шагах) на снятие
            //Чем больше врагов - тем быстрее спадёт
            if (cbP.getDebuffTime() > 0)
                cbP.setDebuffTime(cbP.getDebuffTime() - 1);
            else if (cbP.getDebuffTime() == 0)
                cbP.NullDebuffStats();
            //DisableTime - заморозка существ 
            if (cb.getDisableTime() > 0)
            {
                cb.setDisableTime((byte)(cb.getDisableTime() - 1));
                return;
            }
            //Проверка на то, что игрок в поле НЕдосягаемости, если true - состояние врага меняется на обычное хождение Move
            //Проверка ДО ШАГА
            if ((Math.Abs(PlayerYcoor - cb.getY()) >= cb.getVanishRange()) ||
                (Math.Abs(PlayerXcoor - cb.getX()) >= cb.getVanishRange())) 
            {
                cb.setChase(false);
                Move(cb,cbP, PlayerXcoor, PlayerYcoor);
                return;
            }
            //Проверка на Ghost - по идее он просто бродит по карте, не гонится за игроком, вешает дебаффы
            //Далее идёт проверка на то, что игрок коснулся призрака
            if (PlayerYcoor - cb.getY() == 0 &&
                PlayerXcoor - cb.getX() == 0 && cb.getType() == creatures.types.Type.ENT_GHOST)
            {
                //Вешаются дебаффы на 20 ходов
                cbP.setDebuffStats(20);
                cbP.setCh_Evade(0);
                cbP.setCh_Block(0);
            }
            //Если игрок наступил на врага (НЕ GHOST)
            else if (PlayerYcoor - cb.getY() == 0 &&
                PlayerXcoor - cb.getX() == 0) 
            {
                //Начинается драка
                Console.WriteLine("Cheeky-Breeky i v damki");

                if (cbP.getZeroTriggerFighting() == false)
                    cbP.setZeroTriggerFighting(true);

                Thread th = new Thread(x => fight.Start(cb, cbP));
                th.Start();

                return;
            }
            //Проверка на Ghost (НЕ МОЖЕТ СЛЕДОВАТЬ ЗА ИГРОКОМ)
            if (cb.getType() == creatures.types.Type.ENT_GHOST)
                return;
            //Из метода Graph.getShortestDistance() возвращается кратчайший путь до игрока на графе
            LinkedList<short> NewEntryPath = Graph.getShortestDistance(cb.getX() * CreatedMap.Mwidth + cb.getY(),
                                          PlayerXcoor * CreatedMap.Mwidth + PlayerYcoor);
            //Помещается путь, по которому шагает враг до игрока
            cb.setChasePath(NewEntryPath);
            // Удаляется последняя, т.к.На последнем месте вершина, на которой распологается сам враг
            cb.getChasePath().RemoveLast();

            //WayToGo (вычисляется следующее направление, сделано для отображения на "карте")
            short NextStep = cb.getChasePath().ElementAt(cb.getChasePath().Count-1);
            short CreaturePosition = (short)(cb.getX() * CreatedMap.Mwidth + cb.getY());
            short diff = (short) (NextStep - CreaturePosition);

            cb.RememberLastCoordinates();
            //Хождение по кратчайшему пути в графе
            Random rnd = new Random();
            //PercentageOfTimeThinks - сделано для того, чтобы игрок смог убежать от врага, 
            //иначе бы существо вело себя как хвостик
            byte Number = (byte)rnd.Next(1, cb.getPercentageOfTimeThinks());
            if (Number > 4)//DO NOTHING
            { }
            else
            {
                if (diff == -1) //UP
                {
                    cb.setY((byte)(cb.getY() - 1));
                }
                else if (diff == 1) //DOWN
                {
                    cb.setY((byte)(cb.getY() + 1));
                }
                else if (diff == -CreatedMap.Mheight) //LEFT
                {
                    cb.setX((byte)(cb.getX() - 1));
                }
                else //RIGHT
                {
                    cb.setX((byte)(cb.getX() + 1));
                }
            }
            //Проверка на то, что игрок вышел из поля зрения (Ещё одна) true -> Запускается Move
            //Проверка ПОСЛЕ ШАГА
            if ((Math.Abs(PlayerXcoor - cb.getY()) >= cb.getVanishRange()) &&
                (Math.Abs(PlayerYcoor - cb.getX()) >= cb.getVanishRange()))
            {
                cb.setChase(false);
            }
            //Если догнал
            if (cb.getChasePath().Count == 1) 
            {
                //Начинается бой
                Console.WriteLine("Cheeky-Breeky i v damki");
                if (cbP.getZeroTriggerFighting() == false)
                    cbP.setZeroTriggerFighting(true);

                Thread th = new Thread(x => fight.Start(cb, cbP));
                th.Start();
            }    
        }
    }
}

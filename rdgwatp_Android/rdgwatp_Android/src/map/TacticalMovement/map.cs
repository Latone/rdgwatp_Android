using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization;
using ConsoleApp1.src.creatures.builders;
using ConsoleApp1.src.creatures.director;
using ConsoleApp1.src.items;
using ConsoleApp1.src.map.TacticalMovement.LocalVars;
using ConsoleApp1.src.map.TacticalMovement.Strategies;
using rdgwatp_Android;
using rdgwatp_Android.src.map.MapGenerator;
using System.Threading.Tasks;

namespace ConsoleApp1.src.map
{

    class map
    {
        private static Context context;
        private static List<CreatureBuilder> Lcb;
        //Обновление фрейма с каждым шагом 
        private static void RefreshFrame(ref List<CreatureBuilder> Lcb)
        {
            foreach (CreatureBuilder cb in Lcb)
            {
                //Обновление местонахождения каждого из существ
                Rendering.WriteAt(CreatedMap.testMap[cb.getLastX(), cb.getLastY()], cb.getLastX(), cb.getLastY());
                Rendering.WriteAt(cb.getIcon(), cb.getX(), cb.getY());
            }
            //Обновление карты на странице MapPage.xaml
            FastForwardChangeXMLObjects.Process("");
        }
        public static void StartMap()
        {
            Drunkard.Initialize(); // Создаём рандомную мапу
            short PcoorX = (short)Drunkard.RstartPointX, //Тестовые координаты для игрока
                 PcoorY = (short)Drunkard.RstartPointY;

            // CreatedMap.fillMap();
            CreatedMap.CreateGraph(); // Создание графа для карты, по нему перемещаются существа
            //Список существ
            Lcb = new List<CreatureBuilder>();
            //Создание отдельной оболочки для игрока
            CreatureBuilder cbP = new CreatureBuilder();
            Director director = new Director();

            //Заполнение оболочки игрока
            director.constructPlayer(cbP, PcoorX, PcoorY);
            Lcb.Add(cbP);
            //Создание/Заполнение оболочек для существ
            while (Drunkard.Enemy_Cords.Count > 0)
            {
                Drunkard.coords en_c = Drunkard.Enemy_Cords.Pop();
                CreatureBuilder cbNEW = new CreatureBuilder();
                switch (Randomizator.GenerateRandomNumber(4))
                {
                    case 0:
                        director.constructSkeleton(cbNEW, en_c.x, en_c.y);
                        break;
                    case 1:
                        director.constructSlime(cbNEW, en_c.x, en_c.y);
                        break;
                    case 2:
                        director.constructDoorKnob(cbNEW, en_c.x, en_c.y);
                        break;
                    case 3:
                        director.constructGhost(cbNEW, en_c.x, en_c.y);
                        break;
                }
                Lcb.Add(cbNEW);
            }
            CreatedMap.ShowMap();
            //Задание кодировки (надо ли сейчас, хз)
            Console.OutputEncoding = Encoding.Unicode;
            RefreshFrame(ref Lcb);

            context = new Context();
           
        }

        //Тут с каждым шагом выполняется та или иная стратегия (они не особо отличаются)
        public static void stepGame(string keyInfo)
        {
            //Удаление всех существ с хп ниже 1
            Lcb.RemoveAll(c => c.getHP() < 1);
            //RefreshFrame(ref Lcb);

            if (context == null)
                return;
            switch (keyInfo)
            {
                case "UP":
                    context.SetStrategy(new StrategyGoUp());//Шаг вверх
                    break;
                case "DOWN":
                    context.SetStrategy(new StrategyGoDown());//Шаг вниз
                    break;
                case "LEFT":
                    context.SetStrategy(new StrategyGoLeft());//Шаг Влево
                    break;
                case "RIGHT":
                    context.SetStrategy(new StrategyGoRight());//Шаг вправо
                    break;
                case "I":
                    context.SetStrategy(new StrategyOpenInventory());//Открыть инвентарь
                    break;
                default:
                    break;
            }
            //Выполнение той или иной стратегии
            context.DoSomeBusinessLogic(ref Lcb);
            //Удаление всех существ с хп ниже 1
            Lcb.RemoveAll(c => c.getHP() < 1);
            //Обновление фрейма
            RefreshFrame(ref Lcb);

            if (Lcb[0].getZeroTriggerFighting())
            {
                Lcb[0].setIsFighting(true);
            }
        }
        public static CreatureBuilder getPlayer()
        {
            return Lcb[0];
        }
        public static void Clear() //Очистка при выходе из окна
        {
            context = null;
            Lcb = null;
            VisualCharacters.mapview = new List<string> { };
        }
    }
}

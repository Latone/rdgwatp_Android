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
            const byte PcoorX = 1, //Тестовые координаты для игрока
                       PcoorY = 1;

            // CreatedMap.fillMap();
            CreatedMap.CreateGraph(); // Создание графа для карты, по нему перемещаются существа
            //Создание оболочек для существ
            CreatureBuilder cbP = new CreatureBuilder();
            CreatureBuilder cbS = new CreatureBuilder();
            CreatureBuilder cbL = new CreatureBuilder();
            CreatureBuilder cbK = new CreatureBuilder();
            CreatureBuilder cbG = new CreatureBuilder();
            Director director = new Director();

            //Заполнение оболочек для существ
            director.constructPlayer(cbP, PcoorX, PcoorY);
            director.constructSkeleton(cbS, PcoorX + 5, PcoorY + 5);
            director.constructSlime(cbL, PcoorX + 5, PcoorY + 5);
            //director.constructDoorKnob(cbK, PcoorX+5, PcoorY+5);
            director.constructGhost(cbG, PcoorX + 5, PcoorY + 4);

            //Список существ
            Lcb = new List<CreatureBuilder>();
            Lcb.Add(cbP);
            Lcb.Add(cbS);
            Lcb.Add(cbL);
            Lcb.Add(cbK);
            Lcb.Add(cbG);
            CreatedMap.ShowMap();
            //Задание кодировки (надо ли сейчас, хз)
            Console.OutputEncoding = Encoding.Unicode;
            RefreshFrame(ref Lcb);
            //CreatedMap.testMap[cbP.getX(), cbP.getY()] = cbP.getIcon();
            //CreatedMap.testMap[cbS.getX(), cbS.getY()] = cbS.getIcon();
            //CreatedMap.testMap[cbL.getX(), cbL.getY()] = cbL.getIcon();
            //CreatedMap.testMap[cbK.getX(), cbK.getY()] = cbK.getIcon();
            //ConsoleKeyInfo keyInfo;

            context = new Context();
           
        }

        //Тут с каждым шагом выполняется та или иная стратегия (они не особо отличаются)
        public static void stepGame(string keyInfo)
        {
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
            VisualCharacters.mapview = new List<string>();
        }
    }
}

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
using rdgwatp_Android.src.map.Log;
using System.Timers;
using rdgwatp_Android.src.map.Score;
using ConsoleApp1.src.map.TacticalMovement.FightScene;

namespace ConsoleApp1.src.map
{
    class map
    {
        private static Context context;
        private static List<CreatureBuilder> Lcb;
        private static Timer myTimer;
        //Обновление фрейма с каждым шагом 
        private static void RefreshFrame(ref List<CreatureBuilder> Lcb)
        {
            //Обновление перспективы игрока
            CreatedMap.updatePPmap();
            foreach (CreatureBuilder cb in Lcb)
            {
                //Обновление местонахождения каждого из существ
                Rendering.WriteAt(CreatedMap.testMap[cb.getLastX(), cb.getLastY()], cb.getLastX(), cb.getLastY());
                Rendering.WriteAt(cb.getIcon(), cb.getX(), cb.getY());
            }
            //Обновление карты на странице MapPage.xaml
            FastForwardChangeXMLObjects.Process("");
        }
        public static void StartMap(bool nextLvl)
        {
                //Создание таймера на обновления лога
                myTimer = new System.Timers.Timer();
                //подписка таймера
                myTimer.Elapsed += new ElapsedEventHandler(clearLogger);
                //Интервал
                myTimer.Interval = 3000;
                //апуск      
                myTimer.Enabled = true;

            Drunkard.Initialize(nextLvl); // Создаём рандомную мапу
            short PcoorX = (short)Drunkard.RstartPointX, //Тестовые координаты для игрока
                 PcoorY = (short)Drunkard.RstartPointY;

            // CreatedMap.fillMap();
            CreatedMap.CreateGraph(); // Создание графа для карты, по нему перемещаются существа
            
            Director director = new Director();
            if (nextLvl == false)
            {
                //Список существ
                Lcb = new List<CreatureBuilder>();
                //Создание отдельной оболочки для игрока
                CreatureBuilder cbP = new CreatureBuilder();
                //Заполнение оболочки игрока
                director.constructPlayer(cbP, PcoorX, PcoorY);
                Lcb.Add(cbP);
            }
            else {
                Lcb[0].setX(PcoorX);
                Lcb[0].setY(PcoorY);
            }
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
            
            RefreshFrame(ref Lcb);
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
        public static void NextLvl()
        {
            if (Lcb.Count > 1)
                Lcb.RemoveRange(1, Lcb.Count - 1);

            myTimer.Close();
            VisualCharacters.mapview = new List<string> { };
            VisualCharacters.PlayerPerspectiveMV = new List<string> { };
            StartMap(true);
        }
        public static void Clear() //Очистка при выходе из окна
        {
            fight.dirtyThreadCount = 0;
            Scorer.dropScore();
            context = null;
            if(myTimer!=null)
                myTimer.Close();
            Lcb = null;
            VisualCharacters.mapview = new List<string> { };
            VisualCharacters.PlayerPerspectiveMV = new List<string> { };
        }
        private static void clearLogger(object source, ElapsedEventArgs e) 
        {
            Logger.dropRecreate();
        }
    }
}

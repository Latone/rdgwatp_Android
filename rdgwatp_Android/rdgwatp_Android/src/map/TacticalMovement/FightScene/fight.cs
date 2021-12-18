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
                    waitHandler.Set();
                    cb.setDisableTime(3);
                    return;
            }
            //Переброс предметов из карманов противника в грязные карманцы хобиттса
            if (cb.getHP() < 1)
            {
                foreach (ItemType it in cb.getInventory())
                {
                    cbP.getInventory().Add(it);
                }
                waitHandler.Set();
                return;
            }
        }
        public static void Start(CreatureBuilder cbL, CreatureBuilder cbPL)
        {
            waitHandler.WaitOne();
            cb = cbL;
            cbP = cbPL;
            waitHandler.WaitOne();

            //Организовать очередь потоков на бой (каждого из врагов)


            //Построение графа для ориентирования по окну боя
            //List<CustomCollectionForFightSceneGraph> graph = FightGraph.fillGraph();
            //Показ окна боя
            //ShowScreen();
            //ConsoleKeyInfo keyInfo;
            //do
            //{
            //Перемещение по окну боя
            //keyInfo = Console.ReadKey();
            //switch (keyInfo.Key)
            //{
            /* case ConsoleKey.UpArrow:
                 if (graph.Where(c => c.getisHere() == true && c.getEdges().Contains(DirectionType.UP)).Any())
                 {
                     //Запоминание последней ячейки
                     int LastEdge = graph.Find(c => c.getisHere() == true && c.getEdges().Contains(DirectionType.UP)).getThisEdgeNumber();
                     int SpecificNumberToCorrectTravelingAround = 2; //Из состовляющих соображений, это надо для путешествия вверх/вниз 
                     if (LastEdge % 2 == 1)                          //от вершин 4 или 5 в четырёхугольнике (2,4,5,6)
                         SpecificNumberToCorrectTravelingAround = 3;
                     //Переписывание двух кейсов
                     Rendering.RewriteTwoCasesInAFight(graph.Find(c => c.getisHere() == true).getX(),
                                                  graph.Find(c => c.getisHere() == true).getY(),
                                                     graph.ElementAt(LastEdge - SpecificNumberToCorrectTravelingAround).getX(),
                                                         graph.ElementAt(LastEdge - SpecificNumberToCorrectTravelingAround).getY(),
                                                             graph.Find(c => c.getisHere() == true).getTwoConditions(),
                                                                 graph.ElementAt(LastEdge - SpecificNumberToCorrectTravelingAround).getTwoConditions());
                     //Для определения: где находится "курсор"
                     graph.ElementAt(LastEdge).setisHere(false);
                     graph.ElementAt(LastEdge - SpecificNumberToCorrectTravelingAround).setisHere(true);
                 }
                 break;
             case ConsoleKey.DownArrow:
                 if (graph.Where(c => c.getisHere() == true && c.getEdges().Contains(DirectionType.DOWN)).Any())
                 {
                     //Запоминание последней ячейки
                     int LastEdge = graph.Find(c => c.getisHere() == true && c.getEdges().Contains(DirectionType.DOWN)).getThisEdgeNumber();
                     int SpecificNumberToCorrectTravelingAround = 2; //Из состовляющих соображений, это надо для путешествия вверх/вниз 
                     if (LastEdge % 2 == 1)                          //от вершин 4 или 5 в четырёхугольнике (2,4,5,6)
                         SpecificNumberToCorrectTravelingAround = 1;
                     //Переписывание двух кейсов
                     Rendering.RewriteTwoCasesInAFight(graph.Find(c => c.getisHere() == true).getX(),
                                                  graph.Find(c => c.getisHere() == true).getY(),
                                                     graph.ElementAt(LastEdge + SpecificNumberToCorrectTravelingAround).getX(),
                                                         graph.ElementAt(LastEdge + SpecificNumberToCorrectTravelingAround).getY(),
                                                             graph.Find(c => c.getisHere() == true).getTwoConditions(),
                                                                 graph.ElementAt(LastEdge + SpecificNumberToCorrectTravelingAround).getTwoConditions());
                     //Для определения: где находится "курсор"
                     graph.ElementAt(LastEdge).setisHere(false);
                     graph.ElementAt(LastEdge + SpecificNumberToCorrectTravelingAround).setisHere(true);
                 }
                 break;
             case ConsoleKey.LeftArrow:
                 if (graph.Where(c => c.getisHere() == true && c.getEdges().Contains(DirectionType.LEFT)).Any())
                 {
                     //Запоминание последней ячейки
                     int LastEdge = graph.Find(c => c.getisHere() == true && c.getEdges().Contains(DirectionType.LEFT)).getThisEdgeNumber();
                     //Переписывание двух кейсов
                     Rendering.RewriteTwoCasesInAFight(graph.Find(c => c.getisHere() == true).getX(),
                                                  graph.Find(c => c.getisHere() == true).getY(),
                                                     graph.ElementAt(LastEdge - 1).getX(),
                                                         graph.ElementAt(LastEdge - 1).getY(),
                                                             graph.Find(c => c.getisHere() == true).getTwoConditions(),
                                                                 graph.ElementAt(LastEdge - 1).getTwoConditions());
                     //Для определения: где находится "курсор"
                     graph.ElementAt(LastEdge).setisHere(false);
                     graph.ElementAt(LastEdge - 1).setisHere(true);
                 }
                 break;
             case ConsoleKey.RightArrow:
                 if (graph.Where(c => c.getisHere() == true && c.getEdges().Contains(DirectionType.RIGHT)).Any())
                 {
                     //Запоминание последней ячейки
                     int LastEdge = graph.Find(c => c.getisHere() == true && c.getEdges().Contains(DirectionType.RIGHT)).getThisEdgeNumber();
                     //Переписывание двух кейсов
                     Rendering.RewriteTwoCasesInAFight(graph.Find(c => c.getisHere() == true).getX(),
                                                  graph.Find(c => c.getisHere() == true).getY(),
                                                     graph.ElementAt(LastEdge + 1).getX(),
                                                         graph.ElementAt(LastEdge + 1).getY(),
                                                             graph.Find(c => c.getisHere() == true).getTwoConditions(),
                                                                 graph.ElementAt(LastEdge + 1).getTwoConditions());
                     //Для определения: где находится "курсор"
                     graph.ElementAt(LastEdge).setisHere(false);
                     graph.ElementAt(LastEdge + 1).setisHere(true);
                 }
                 break;
                 //Выбор опции
             case ConsoleKey.Enter:
                 InputType it = graph.Where(c => c.getisHere() == true).FirstOrDefault().getInputType();
                 switch (it)
                 {
                     //Показ статов
                     case InputType.STATS:
                         CreatureStats.ShowStats(cb,cbP);
                         ShowScreen();
                         break;
                     //Атака
                     case InputType.ATTACK_MOVE:
                         CreatureFight.RaiseYerHand(ref cb, ref cbP);
                         break;
                     //Просмотр инветаря
                     case InputType.INVENTORY_USE:

                         List<CreatureBuilder> Lcb = new List<CreatureBuilder>();
                         Lcb.Add(cbP);

                         var context = new Context(new StrategyOpenInventory());
                         context.DoSomeBusinessLogic(ref Lcb);
                         ShowScreen();
                         CreatureFight.EnemyRaisedABiggerOne(ref cb, ref cbP);
                         break;
                     //Special Secret Joestar technique
                     case InputType.RUN_AWAY_TACTICS:
                         cb.setDisableTime(3);
                         Console.Clear();
                         CreatedMap.ShowMap();
                         return;
                 }
                 break;
         }*/
            //Апдейт баров существ главного окна боя
            //graph.ElementAt(0).setTwoConditions(MainWindow.UpdateStats(cb));
            //graph.ElementAt(2).setTwoConditions(MainWindow.UpdateStats(cbP));
            //ShowScreen();


            /*//Переброс предметов из карманов противника в грязные карманцы хобиттса
            if (cb.getHP() < 1)
            {
                foreach (ItemType it in cb.getInventory())
                {
                    if (cbP.getInventory().Count < InventoryGraph.v)
                        cbP.getInventory().Add(it);
                    else
                        break;
                }
                //Показ карты
                //Console.Clear();
                //CreatedMap.ShowMap();
                return;
            }*/
            //} while (keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.I);
            //ShowScreen - апдейт баров существ
            /*void ShowScreen()
            {
                MainWindow.ShowMainWindow(cb, cbP, ref graph);
                Rendering.RewriteTwoCasesInAFight(0, 25,
                                                    graph.Find(c => c.getisHere() == true).getX(),
                                                        graph.Find(c => c.getisHere() == true).getY(),
                                                             new String[] { "", "" }, graph.Find(c => c.getisHere() == true).getTwoConditions());
            }*/
        }

    }
}

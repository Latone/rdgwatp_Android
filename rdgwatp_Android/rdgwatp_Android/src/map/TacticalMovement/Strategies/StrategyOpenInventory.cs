using ConsoleApp1.src.creatures.builders;
using ConsoleApp1.src.items;
using ConsoleApp1.src.items.Types;
using ConsoleApp1.src.map.TacticalMovement.LocalVars;
using ConsoleApp1.src.map.TacticalMovement.MinPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.src.map.TacticalMovement.Strategies
{
    class StrategyOpenInventory : IStrategy
    {
        //Реализация открытия инвентаря, использования в нём предметов
        public void DoAlgorithm(ref List<CreatureBuilder> Lcb)
        {
            List<CustomCollectionForInventoryGraph> graph = InventoryGraph.fillGraph(Lcb.Where(c => c.getType()
            == creatures.types.Type.PLAYER).FirstOrDefault().getInventory()); //Немножчк LINQ || Достаём только игрока
            //Визуализация инвентаря
            Console.Clear();
            VisualizedInventory.Visualize(graph);
            Rendering.RewriteTwoCases(3, 0, 0, 0, graph.ElementAt(0).getItemType());

            ConsoleKeyInfo keyInfo;
            //Проверка: открыт ли инвентарь в графе (В драке можно использовать предметы на урон)
            bool UsedItemInFight = false;
            do
            {
                keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    //Далее идёт перемещение стрелочками по инвентарю
                    case ConsoleKey.UpArrow:
                        if (graph.Where(c => c.getisHere() == true && c.getEdges().Contains(c.getThisEdgeNumber() - InventoryGraph.width)).Any())
                        {
                            //Запоминание ячейки,в которой БЫЛ "курсор"
                            int LastEdge = graph.Find(c => c.getisHere() == true && c.getEdges().Contains(c.getThisEdgeNumber() - InventoryGraph.width)).getThisEdgeNumber();
                            //Переписывание ячеек инвентаря, условно визуальный выбор (прошлая и следующая ячейки)
                            Rendering.RewriteTwoCases(graph.Find(c => c.getisHere() == true).getX(),
                                                         graph.Find(c => c.getisHere() == true).getY(),
                                                            graph.ElementAt(LastEdge - InventoryGraph.width).getX(),
                                                                graph.ElementAt(LastEdge - InventoryGraph.width).getY(),
                                                                    graph.ElementAt(LastEdge - InventoryGraph.width).getItemType());
                            //Поправка на нахождение "Курсора"
                            graph.ElementAt(LastEdge).setisHere(false);
                            graph.ElementAt(LastEdge - InventoryGraph.width).setisHere(true);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (graph.Where(c => c.getisHere() == true && c.getEdges().Contains(c.getThisEdgeNumber() + InventoryGraph.width)).Any())
                        {
                            //Запоминание ячейки,в которой БЫЛ "курсор"
                            int LastEdge = graph.Find(c => c.getisHere() == true && c.getEdges().Contains(c.getThisEdgeNumber() + InventoryGraph.width)).getThisEdgeNumber();
                            //Переписывание ячеек инвентаря, условно визуальный выбор (прошлая и следующая ячейки)
                            Rendering.RewriteTwoCases(graph.Find(c => c.getisHere() == true).getX(),
                                                         graph.Find(c => c.getisHere() == true).getY(),
                                                            graph.ElementAt(LastEdge + InventoryGraph.width).getX(),
                                                                graph.ElementAt(LastEdge + InventoryGraph.width).getY(),
                                                                    graph.ElementAt(LastEdge + InventoryGraph.width).getItemType());
                            //Поправка на нахождение "Курсора"
                            graph.ElementAt(LastEdge).setisHere(false);
                            graph.ElementAt(LastEdge + InventoryGraph.width).setisHere(true);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (graph.Where(c => c.getisHere() == true && c.getEdges().Contains(c.getThisEdgeNumber() - 1)).Any())
                        {
                            //Запоминание ячейки,в которой БЫЛ "курсор"
                            int LastEdge = graph.Find(c => c.getisHere() == true && c.getEdges().Contains(c.getThisEdgeNumber() - 1)).getThisEdgeNumber();
                            //Переписывание ячеек инвентаря, условно визуальный выбор (прошлая и следующая ячейки)
                            Rendering.RewriteTwoCases(graph.Find(c => c.getisHere() == true).getX(),
                                                         graph.Find(c => c.getisHere() == true).getY(),
                                                            graph.ElementAt(LastEdge - 1).getX(),
                                                                graph.ElementAt(LastEdge - 1).getY(),
                                                                    graph.ElementAt(LastEdge - 1).getItemType());
                            //Поправка на нахождение "Курсора"
                            graph.ElementAt(LastEdge).setisHere(false);
                            graph.ElementAt(LastEdge - 1).setisHere(true);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (graph.Where(c => c.getisHere() == true && c.getEdges().Contains(c.getThisEdgeNumber() + 1)).Any())
                        {
                            //Запоминание ячейки,в которой БЫЛ "курсор"
                            int LastEdge = graph.Find(c => c.getisHere() == true && c.getEdges().Contains(c.getThisEdgeNumber() + 1)).getThisEdgeNumber();
                            //Переписывание ячеек инвентаря, условно визуальный выбор (прошлая и следующая ячейки)
                            Rendering.RewriteTwoCases(graph.Find(c => c.getisHere() == true).getX(),
                                                         graph.Find(c => c.getisHere() == true).getY(),
                                                            graph.ElementAt(LastEdge + 1).getX(),
                                                                graph.ElementAt(LastEdge + 1).getY(),
                                                                    graph.ElementAt(LastEdge + 1).getItemType());
                            //Поправка на нахождение "Курсора"
                            graph.ElementAt(LastEdge).setisHere(false);
                            graph.ElementAt(LastEdge + 1).setisHere(true);
                        }
                        break;
                        //Использование предмета
                    case ConsoleKey.Enter:
                        //Проверка, что клетка не пуста
                        if (graph.Find(c => c.getisHere() == true).getItemType().GetSubType() == SubType.NONE)
                            break;
                        //Просмотр типа предмета
                        switch (graph.Find(c => c.getisHere() == true).getItemType().GetSubType())
                        {
                            //Защита
                            case SubType.ARMOUR:
                                Lcb.FirstOrDefault().setARMOR((short)(Lcb.FirstOrDefault().getARMOR() +
                                    graph.Find(c => c.getisHere() == true).getItemType().GetPoints()));
                                break;
                            //Хп
                            case SubType.HP:
                                Lcb.FirstOrDefault().setHP((short)(Lcb.FirstOrDefault().getHP() +
                                    graph.Find(c => c.getisHere() == true).getItemType().GetPoints()));
                                break;
                            //IncreaseStats - в конечном итоге повышает мощь критического удара
                            case SubType.IncreaseStats:
                                Lcb.FirstOrDefault().setPower_Crit_Attack((short)(Lcb.FirstOrDefault().getPower_Crit_Attack() +
                                    graph.Find(c => c.getisHere() == true).getItemType().GetPoints()));
                                break;
                            //Предмет на урон (используется только в бою)
                            case SubType.DMG:
                                if (Lcb.FirstOrDefault().getIsFighting())
                                {
                                    //Тут прибавляется дамаг предмета к базовому значению. После удара статы, данные предметов спадают
                                    Lcb.FirstOrDefault().setDMG((short)(Lcb.FirstOrDefault().getDMG() +
                                        graph.Find(c => c.getisHere() == true).getItemType().GetPoints()));
                                    //Зато тут увеличивается базовый урон игрока на 0.1 от значения предмета
                                    Lcb.FirstOrDefault().setDefaultDMG((short)(Lcb.FirstOrDefault().getDefaultDMG() +
                                        graph.Find(c => c.getisHere() == true).getItemType().GetPoints()*0.1));
                                }
                                break;
                        }
                        //Проверка на файт
                        if(Lcb.FirstOrDefault().getIsFighting())
                            UsedItemInFight = true;
                        //Условие даёт разрешение на использование предмета в бою и не даёт его в обычном хождении
                        //!Лишь один предмет можно использовать в бою ЗА ХОД, затем ходит враг!
                        if ((!Lcb.FirstOrDefault().getIsFighting() && graph.Find(c => c.getisHere() == true).getItemType().GetSubType() != SubType.DMG) || Lcb.FirstOrDefault().getIsFighting())
                        {
                            graph.Find(c => c.getisHere() == true && c.getItemType().GetSubType() != SubType.NONE).getItemType().UseItem();
                            int theEdge = graph.Find(c => c.getisHere() == true).getThisEdgeNumber();

                            Rendering.EraseIconAt(graph.ElementAt(theEdge).getX(),
                                                    graph.ElementAt(theEdge).getY());
                        }
                        break;
                    case ConsoleKey.I:
                        break;
                }
                //Закрытие инвентаря
            } while (keyInfo.Key != ConsoleKey.Escape && keyInfo.Key != ConsoleKey.I && !(Lcb.FirstOrDefault().getIsFighting() && UsedItemInFight));
            //Уборка использованных предметов из инвентаря
            for (int i = 0; i < Lcb.FirstOrDefault().getInventory().Count; i++)
            {
                if (Lcb.FirstOrDefault().getInventory().ElementAt(i).GetSubType() == SubType.NONE)
                {
                    Lcb.FirstOrDefault().getInventory().RemoveAt(i);
                    i--;
                }
            }
            //В зависимости: Идёт борьба или нет - показывается соответствующий экран
            Console.Clear();
            if (!Lcb.FirstOrDefault().getIsFighting())
                CreatedMap.ShowMap();
        }
    }
}

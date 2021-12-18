using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.src.map.TacticalMovement.GraphCollection.FightGraph
{
    //Графи для окна боя
    class FightGraph
    {
        public static int height = 4; // Кол-во строк
        public static int width = 2; // Кол-во столбцов
        public static int v = height * width; // "Площадь"
        public static List<CustomCollectionForFightSceneGraph> fillGraph() //Создание графа
        {
            List<CustomCollectionForFightSceneGraph> adj = new List<CustomCollectionForFightSceneGraph>(v);
            for (int i = 0; i < v; i++)
            {
                adj.Add(new CustomCollectionForFightSceneGraph());
                adj.ElementAt(i).SetThisEdgeNumber(i);
            }
            //Самовыстроенная структура:
            // [0]   1   // 0 - Stats
            //  |
            // [2]   3   // 2 - Stats
            //  |  \
            // [4]--[5]  // 4 - Attack ; 5 - Inventory
            //  |  /
            // [6]   7   // 6 - RunAway

            //Прокладывание дорог
            adj.ElementAt(0).AddEdge(DirectionType.DOWN);
            adj.ElementAt(2).AddEdge(DirectionType.UP);

            adj.ElementAt(2).AddEdge(DirectionType.DOWN);
            adj.ElementAt(4).AddEdge(DirectionType.UP);

            adj.ElementAt(4).AddEdge(DirectionType.RIGHT);
            adj.ElementAt(5).AddEdge(DirectionType.LEFT);

            adj.ElementAt(4).AddEdge(DirectionType.DOWN);
            adj.ElementAt(6).AddEdge(DirectionType.UP);

            adj.ElementAt(5).AddEdge(DirectionType.UP);
            adj.ElementAt(5).AddEdge(DirectionType.DOWN);
            //Назначение типа для каждой из вершин
            adj.ElementAt(0).setInputType(InputType.STATS);
            adj.ElementAt(2).setInputType(InputType.STATS);
            adj.ElementAt(4).setInputType(InputType.ATTACK_MOVE);
            adj.ElementAt(5).setInputType(InputType.INVENTORY_USE);
            adj.ElementAt(6).setInputType(InputType.RUN_AWAY_TACTICS);
            //Выделение ячейки, в которй находится "курсор"
            adj.ElementAt(4).setisHere(true);
            return adj;
        }
    }
}

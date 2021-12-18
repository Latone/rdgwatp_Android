using ConsoleApp1.src.map.TacticalMovement.LocalVars;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.src.map.TacticalMovement.MinPath
{
    //Построение графа для карты
    public static class Graph
    {
        //Сам граф adj
        public static List<List<int>> adj;
        //Его "Площадь"
        public static int v = CreatedMap.Mwidth * CreatedMap.Mheight;
        //Сам BFS
        private static bool BFS(int src,
                                 int dest, int v, int[] pred, int[] dist) //сам BFS <*DFS, сорян там другая логика требуется, но ты тоже special*>
        {
            LinkedList<int> queue = new LinkedList<int>();
            //Проверка на посещённые точки
            bool[] visited = new bool[v];
            //Настрой каждой из точки (как непосещённые)
            for (int i = 0; i < v; i++)
            {
                visited[i] = false;
                dist[i] = int.MaxValue;
                pred[i] = -1;
            }
            //Добавление начальной точки, с которой пойдёт прохождение по графу
            visited[src] = true;
            dist[src] = 0;
            queue.AddLast(src);

            while (queue.Any())
            {
                int u = queue.First();
                queue.RemoveFirst();
                for (int i = 0; i < adj.ElementAt(u).Count; i++)
                {
                    if (visited[adj.ElementAt(u).ElementAt(i)] == false)
                    {
                        //Точка была посещена
                        visited[adj.ElementAt(u).ElementAt(i)] = true;
                        //Дистанция соседних вершин в массиве dist на 1 больше (от точки u)
                        dist[adj.ElementAt(u).ElementAt(i)] = dist[u] + 1;
                        //Запись данной точки в массив predicate
                        pred[adj.ElementAt(u).ElementAt(i)] = u;
                        //Добавление соседей в очередь
                        queue.AddLast(adj.ElementAt(u).ElementAt(i));
                        //Если прибыл - выход
                        if (adj.ElementAt(u).ElementAt(i) == dest)
                            return true;
                    }
                }
            }
            return false;
        }
        public static void addEdge(int i, int j) //Добавление дуг
        {
            adj[i].Add(j);
        }
        public static LinkedList<short> getShortestDistance(
                             int s, int dest) //getPath 
        {
            int[] pred = new int[v];
            int[] dist = new int[v];

            //Проверка на существование кратчайшего пути между точками
            if (BFS(s, dest, v, pred, dist) == false)
            {
                Console.WriteLine("Given source and destination" +
                                             "are not connected! s:" + s +" dest:"+dest);
                return null;
            }
            //Выстроение кратчайшего пути
            LinkedList<short> path = new LinkedList<short>();
            int crawl = dest;
            path.AddLast((short)crawl);
            while (pred[crawl] != -1)
            {
                path.AddLast((short)pred[(short)crawl]);
                crawl = pred[crawl];
            }
            return path;
        }
        //fillGraph - построение графа (его мосты)
        public static void fillGraph() //Создание графа
        {
            adj = new List<List<int>>(v);
            //Добавление пустой вершины
            for (int i = 0; i < v; i++)
            {
                adj.Add(new List<int>());
            }
            //Добавление мостов
            for (int j = 0; j < CreatedMap.Mheight; j++)
            {
                for (int i = 0; i < CreatedMap.Mwidth; i++)
                {
                    if (VisualCharacters.WallPattern.Contains(CreatedMap.testMap[i, j]))
                        continue;
                    if (j + 1 < CreatedMap.Mwidth && !VisualCharacters.WallPattern.Contains(CreatedMap.testMap[i, j + 1]))
                        addEdge(i * CreatedMap.Mheight + j, i * CreatedMap.Mheight + j + 1);

                    if(j-1>=0 && !VisualCharacters.WallPattern.Contains(CreatedMap.testMap[i, j - 1]))
                        addEdge(i * CreatedMap.Mheight + j, i * CreatedMap.Mheight + j - 1);

                    if(i+1 < CreatedMap.Mheight && !VisualCharacters.WallPattern.Contains(CreatedMap.testMap[i + 1, j]))
                        addEdge(i * CreatedMap.Mheight + j, (i+1) * CreatedMap.Mheight +j);

                    if (i - 1 >=0 && !VisualCharacters.WallPattern.Contains(CreatedMap.testMap[i - 1, j]))
                        addEdge(i * CreatedMap.Mheight + j, (i-1) * CreatedMap.Mheight + j);
                }
            }
        }
       
    }
}

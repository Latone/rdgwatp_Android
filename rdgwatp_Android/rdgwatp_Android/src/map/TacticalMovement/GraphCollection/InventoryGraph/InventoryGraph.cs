using ConsoleApp1.src.items;
using ConsoleApp1.src.items.Types;
using ConsoleApp1.src.map.TacticalMovement.LocalVars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.src.map.TacticalMovement.MinPath
{
    //Построение графа для инвентаря
    class InventoryGraph
    {
        public static int height = 3; // Кол-во строк
        public static int width = 6; // Кол-во столбцов
        public static int v = height * width; // "Площадь"
        public static List<CustomCollectionForInventoryGraph> fillGraph(List<ItemType> inventory) //Создание графа
        {
            List<CustomCollectionForInventoryGraph> adj;
            adj = new List<CustomCollectionForInventoryGraph>(v);
            //Добавление пустых вершин
            for (int i = 0; i < v; i++)
            {
                adj.Add(new CustomCollectionForInventoryGraph());
            }
            //Назначение первой выделенной точки
            adj.ElementAt(0).setisHere(true);
            //Построение мостов в графе
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if(i+1<width)
                        adj.ElementAt(j * width + i).AddEdge(j * width + i+1);
                    if(i-1>=0)
                        adj.ElementAt(j * width + i).AddEdge(j * width + i - 1);
                    if(j+1<height)
                        adj.ElementAt(j * width + i).AddEdge((j + 1) * width + i);
                    if (j - 1 >= 0)
                        adj.ElementAt(j * width + i).AddEdge((j - 1) * width + i);
                    if (j * width + i < inventory.Count())
                        adj.ElementAt(j * width + i).setItemType(inventory.ElementAt(j * width + i));
                    else if (j * height + i < v)
                    {
                        adj.ElementAt(j * width + i).setItemType(new ItemType());
                        adj.ElementAt(j * width + i).getItemType().SetSubType(SubType.NONE);
                    }
                    adj.ElementAt(j * width + i).SetThisEdgeNumber(j * width + i);
                    adj.ElementAt(j * width + i).setX((short)(i * VisualCharacters.caseSize));
                    adj.ElementAt(j * width + i).setY((short)(j * VisualCharacters.caseSize));
                }
            }
            return adj;
        }
    }
}

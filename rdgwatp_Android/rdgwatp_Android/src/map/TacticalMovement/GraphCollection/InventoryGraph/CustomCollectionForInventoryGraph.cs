using ConsoleApp1.src.items;
using ConsoleApp1.src.items.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.src.map.TacticalMovement.MinPath
{
    //Кастомная коллеция для графа в инвентаре
    class CustomCollectionForInventoryGraph
    {
        public void setisHere(bool ishere)
        {
            this.ishere = ishere;
        }
        public void setItemType(ItemType ItemType)
        {
            this.ItemType = ItemType;
        }
        public void AddEdge(int NUM)
        {
            Edges.Add(NUM);
        }
        public void SetThisEdgeNumber(int NUM)
        {
            this.NUM = NUM;
        }
        public void setX(short x)
        {
            this.x = x;
        }
        public void setY(short y)
        {
            this.y = y;
        }
        public bool getisHere()
        {
            return ishere;
        }
        public List<int> getEdges()
        {
            return Edges;
        }
        public ItemType getItemType()
        {
            return ItemType;
        }
        public int getThisEdgeNumber()
        {
            return NUM;
        }
        public short getX()
        {
            return x;
        }
        public short getY()
        {
            return y;
        }
        //Мосты между соседними выршинами данной точки
        private List<int> Edges = new List<int>();
        //Объявление типа придмета для данной ячейки инвентаря
        private ItemType ItemType;
        //Объявление номера вершины
        private int NUM;
        //Находится ли здесь "указатель"
        private bool ishere;
        //Координаты начала кейса
        private short x;
        private short y;
    }
}

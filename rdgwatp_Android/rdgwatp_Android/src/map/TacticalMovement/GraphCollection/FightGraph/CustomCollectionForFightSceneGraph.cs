using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.src.map.TacticalMovement.GraphCollection.FightGraph
{
    //Кастомная коллеция для графа в драке
    class CustomCollectionForFightSceneGraph
    {
        public void setTwoConditions(String[] str)
        {
            StrConditions = str;
        }
        public void setisHere(bool ishere)
        {
            this.ishere = ishere;
        }
        public void setInputType(InputType it)
        {
            this.it = it;
        }
        public void AddEdge(DirectionType dt)
        {
            Edges.Add(dt);
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
        public String[] getTwoConditions()
        {
            return StrConditions;
        }
        public bool getisHere()
        {
            return ishere;
        }
        public InputType getInputType()
        {
            return it;
        }
        public List<DirectionType> getEdges()
        {
            return Edges;
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
        private List<DirectionType> Edges = new List<DirectionType>();
        //Объявление номера вершины
        private int NUM;
        //Находится ли здесь "указатель"
        private bool ishere;
        //Координаты начала кейса
        private short x;
        private short y;
        //Тип ввода
        private InputType it;
        //
        private String[] StrConditions;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.src.map.Gen
{
    class MapCollection
    {
        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }

        public void setAmountNei(int num)
        {
            amount_nei = num;
        }

        public void setParametrs(int num_nei, int x_nei, int y_nei)
        {
            this.num_nei = num_nei;
            this.x_nei = x_nei;
            this.y_nei = y_nei;
            Parametrs.Add(num_nei);
            Parametrs.Add(x_nei);
            Parametrs.Add(y_nei);
        }

        public void setSides(List<int> Parametrs)
        {
            Sides.Add(Parametrs);
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public List<List<int>> getSides()
        {
            return Sides;
        }

        public int getAmountNei()
        {
            return amount_nei;
        }

        public int getNumberNei()
        {
            return num_nei;
        }

        public int getXNei()
        {
            return x_nei;
        }

        public int getYNei()
        {
            return y_nei;
        }

        //Список всех соседей для данной комнаты по пармаетрам
        private List<List<int>> Sides = new List<List<int>>();
        //Список параметров: номер, координата по x, координата по y
        private List<int> Parametrs = new List<int>();
        //Координаты данной комнаты
        private int x;
        private int y;
        //Количество соседей
        private int amount_nei;
        //Номер соседа
        private int num_nei;
        //Координаты соседа
        private int x_nei;
        private int y_nei;
    }
}

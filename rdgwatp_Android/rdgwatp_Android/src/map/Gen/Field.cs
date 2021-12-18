using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.src.map.Gen
{
    public class Field
    {
        Random random = new Random();
        private int num_rooms;
        private int[,] map;
        private int x, y;
        private int[] model; //Список комнат с количеством выходов для каждой.

        public Field(int n)
        {
            num_rooms = n;
            x = n*3;
            y = n*3;
            map = new int[x,y];
            model = maket();
        }

        private int[] maket() //Метод для заполнения списка комнат. В нес создается модель с минимальным количеством, полученных комнат и максимум выходов для каждой.
        {
            int[] maket = new int[num_rooms];
            for (int i = 0; i < num_rooms; i++)
            {
                maket[i] = random.Next(1, 5);
            }
            return maket;
        }

        public void create()
        {
            int new_x = x / 2; //Начальные позиции для корневой комнаты.
            int new_y = y / 2;
            map[new_x, new_y] = 1;
            int step_x = 0; //Значения, прибовляемые к координатам для выбора стороны хода (вверх, вниз, ввлево, вправо).
            int step_y = 0;
            int signal_to_move; //Собственно, сигнал выбора стороны.
            for (int i = 0; i < model.Length; i++)
            {
                for(int j = 0; j < model[i]; j++)
                {
                    signal_to_move = random.Next(1, model[i] + 1); //По количеству выходов для данной комнаты рвндомно выбирается сторона.
                    if (signal_to_move == 1 && map[new_x, new_y + 1] == 0) //После стороны, происходит сам переход.
                    {
                        step_x = 0;
                        step_y = 1;
                        new_x = new_x + step_x;
                        new_y = new_y + step_y;
                        map[new_x, new_y] = 1;
                    }
                    if (signal_to_move == 2 && map[new_x + 1, new_y] == 0)
                    {
                        step_x = 1;
                        step_y = 0;
                        new_x = new_x + step_x;
                        new_y = new_y + step_y;
                        map[new_x, new_y] = 1;
                    }
                    if (signal_to_move == 3 && map[new_x, new_y - 1] == 0)
                    {
                        step_x = 0;
                        step_y = -1;
                        new_x = new_x + step_x;
                        new_y = new_y + step_y;
                        map[new_x, new_y] = 1;
                    }
                    if (signal_to_move == 4 && map[new_x - 1, new_y] == 0)
                    {
                        step_x = -1;
                        step_y = 0;
                        new_x = new_x + step_x;
                        new_y = new_y + step_y;
                        map[new_x, new_y] = 1;
                    }
                }
            }
        }

        public void print() 
        {
            for (int i = 0; i < x; i++) 
            {
                for(int j = 0; j < y; j++)
                {
                    if (map[i, j] != 0)
                        Console.Write("*");
                    else
                        Console.Write(" ");
                }
                Console.WriteLine(" ");
            }

        }
    }
}

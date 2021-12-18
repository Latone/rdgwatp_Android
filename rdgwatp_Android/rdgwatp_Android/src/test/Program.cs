using ConsoleApp1.src.map;
using ConsoleApp1.src.map.TacticalMovement.MinPath;
using System;
namespace ConsoleApp1
{
    //Тестовый старт игры
    //Тестовая надпись для проверки работоспособности stash #1
    ////Тестовая надпись для проверки работоспособности stash #2
    //La-Li-Lu-Le-Lo
    class Program
    {
        public static void Main(string[] args)
        {
            map map = new map(); //От загрузки мапы всё остальное идёт
            map.StartMap();
        }

    }
}

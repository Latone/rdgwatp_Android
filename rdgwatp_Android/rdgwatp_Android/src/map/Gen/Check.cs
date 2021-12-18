using ConsoleApp1.src.map.Gen;
using System;

public class Check
{
    public static void Main(string[] args)
    {
        Field map = new Field(5);
        map.create();
        map.print();
    }
}

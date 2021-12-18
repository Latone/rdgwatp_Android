using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.src.items
{
    //Типа интерфейса
    abstract class Creator
    {
        //НТЕРФЕЙС ИНТЕРФЕЙСА!! УУУ
        public abstract Iitems FactoryMethod();

        public ItemType SomeOperation()
        {
            var item = FactoryMethod();

            var result = item.FullFill();

            return result;
        }
    }
}

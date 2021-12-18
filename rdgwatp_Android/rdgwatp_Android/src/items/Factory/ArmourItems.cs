using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.src.items
{
    // Конкретные Создатели переопределяют фабричный метод для того, чтобы
    // изменить тип результирующего айтема.
    class Armouritems : Creator
    {
        public override Iitems FactoryMethod()
        {
            return new RandomArmourItem();
        }
    }
    //Различные реализации Iitems, другие смотреть в других классах
    class RandomArmourItem : Iitems
    {
        public ItemType FullFill()
        {
            ItemType it = new ItemType();
            it.SetSubType(Types.SubType.ARMOUR);
            it.SetIcon('⛨');
            Random rnd = new Random();
            switch (rnd.Next(1, 10))
            {
                case 1:
                    it.SetPoints(5);
                    it.SetName("Ведро");
                    it.SetLore("Вам не идёт. Можно надеть");
                    break;
                case 2:
                    it.SetPoints(15);
                    it.SetName("Изготовленный парафин");
                    it.SetLore("Похоже, что он был оборван с забора. Можно надеть");
                    break;
                case 3:
                    it.SetPoints(10);
                    it.SetName("Тёплые змеиные пагоны");
                    it.SetLore("Шкр-шкр - издаёт кожа. Можно надеть");
                    break;
                case 4:
                    it.SetPoints(35);
                    it.SetName("Доспех земноморца");
                    it.SetLore("Кольчуга хорошего изделия. Можно надеть");
                    break;
                case 5:
                    it.SetPoints(2);
                    it.SetName("Тряпка");
                    it.SetLore("Приятное на ощупь изделие. Можно надеть");
                    break;
                case 6:
                    it.SetPoints(12);
                    it.SetName("Перчатки, измытые в цементе");
                    it.SetLore("Перед тем, как надевать, обматайте тряпкой руки. Можно надеть");
                    break;
                case 7:
                    it.SetPoints(6);
                    it.SetName("Балистические очки");
                    it.SetLore("Stylish. Можно надеть");
                    break;
                case 8:
                    it.SetPoints(20);
                    it.SetName("Бронзовый шарф");
                    it.SetLore("В нём дышать невозможно. Можно надеть");
                    break;
                case 9:
                    it.SetPoints(4);
                    it.SetName("Верёвка");
                    it.SetLore("Можно использовать как ремень. Можно надеть");
                    break;
            }
            return it;
        }
    }
}

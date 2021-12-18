using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.src.items
{
    // Конкретные Создатели переопределяют фабричный метод для того, чтобы
    // изменить тип результирующего айтема.
    class HPitems : Creator
    {
        public override Iitems FactoryMethod()
        {
            return new RandomHPItem();
        }
    }
    //Различные реализации Iitems, другие смотреть в других классах
    class RandomHPItem : Iitems
    {
        public ItemType FullFill()
        {
            ItemType it = new ItemType();
            it.SetSubType(Types.SubType.HP);
            it.SetIcon('☕');
            Random rnd = new Random();
            switch (rnd.Next(1, 10))
            {
                case 1:
                    it.SetPoints(3);
                    it.SetName("Булка");
                    it.SetLore("Зачерствевший кусочек переработанной пшеницы");
                break;
                case 2:
                    it.SetPoints(5);
                    it.SetName("Семечки");
                    it.SetLore("5 неизвестных семян");
                    break;
                case 3:
                    it.SetPoints(15);
                    it.SetName("Окорок");
                    it.SetLore("Полусырой кусок мяса, снятый с плоти врага");
                    break;
                case 4:
                    it.SetPoints(30);
                    it.SetName("Элексир молодости");
                    it.SetLore("Применять внутрь тела");
                    break;
                case 5:
                    it.SetPoints(8);
                    it.SetName("Грунтовая вода");
                    it.SetLore("Чистая вода, налитая из пустой ранее бочки под вино");
                    break;
                case 6:
                    it.SetPoints(-15);
                    it.SetName("Неизвестный Пирог");
                    it.SetLore("Чёрный, как сажа");
                    break;
                case 7:
                    it.SetPoints(15);
                    it.SetName("Известный Пирог");
                    it.SetLore("Выглядит вкусно");
                    break;
                case 8:
                    it.SetPoints(-30);
                    it.SetName("Зелёный элексир");
                    it.SetLore("Я где-то уже это видел");
                    break;
                case 9:
                    it.SetPoints(0);
                    it.SetName("Чьи-то надпочечники");
                    it.SetLore("Только гурман способен оценить сей вкусный орган");
                    break;
            }
            return it;
        }
    }
}

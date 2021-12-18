using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.src.items
{
    // Конкретные Создатели переопределяют фабричный метод для того, чтобы
    // изменить тип результирующего айтема.
    class IncreaseStatsitems : Creator
    {
        public override Iitems FactoryMethod()
        {
            return new RandomStatsItem();
        }
    }
    //Различные реализации Iitems, другие смотреть в других классах
    class RandomStatsItem : Iitems
    {
        public ItemType FullFill()
        {
            ItemType it = new ItemType();
            it.SetSubType(Types.SubType.IncreaseStats);
            it.SetIcon('⚙');
            Random rnd = new Random();
            switch (rnd.Next(1, 10))
            {
                case 1:
                    it.SetPoints(1);
                    it.SetName("Напиток Brand");
                    it.SetLore("Заряжает");
                    break;
                case 2:
                    it.SetPoints(1);
                    it.SetName("Книжка с картинками");
                    it.SetLore("Детская книжка, в ней также присутствуют картинки");
                    break;
                case 3:
                    it.SetPoints(2);
                    it.SetName("Касета");
                    it.SetLore("Ярость бурлит от того, что у вас нет средства воспроизведения сия объекта");
                    break;
                case 4:
                    it.SetPoints(2);
                    it.SetName("Блокнот с шутками");
                    it.SetLore("Блокнот с модерн-шутками");
                    break;
                case 5:
                    it.SetPoints(4);
                    it.SetName("Плакат");
                    it.SetLore("На плакате изображён герой с мечом в руках");
                    break;
                case 6:
                    it.SetPoints(3);
                    it.SetName("Оригами");
                    it.SetLore("Качественно сделанное оригами");
                    break;
                case 7:
                    it.SetPoints(4);
                    it.SetName("Комикс");
                    it.SetLore("Формат чёрно-белый, как в старом кино");
                    break;
                case 8:
                    it.SetPoints(2);
                    it.SetName("Монета неизвестной валюты");
                    it.SetLore("Вам смешно от того, что вы её подняли");
                    break;
                case 9:
                    it.SetPoints(5);
                    it.SetName("Бесцветный соус");
                    it.SetLore("На обратной стороне ёмкости нарисован огонь");
                    break;

            }
            return it;
        }
    }
}

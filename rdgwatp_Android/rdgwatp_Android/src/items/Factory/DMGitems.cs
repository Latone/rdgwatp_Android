using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.src.items
{
    // Конкретные Создатели переопределяют фабричный метод для того, чтобы
    // изменить тип результирующего айтема.
    class DMGitems : Creator
    {
        public override Iitems FactoryMethod()
        {
            return new RandomDMGItem();
        }
    }
    //Различные реализации Iitems, другие смотреть в других классах
    class RandomDMGItem : Iitems
    {
        public ItemType FullFill()
        {
            ItemType it = new ItemType();
            it.SetSubType(Types.SubType.DMG);
            it.SetIcon('⚔');
            Random rnd = new Random();
            switch (rnd.Next(1, 10))
            {
                case 1:
                    it.SetPoints(6);
                    it.SetName("Качественная палка");
                    it.SetLore("Гибкая палка древа берёзы");
                    break;
                case 2:
                    it.SetPoints(11);
                    it.SetName("Шар для боулинга");
                    it.SetLore("Этим можно сбить с ног врага");
                    break;
                case 3:
                    it.SetPoints(75);
                    it.SetName("Подрывной заряд");
                    it.SetLore("Отрывает ноги");
                    break;
                case 4:
                    it.SetPoints(15);
                    it.SetName("Лист бумаги");
                    it.SetLore("Этим порезаться можно");
                    break;
                case 5:
                    it.SetPoints(25);
                    it.SetName("Хлопушки размеров с ладонь");
                    it.SetLore("Враг, разинув рот, впадает вступор, не понимая, почему у вас в руке пиротехника");
                    break;
                case 6:
                    it.SetPoints(35);
                    it.SetName("Файерболл");
                    it.SetLore("Это настоящая магия");
                    break;
                case 7:
                    it.SetPoints(45);
                    it.SetName("Велосипед");
                    it.SetLore("Сломанный, но в то же время острый");
                    break;
                case 8:
                    it.SetPoints(10);
                    it.SetName("Чайник с кипятком");
                    it.SetLore("Сезон ожогов открыт");
                    break;
                case 9:
                    it.SetPoints(0);
                    it.SetName("Потёртый круглый мешок");
                    it.SetLore("Вы не умеете этим пользоваться");
                    break;
            }
            return it;
        }
    }
}

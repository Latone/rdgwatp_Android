using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.src.items
{
    //Построение рандомного инвентаря для какого-то существа
    public static class CreateBasicInventory
    {
        public static List<ItemType> getBasicInventory(byte DecreaseNumberOfItemsBy)
        {
            List<ItemType> Inventory = new List<ItemType>();
            Random rnd = new Random();
            int k = rnd.Next(1, 4); //Кол-во предметов на создание (каждого из существ)
            for (int i = 0; i < k; i++)
            {
                Random rnd2 = new Random();
                switch (rnd2.Next(1, 5))
                {
                    case 1:
                        Inventory.Add(new Armouritems().SomeOperation());
                        break;
                    case 2:
                        Inventory.Add(new DMGitems().SomeOperation());
                        break;
                    case 3:
                        Inventory.Add(new HPitems().SomeOperation());
                        break;
                    case 4:
                        Inventory.Add(new IncreaseStatsitems().SomeOperation());
                        break;
                }
            }
            return Inventory;
        }
    }
}

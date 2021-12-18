using ConsoleApp1.src.items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ConsoleApp1.src.creatures.builders
{
    //Интерфейс для CreatureBuilder
    interface IBuilder : INotifyPropertyChanged
    {
        void setType(types.Type type);
        void setHP(short HP);
        void setCh_Evade(byte Ch_Evade);
        void setCh_Block(byte Ch_Block);
        void setDMG(short DMG);
        void setCh_Crit_Attack(byte Ch_Crit_Attack);
        void setPower_Crit_Attack(short Power_Crit_Attack);
        void setX(short x);
        void setY(short y);
        void setIcon(char icon);
        void setInventory(List<ItemType> inventory);
    }
}

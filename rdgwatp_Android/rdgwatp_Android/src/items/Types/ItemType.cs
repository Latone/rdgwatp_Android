using ConsoleApp1.src.items.Types;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ConsoleApp1.src.items
{
    //Всё то, что содержат предметы
    public class ItemType : INotifyPropertyChanged
    {
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void SetSubType(SubType subType)
        {
            this.subType = subType;
        }
        public void SetPoints(short points)
        {
            this.points = points;
        }
        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetLore(string Lore)
        {
            this.Lore = Lore;
        }
        public void SetIcon(char icon)
        {
            this.icon = icon;
        }

        public char GetIcon()
        {
            return icon;
        }

        public SubType GetSubType()
        {
            return subType;
        }
        public short GetPoints()
        {
            return points;
        }
        public string GetName()
        {
            return name;
        }
        public string GetLore()
        {
            return Lore;
        }
        //Методы
        public void UseItem()
        {
            subType = SubType.NONE;
            NotifyPropertyChanged("subType");
        }
        //Определение, к чему относится данный предмет
        public SubType subType { get; set; }
        //Кол-во профита от предмета
        public short points { get; set; }
        //Название предмета
        public string name { get; set; }
        //Описание
        public string Lore { get; set; }
        //Как выглядит
        public char icon { get; set; }
    }
}

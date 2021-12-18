using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ConsoleApp1.src.creatures.types;
using ConsoleApp1.src.items;

namespace ConsoleApp1.src.creatures.builders
{
    public class CreatureBuilder : IBuilder
    {
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged; //Можно подписываться на него

        //Setter'i

        public void setType(types.Type type)
        {
            this.type = type;
        }

        public void setHP(short HP)
        {
            this.HP = HP;
        }
        public void setMaxHP(short MaxHP)
        {
            this.MaxHP = MaxHP;
        }
        public void setARMOR(short ARMOR)
        {
            this.ARMOR = ARMOR;
        }

        public void setCh_Evade(byte Ch_Evade)
        {
            this.Ch_Evade = Ch_Evade;
        }

        public void setCh_Block(byte Ch_Block)
        {
            this.Ch_Block = Ch_Block;
        }

        public void setDMG(short DMG)
        {
            this.DMG = DMG;
        }
        public void setDefaultDMG(short DefaultDMG)
        {
            this.DefaultDMG = DefaultDMG;
        }
        public void setCh_Crit_Attack(byte Ch_Crit_Attack)
        {
            this.Ch_Crit_Attack = Ch_Crit_Attack;
        }

        public void setPower_Crit_Attack(short Power_Crit_Attack)
        {
            this.Power_Crit_Attack = Power_Crit_Attack;
        }

        public void setX(short x)
        {
            this.x = x;
        }

        public void setY(short y)
        {
            this.y = y;
        }
        public void setLastX(short lastx)
        {
            this.lastx = lastx;
        }

        public void setLastY(short lasty)
        {
            this.lasty = lasty;
        }

        public void setIcon(char icon)
        {
            this.icon = icon;
        }
        public void setChase(bool chase)
        {
            this.chase = chase;
        }
        public void setNoticeRange(short NoticeRange)
        {
            this.NoticeRange = NoticeRange;
        }
        public void setVanishRange(short VanishRange)
        {
            this.VanishRange = VanishRange;
        }
        public void setChasePath(LinkedList<short> ChasePath)
        {
            this.ChasePath = ChasePath;
        }
        public void setPercentageOfTimeThinks(short PercentageOfTimeThinks)
        {
            this.PercentageOfTimeThinks = PercentageOfTimeThinks;
        }
        public void setInventory(List<ItemType> inventory)
        {
            this.inventory = inventory;
        }
        public void setIsFighting(bool fighting)
        {
            this.fighting = fighting;
            if(fighting)
                NotifyPropertyChanged("fighting");
        }
        public void setZeroTriggerFighting(bool Tfighting)
        {
            this.ZeroTriggerFighting = Tfighting;
        }
        public void setEmotions(String[] emoji)
        {
            this.emoji = emoji;
        }
        public void setlastEmotionReproduced(short num)
        {
            this.lastEmotionReproduced = num;
        }
        public void setDisableTime(byte FreezeTime)
        {
            this.FreezeTime = FreezeTime;
        }
        
        //Getter'i
        public types.Type getType()
        {
            return this.type;
        }
        public short getX()
        {
            return x;
        }

        public short getY()
        {
            return y;
        }
        public short getLastX()
        {
            return lastx;
        }

        public short getLastY()
        {
            return lasty;
        }

        public short getHP()
        {
            return HP;
        }
        public void setIsDead(bool dead)
        {
            isDead = dead;
            if (HP < 1)
                NotifyPropertyChanged("got less than 1 hp");
        }
        public bool getIsDead()
        {
            return isDead;
        }
        public short getMaxHP()
        {
            return MaxHP;
        }
        public short getARMOR()
        {
            return ARMOR;
        }
        public byte getCh_Evade()
        {
            return Ch_Evade;
        }

        public byte getCh_Block()
        {
            return Ch_Block;
        }

        public short getDMG()
        {
            return DMG;
        }
        public short getDefaultDMG()
        {
            return DefaultDMG;
        }

        public byte getCh_Crit_Attack()
        {
            return Ch_Crit_Attack;
        }

        public short getPower_Crit_Attack()
        {
            return Power_Crit_Attack;
        }

        public char getIcon()
        {
            return icon;
        }
        public bool getChase()
        {
            return this.chase;
        }
        public short getPercentageOfTimeThinks()
        {
            return PercentageOfTimeThinks;
        }
        public short getNoticeRange()
        {
            return NoticeRange;
        }
        public short getVanishRange()
        {
            return VanishRange;
        }
        public LinkedList<short> getChasePath()
        {
            return ChasePath;
        }
        public List<ItemType> getInventory()
        {
            return inventory;
        }
        public bool getIsFighting()
        {
            return fighting;
        }
        public bool getZeroTriggerFighting()
        {
            return ZeroTriggerFighting;
        }
        public String[] getEmotions(short num, bool notify)
        {
            if(notify)
                NotifyPropertyChanged("emotions");
            setlastEmotionReproduced(num);
            return emoji;
        }
        public short getlastEmotionReproduced()
        {
            return lastEmotionReproduced;
        }
        public byte getDisableTime()
        {
            return FreezeTime;
        }
        
        public int getDebuffTime()
        { if (debuff == null)
                return -1;
            return debuff[0];
        }
        //Некоторые методы
        //Запоминает прошлые координаты
        public void RememberLastCoordinates()
        {
            lastx = x;
            lasty = y;
        }
        public void setDebuffTime(int time)
        {
            debuff[0] = time;
        }
        public void setDebuffStats(int time)
        {
            this.debuff = new int[] { time, Ch_Block, Ch_Evade };
        }
        public void NullDebuffStats()
        {
            Ch_Block = (byte)debuff[1];
            Ch_Evade = (byte)debuff[2];
            debuff = null;
        }

        //Тип ходячего
        private types.Type type;
        //Основные статы
        private short HP;
        private short MaxHP;
        private short ARMOR;
        private byte Ch_Evade;
        private byte Ch_Block;
        private short DMG;
        private short DefaultDMG;
        private byte Ch_Crit_Attack;
        private short Power_Crit_Attack;
        private bool isDead = false;
        //Местность, позиция
        private short x, y;
        private short lastx, lasty;
        //Погоня
        private bool chase;
        private short PercentageOfTimeThinks; //Время препровождения в афк/на месте
        private short NoticeRange;
        private short VanishRange;
        private LinkedList<short> ChasePath;
        //Обозначение на карте
        private char icon;
        //Сам инвентарь существа
        private List<ItemType> inventory;
        //В борьбе или нет
        private bool fighting = false;
        private bool ZeroTriggerFighting = false;
        //Эмоции в борьбе
        private String[] emoji;
        private short lastEmotionReproduced;
        //Заморозка существа
        private byte FreezeTime=0;
        //Дебафф существа
        private int[] debuff = null;
    }
}

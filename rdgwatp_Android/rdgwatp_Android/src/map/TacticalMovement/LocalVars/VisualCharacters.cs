using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.src.map.TacticalMovement.LocalVars
{
    //VisualCharacter - в общем случае вседоступные визуальные оформления
    public static class VisualCharacters
    {
        public static List<string> mapview = new List<string>();
        public static List<string> inventoryview = new List<string>();
        public static List<string> itemDescriptionview = new List<string>();

        public static char[] WallPattern = new char[] { '■' };
        public static char[] FloorPattern = new char[] { '□' }; //Изменится с рандомизацией мапы
        public static char[,] EmptyInventoryCase = new char[,] { {' ', '෴', ' ' }, 
                                                                 { '┫', ' ', '┣' }, 
                                                                 { ' ', 'ᚚ', ' ' }, };

        public static char ChosenElementPattern = (char)9632;
        public static char ChosenElementPatternNONchosen = ' ';
        public static byte caseSize = (byte)EmptyInventoryCase.GetLongLength(0);
    }
}

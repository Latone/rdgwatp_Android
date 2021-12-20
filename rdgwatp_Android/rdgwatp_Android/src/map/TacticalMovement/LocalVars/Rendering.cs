using ConsoleApp1.src.creatures.builders;
using ConsoleApp1.src.items;
using ConsoleApp1.src.items.Types;
using ConsoleApp1.src.map.TacticalMovement.GraphCollection.FightGraph;
using System;

namespace ConsoleApp1.src.map.TacticalMovement.LocalVars
{
    //CV - Console Version
    //Рендер фрейма
    public static class Rendering
    {
        //WriteAt - Визуально обновляет местоположение существ "на" карте
        public static void WriteAt(char s, int x, int y)
        {
            char [] h = VisualCharacters.mapview[y].ToCharArray();
            char[] k = { };
            if (Math.Abs(y - map.getPlayer().getY()) < VisualCharacters.playerVisionRange)
            {
                k = VisualCharacters.PlayerPerspectiveMV[y - map.getPlayer().getY() + VisualCharacters.playerVisionRange].ToCharArray();
                if(Math.Abs(map.getPlayer().getX() - x) < VisualCharacters.playerVisionRange)
                    k[x-map.getPlayer().getX() + VisualCharacters.playerVisionRange] = s;
                VisualCharacters.PlayerPerspectiveMV[y - map.getPlayer().getY() + VisualCharacters.playerVisionRange] = new string(k);
            }
            h[x] = s;
            VisualCharacters.mapview[y] = new string(h);
           
        }
        //RewriteTwoCases - переписывание прошлой и предыдущей ячейки в инвентаре
        public static void RewriteTwoCases(int xOldCase, int yOldCase
                                            , int xNewCase, int yNewCase, ItemType it)
        {
            try
            {
                //Стереть определение предмета
                EraseDesription();

                for (int i = 0; i <= 2; i += 2)
                {
                    for (int j = 0; j <= 2; j += 2)
                    {
                        char[] oldI = VisualCharacters.inventoryview[yOldCase + j].ToCharArray();
                        char[] newI = VisualCharacters.inventoryview[yNewCase + j].ToCharArray();
                        /*Console.SetCursorPosition(xOldCase + i, yOldCase + j); -CV
                        Console.Write(VisualCharacters.ChosenElementPatternNONchosen);
                        Console.SetCursorPosition(xNewCase + i, yNewCase + j);
                        Console.Write(VisualCharacters.ChosenElementPattern);
                        */
                        oldI[xOldCase + i] = VisualCharacters.ChosenElementPatternNONchosen;
                        newI[xNewCase + i] = VisualCharacters.ChosenElementPattern;

                        VisualCharacters.inventoryview[xOldCase + i] = new string(oldI);
                        VisualCharacters.inventoryview[xNewCase + i] = new string(newI);
                    }
                    
                }

                /* CV Console.SetCursorPosition(VisualCharacters.caseSize*VisualCharacters.caseSize,
                     VisualCharacters.caseSize * VisualCharacters.caseSize);*/

                //Если ячейка пуста - описание не требуется
                if (it.GetSubType() == SubType.NONE)
                {
                    
                    return;
                }
                //Описание предмета
                VisualCharacters.itemDescriptionview.Add("Название: " + it.GetName());
                //Console.SetCursorPosition(VisualCharacters.caseSize * 6 + 1, 0);
                //Console.Write("Название: " + it.GetName());
                VisualCharacters.itemDescriptionview.Add("Описание: " + it.GetLore());
                //Console.SetCursorPosition(VisualCharacters.caseSize * 6 + 1, 2);
                //Console.Write("Описание: " + it.GetLore());
                VisualCharacters.itemDescriptionview.Add("Профит: " + it.GetPoints());
                //Console.SetCursorPosition(VisualCharacters.caseSize * 6 + 1, 4);
                //Console.Write("Профит: " + it.GetPoints());
            }
            catch (Exception e)
            {
                //Console.Clear();
                //Console.WriteLine(e.Message);
            }
        }
        //RewriteTwoCasesInAFight - Особое переписывание двух "кнопок", кейсов в окне боя
        public static void RewriteTwoCasesInAFight(int xOldCase, int yOldCase
                                            , int xNewCase, int yNewCase, 
                                            String[] StrOldCase, String[] StrNewCase) 
        {
            //Переписывание старой ячейки
            byte NumberOfStrings = Convert.ToByte(StrOldCase[0].Split('\n').Length);
            //Console.SetCursorPosition(xOldCase, yOldCase);
            for (int i = 0; i < NumberOfStrings; i++)
            {
                //Console.Write(StrOldCase[0].Split('\n')[i]);
                //Console.SetCursorPosition(xOldCase, yOldCase + i+1);
            }
            //Переписывание новой ячейки
            NumberOfStrings = Convert.ToByte(StrNewCase[1].Split('\n').Length);
            //Console.SetCursorPosition(xNewCase, yNewCase);
            for (int i = 0; i < NumberOfStrings; i++)
            {
                //Console.Write(StrNewCase[1].Split('\n')[i]);
               // Console.SetCursorPosition(xNewCase, yNewCase + i+1);
            }
            //Подальше, чтобы не стирал символы под названием кейса
            //Console.SetCursorPosition(0,13); 
        }
        //EraseDesription - стирает описание предмета
        static void EraseDesription()
        {
            VisualCharacters.itemDescriptionview.Clear();
            /*Console.SetCursorPosition(VisualCharacters.caseSize * 6 + 1, 0);
            Console.Write("                                                                                                    ");
            Console.SetCursorPosition(VisualCharacters.caseSize * 6 + 1, 2);
            Console.Write("                                                                                                    ");
            Console.SetCursorPosition(VisualCharacters.caseSize * 6 + 1, 4);
            Console.Write("                                                                                                    ");
        */
        }
        //Стирание иконки предмета в инвентаре
        public static void EraseIconAt(int CaseX, int CaseY)
        {
            char[] tI = VisualCharacters.inventoryview[CaseY + VisualCharacters.caseSize / 2].ToCharArray();
            //Console.SetCursorPosition(CaseX+VisualCharacters.caseSize/2, CaseY+VisualCharacters.caseSize/2);
            tI[CaseX + VisualCharacters.caseSize / 2] = ' ';
            //Console.Write(' ');
            VisualCharacters.inventoryview[CaseY + VisualCharacters.caseSize / 2] = new string(tI);
            EraseDesription();
        }
        
    }
}

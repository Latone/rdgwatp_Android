using ConsoleApp1.src.items.Types;
using ConsoleApp1.src.map.TacticalMovement.MinPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.src.map.TacticalMovement.LocalVars
{
    class VisualizedInventory
    {
        //Вывод в консоль всех ячеек инветаря (18 штук)
        public static void Visualize(List<CustomCollectionForInventoryGraph> graph)
        {
            for (int i = 0; i < InventoryGraph.height; i++)
            {
                for (int j = 0; j < InventoryGraph.width; j++)
                {
                    int position = i * InventoryGraph.width + j;
                    if (graph.ElementAt(position).getItemType().GetSubType() != SubType.NONE)
                    {
                        OutputCase(VisualCharacters.EmptyInventoryCase,
                            i * VisualCharacters.caseSize,
                            j * VisualCharacters.caseSize, graph.ElementAt(position).getItemType().GetIcon());
                    }
                    else
                        OutputCase(VisualCharacters.EmptyInventoryCase,
                            i * VisualCharacters.caseSize,
                            j * VisualCharacters.caseSize, ' ');
                }
            }
        }
        static void OutputCase(char[,]BigIcon, int startI, int startJ,char icon)
        {
            int lengthI = startI + VisualCharacters.caseSize,
                lengthJ = startJ + VisualCharacters.caseSize;
            int DUDstartJ = startJ;
            int i = 0, j = 0;
            for (; startI < lengthI; startI++)
            {
                char[] tI = VisualCharacters.inventoryview[startI].ToCharArray();
                for (; startJ < lengthJ; startJ++)
                {
                    //Console.SetCursorPosition(startJ, startI); -CV

                    if (j == 1 && i == 1 && icon != ' ')
                    {
                        tI[startJ] = icon;
                       // Console.Write(icon); //CV
                    }
                    else
                    {
                        tI[startJ] = BigIcon[i, j];
                        //Console.Write(BigIcon[i, j]); //-CV
                    }
                        j++;
                }
                VisualCharacters.inventoryview[startI] = new string(tI);
                startJ = DUDstartJ;
                j = 0;
                i++;
            }
        }
    }
}

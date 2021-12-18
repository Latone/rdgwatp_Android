using ConsoleApp1.src.creatures.builders;
using ConsoleApp1.src.map.TacticalMovement.GraphCollection.FightGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.src.map.TacticalMovement.FightScene
{
    //Построение главного окна боя
    class MainWindow
    {
        public static void ShowMainWindow(CreatureBuilder cb, CreatureBuilder cbP,
                                          ref List<CustomCollectionForFightSceneGraph> graph)
        {
            List<String[]> LinesOfDescription = new List<String[]>();
            LinesOfDescription.Add(UpdateStats(cb));
            //LinesOfDescription.Add(cb.getEmotions());

            LinesOfDescription.Add(UpdateStats(cbP));

            LinesOfDescription.Add(new String[] { "┏••••••••••••••••••••┓\n"+
                                                  "┗       ATTACK       ׃",
                                                  "┏~~~┳~~┳~~~~~~┳~~┳~~~┓\n"+
                                                  "┗╌╌╌■╌╌■ATTACK■╌╌■╌╌■׃"});

            LinesOfDescription.Add(new String[] {"┏•••••••••••••••••••••┓\n"+
                                                 "׃      INVENTORY      ┛",
                                                 "┏~~┳~~┳~~~~~~~~~┳~~┳~~┓\n"+
                                                 "׃╌╌■╌╌■INVENTORY■╌╌■╌╌┛"});

            LinesOfDescription.Add(new String[] {"•••••••••••••••••••••׃", //В этой версси не доступно
                                                 "■■■■HERO■ABILITIES■■■׃"});

            LinesOfDescription.Add(new String[] { "••••••••••••••••••••••",// В этой версии не доступно
                                                  "■■■■■■TOcONVINCE■■■■■■"});

            LinesOfDescription.Add(new String[] {"          secret technique /escape           ",
                                                 "■■■■■■■■■■secret technique■/escape■■■■■■■■■■■"});




            //Console.Clear();
            byte UsingNumber = 0, //Просто итератор
                HeightOfTheLastJoining = 0; //Содержит значение количества строк одного из String, из LinesOfDescription
            foreach (String[] s in LinesOfDescription)
            {
                //Console.SetCursorPosition(0, Console.CursorTop);
                if (UsingNumber > 3 && UsingNumber % 2 == 0 && s[0].Contains('\n'))
                {
                    //Console.SetCursorPosition(s[0].Substring(0, s[0].IndexOf('\n')).Length - 2, Console.CursorTop - HeightOfTheLastJoining);
                    for (int i = 0; i < HeightOfTheLastJoining; i++)
                    {
                        //Console.SetCursorPosition(s[0].Substring(0, s[0].IndexOf('\n')).Length - 2, Console.CursorTop + i);
                        if (UsingNumber == 4 && !(graph.ElementAt(5).getX()>0))
                        {
                            graph.ElementAt(5).setTwoConditions(s);
                            graph.ElementAt(5).setX((short)(s[0].Substring(0, s[0].IndexOf('\n')).Length - 2));
                            //graph.ElementAt(5).setY((short)(Console.CursorTop + i));
                        }
                        //Console.Write(s[0].Split('\n')[i]);
                    }
                    //Console.SetCursorPosition(0, Console.CursorTop + HeightOfTheLastJoining - 1);
                }
                //Одно из условий корректного построения окна боя
                else if (UsingNumber > 3 && UsingNumber % 2 == 0 && !s[0].Contains('\n'))
                {
                    //Console.SetCursorPosition(s[0].Length - 1, Console.CursorTop - 1);
                    //Console.Write(s[0]);
                    //Console.SetCursorPosition(0, Console.CursorTop + 1);
                }
                else
                {
                    //Одно из условий корректного построения окна боя
                    if (UsingNumber==0 || UsingNumber==2 || UsingNumber ==3 || UsingNumber==7)
                    {
                        if (UsingNumber == 3)
                        {
                            graph.ElementAt(UsingNumber+1).setTwoConditions(s);
                            graph.ElementAt(UsingNumber+1).setX(0);
                            //graph.ElementAt(UsingNumber+1).setY((short)Console.CursorTop);
                        }
                        else if (UsingNumber == 7)
                        {
                            graph.ElementAt(UsingNumber - 1).setTwoConditions(s);
                            graph.ElementAt(UsingNumber - 1).setX(0);
                           // graph.ElementAt(UsingNumber - 1).setY((short)Console.CursorTop);
                        }
                        else
                        {
                            graph.ElementAt(UsingNumber).setTwoConditions(s);
                            graph.ElementAt(UsingNumber).setX(0);
                            //graph.ElementAt(UsingNumber).setY((short)Console.CursorTop);
                        }
                    }
                    //Console.WriteLine(s[0]);
                }
                HeightOfTheLastJoining = Convert.ToByte(LinesOfDescription.ElementAt(UsingNumber)[0].Split('\n').Length);
                UsingNumber++;
            }
        }
        //Обновление статов существ в окне боя
        public static String[] UpdateStats(CreatureBuilder cb0)
        {
            String reBuiltHpView = "";
            for (int i = 0; i < cb0.getMaxHP().ToString().Length-cb0.getHP().ToString().Length; i++)
                reBuiltHpView += " ";
            reBuiltHpView += cb0.getHP().ToString();
            return new String[] {"HP:"+reBuiltHpView+"/"+cb0.getMaxHP()+ "     Armor:["+cb0.getARMOR()+"]"+"   ["+cb0.getType()+" stats "+cb0.getIcon()+"]",
                                                         "HP:"+reBuiltHpView+"/"+cb0.getMaxHP()+ "▬▬▬▬▬Armor:["+cb0.getARMOR()+"]"+ "▬▬▬["+cb0.getType()+"▬stats▬"+cb0.getIcon()+"]" };
        }
    }
}

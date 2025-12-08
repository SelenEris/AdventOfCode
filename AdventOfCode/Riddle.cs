using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode
{
    internal class Riddle
    {
        public int Day = 0;
        public int Number = 1;
        public string FileName = string.Empty;
        private string FilePath = string.Empty;
        public int Solution = 0;

        public Riddle(int day, int number, string path)
        {
            Day = day;
            Number = number;
            if (!string.IsNullOrEmpty(path))
            {
                FileName = path;
                FilePath = Constants.Folder + "\\" + path;
            }

            switch (Day)
            {
                case 0:
                    break;
                case 1:
                    if(Day == 1)CodeSafe1();
                    else CodeSafe2();
                    break;
            }
        }

        public void CodeSafe1()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                string line;
                int countSafe = 50;
                int count0 = 0;
                int countline = 0;

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine();


                while (line != null)
                {
                    countline++;
                    Console.WriteLine("Position : " + countSafe);
                    string direction = line.Substring(0, 1);
                    int numberTick = Convert.ToInt32(line.Substring(1));
                    Console.WriteLine("Ligne : " + line);

                    while (numberTick > 99)
                    {
                        Math.DivRem(numberTick, 100, out numberTick);
                    }

                    if (direction == "R")
                    {
                        if (countSafe + numberTick <= 99)
                        {
                            countSafe += numberTick;
                        }
                        else
                        {
                            countSafe = numberTick - (100 - countSafe);
                        }

                    }
                    else
                    {

                        if (numberTick < countSafe)
                        {
                            countSafe -= numberTick;
                        }
                        else
                        {
                            if(numberTick == countSafe) countSafe = 0;
                            else countSafe = 100 - (numberTick - countSafe);
                        }
                    }

                    if (countSafe == 0) count0++;

                    Console.WriteLine("Point final : " + countSafe);

                    line = sr.ReadLine();

                }
                sr.Close();
                Solution = count0;
                Console.WriteLine("Nombre de lignes : " + countline);
            }
            else
            {
                Console.WriteLine("The path to the file is not correct.");
            }
        }

        public void CodeSafe2()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                string line;
                int countSafe = 50;
                int count0 = 0;
                int countline = 0;

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine();


                while (line != null)
                {
                    countline++;
                    Console.WriteLine("Position : " + countSafe);
                    string direction = line.Substring(0, 1);
                    int numberTick = Convert.ToInt32(line.Substring(1));
                    Console.WriteLine("Ligne : " + line);

                    while (numberTick > 99)
                    {
                        count0 += (numberTick / 100);
                        Math.DivRem(numberTick, 100, out numberTick);
                    }

                    if (direction == "R")
                    {
                        if (countSafe + numberTick <= 99)
                        {
                            countSafe += numberTick;
                        }
                        else
                        {
                            countSafe = numberTick - (100 - countSafe);
                            count0++;
                        }

                    }
                    else
                    {

                        if (numberTick < countSafe)
                        {
                            countSafe -= numberTick;
                        }
                        else
                        {
                            if(countSafe !=0) count0++;
                            if (numberTick == countSafe) countSafe = 0;
                            else countSafe = 100 - (numberTick - countSafe); 
                            
                        }
                    }


                    Console.WriteLine("Nombre de 0 : " + count0);
                    Console.WriteLine("Point final : " + countSafe);

                    line = sr.ReadLine();

                }
                sr.Close();
                Solution = count0;
                Console.WriteLine("Nombre de lignes : " + countline);
            }
            else
            {
                Console.WriteLine("Le chemin spécifié n'edst pas correct.");
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Immutable;

namespace AdventOfCode
{
    internal class Riddle
    {
        public int Day = 0;
        public int Number = 1;
        public string FileName = string.Empty;
        private string FilePath = string.Empty;
        public long Solution = 0;

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
                    if(Number == 1)CodeSafe1();
                    else CodeSafe2();
                    break;
                case 2:
                    if (Number == 1) IDCheck1();
                    else IDCheck2();
                    break;
                case 3:
                    if (Number == 1) Joltage1();
                    else Joltage2();
                    break;

            }
        }
        #region Day 1
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
                    string direction = line.Substring(0, 1);
                    int numberTick = Convert.ToInt32(line.Substring(1));

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


                    line = sr.ReadLine();

                }
                sr.Close();
                Solution = count0;
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
                    string direction = line.Substring(0, 1);
                    int numberTick = Convert.ToInt32(line.Substring(1));

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
                    line = sr.ReadLine();

                }
                sr.Close();
                Solution = count0;
            }
            else
            {
                Console.WriteLine("Le chemin spécifié n'edst pas correct.");
            }
        }
        #endregion

        #region Day 2
        public void IDCheck1()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                string line;
                long countInvalidIDs = 0;

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine() ?? string.Empty;

                sr.Close();

                string [] idRanges = line.Split(',') ?? [];

                foreach(string idRange in idRanges)
                {
                    string idStart = idRange.Split('-')[0];
                    string idEnd = idRange.Split('-')[1];
                    long idEndInteger = long.Parse(idEnd);
                    long idIndexInteger = long.Parse(idStart);

                    while (idIndexInteger <= idEndInteger)
                    { 
                        string idIndexString = idIndexInteger.ToString();
                        string firstPart = idIndexString.Substring(0,idIndexString.Length / 2);
                        string lastPart = idIndexString.Substring(idIndexString.Length / 2);
                        if(firstPart == lastPart) countInvalidIDs+=idIndexInteger;
                        idIndexInteger++;
                    }
                }
                Solution = countInvalidIDs;
            }
            else
            {
                Console.WriteLine("The path to the file is not correct.");
            }
        }

        public void IDCheck2()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                string line;
                long countInvalidIDs = 0;

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine() ?? string.Empty;

                sr.Close();

                string[] idRanges = line.Split(',') ?? [];

                foreach (string idRange in idRanges)
                {
                    string idStart = idRange.Split('-')[0];
                    string idEnd = idRange.Split('-')[1];
                    long idEndInteger = long.Parse(idEnd);
                    long idIndexInteger = long.Parse(idStart);

                    while (idIndexInteger <= idEndInteger)
                    {
                        string idIndexString = idIndexInteger.ToString();
                        bool isEqual = Utils.RecursiveDivision(idIndexString, 2);
                        if (isEqual)
                        {
                            countInvalidIDs += idIndexInteger;
                        }
                        idIndexInteger++;
                    }
                }
                Solution = countInvalidIDs;
            }
            else
            {
                Console.WriteLine("The path to the file is not correct.");
            }
        }
        #endregion


        #region Day 3
        public void Joltage1()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                string line;
                long countTotalJoltage = 0;

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine() ?? string.Empty;

                while (line != null)
                {
                    List<char> listIndividualJoltage = line.ToList();

                    char firstHighNumber = '0';
                    int indexFirstHighNumber = 0;
                    char secondHighNumber = '0';
                    for (int i = 0; i < listIndividualJoltage.Count - 1; i++)
                    {
                        if (listIndividualJoltage[i] > firstHighNumber)
                        {
                            firstHighNumber = listIndividualJoltage[i];
                            indexFirstHighNumber = i;
                        }
                    }

                    for (int j = indexFirstHighNumber+1; j < listIndividualJoltage.Count; j++)
                    {
                        if (listIndividualJoltage[j] > secondHighNumber)
                        {
                            secondHighNumber = listIndividualJoltage[j];
                        }
                    }

                    string stringJoltage = firstHighNumber.ToString() + secondHighNumber.ToString();
                    countTotalJoltage += long.Parse(stringJoltage);

                    line = sr.ReadLine();
                }
                sr.Close();

                Solution = countTotalJoltage;
            }
            else
            {
                Console.WriteLine("The path to the file is not correct.");
            }
        }

        public void Joltage2()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                string line;
                long countTotalJoltage = 0;

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine() ?? string.Empty;

                while (line != null)
                {
                    List<char> listIndividualJoltage = line.ToList();

                    string stringJoltage = string.Empty;
                    int indexHighNumber = -1;
                    for (int i = 0; i < 12; i++)
                    {
                        char highNumber = '0';
                        int j = indexHighNumber + 1;
                        while(j < listIndividualJoltage.Count - (11 - i))
                        {
                            if (listIndividualJoltage[j] > highNumber)
                            {
                                highNumber = listIndividualJoltage[j];
                                indexHighNumber = j;
                            }
                            j++;
                        }
                        stringJoltage += highNumber.ToString();
                    }

                    countTotalJoltage += long.Parse(stringJoltage);

                    line = sr.ReadLine();
                }
                sr.Close();

                Solution = countTotalJoltage;
            }
            else
            {
                Console.WriteLine("The path to the file is not correct.");
            }
        }
        #endregion
    }

}

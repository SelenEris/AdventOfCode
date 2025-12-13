using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Immutable;
using System.Collections;

namespace AdventOfCode
{
    internal class Riddle
    {
        public int Day = 0;
        public int Number = 1;
        public string FileName = string.Empty;
        private string FilePath = string.Empty;
        public long Solution = 0;

        public Riddle(int day, int number, string version)
        {
            Day = day;
            Number = number;
            if (!string.IsNullOrEmpty(version))
            {
                FileName = Constants.NameFile + day.ToString() + "-" + version + Constants.TextExtention;
                FilePath = Constants.Folder + FileName;
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
                case 4:
                    if (Number == 1) RollsOfPaper1();
                    else RollsOfPaper2();
                    break;
                case 5:
                    if (Number == 1) FreshIngredient1();
                    else FreshIngredient2();
                    break;

            }
        }
        #region Day 1
        /// <summary>
        /// Calculation of the code of the safe, first part
        /// </summary>
        public void CodeSafe1()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                //Declaration of variables
                string line;
                int countSafe = 50;
                int count0 = 0;

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine();

                //Loop on the lines in the file
                while (line != null)
                {
                    //Construction of direction and number of ticks
                    string direction = line.Substring(0, 1);
                    int numberTick = Convert.ToInt32(line.Substring(1));

                    //Used to skip 360° 
                    while (numberTick > 99)
                    {
                        Math.DivRem(numberTick, 100, out numberTick);
                    }

                    //If the tick goes to the right (+)
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
                    //If the tick goes to the left (-)
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

                    //If the tick is on 0, incrementation of the result
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

        /// <summary>
        /// Calculation of the code of the safe, second part
        /// </summary>
        public void CodeSafe2()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                //Declaration of variables
                string line;
                int countSafe = 50;
                int count0 = 0;
                int countline = 0;

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine();

                // Loop on the lines in the file
                while (line != null)
                {
                    //Construction of direction and number of ticks
                    countline++;
                    string direction = line.Substring(0, 1);
                    int numberTick = Convert.ToInt32(line.Substring(1));

                    //This time, each 360° count as 1 "0"
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
                        // If the number of tick makes the count safe higher than 99, then it automatically pass by 0
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
                            // If the number of tick makes the count safe lesser than 0, then it automatically pass by 0
                            if (countSafe !=0) count0++;
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
                Console.WriteLine("The path to the file is not correct.");
            }
        }
        #endregion

        #region Day 2
        /// <summary>
        /// Checking of differents ID, first part
        /// </summary>
        public void IDCheck1()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                //Declaration of variables
                string line;
                long countInvalidIDs = 0;

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine() ?? string.Empty;

                sr.Close();

                //Split the input into different ranges of IDs
                string [] idRanges = line.Split(',') ?? [];

                //Loop on the ranges
                foreach(string idRange in idRanges)
                {
                    //Preperation of variables used for comparaison
                    string idStart = idRange.Split('-')[0];
                    string idEnd = idRange.Split('-')[1];
                    long idEndInteger = long.Parse(idEnd);
                    long idIndexInteger = long.Parse(idStart);

                    //Loop on all the IDs in the range
                    while (idIndexInteger <= idEndInteger)
                    {
                        //Comparaison between the two halfs of each ID
                        string idIndexString = idIndexInteger.ToString();
                        string firstPart = idIndexString.Substring(0,idIndexString.Length / 2);
                        string lastPart = idIndexString.Substring(idIndexString.Length / 2);
                        //If two parts are equal, the ID is fake
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

        /// <summary>
        /// Checking of differents ID, second part
        /// </summary>
        public void IDCheck2()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                //Declaration of variables
                string line;
                long countInvalidIDs = 0;

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine() ?? string.Empty;

                sr.Close();

                //Split the input into different ranges of IDs
                string[] idRanges = line.Split(',') ?? [];

                //Loop on the ranges
                foreach (string idRange in idRanges)
                {
                    //Preperation of variables used for comparaison
                    string idStart = idRange.Split('-')[0];
                    string idEnd = idRange.Split('-')[1];
                    long idEndInteger = long.Parse(idEnd);
                    long idIndexInteger = long.Parse(idStart);

                    //Loop on all the IDs in the range
                    while (idIndexInteger <= idEndInteger)
                    {
                        string idIndexString = idIndexInteger.ToString();

                        //Recursive method used to find the right number of split to make for the comparaison
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
        /// <summary>
        /// Calculation of the joltage needed to power the escalators, first part
        /// </summary>
        public void Joltage1()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                string line;
                long countTotalJoltage = 0;

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine() ?? string.Empty;

                //Loop on the lines in the file
                while (line != null)
                {
                    //Convertion from string to list of chars for easier manipulation
                    List<char> listIndividualJoltage = line.ToList();

                    //Declaration of variables
                    char firstHighNumber = '0';
                    char secondHighNumber = '0';
                    int indexFirstHighNumber = 0;

                    //To find the first digit, the search is done from the beginning to the end-1, to let 1 place for the second digit
                    for (int i = 0; i < listIndividualJoltage.Count - 1; i++)
                    {
                        if (listIndividualJoltage[i] > firstHighNumber)
                        {
                            firstHighNumber = listIndividualJoltage[i];
                            indexFirstHighNumber = i;
                        }
                    }

                    //To find the second digit, the search is done from the position following the first digit found, to the end
                    for (int j = indexFirstHighNumber+1; j < listIndividualJoltage.Count; j++)
                    {
                        if (listIndividualJoltage[j] > secondHighNumber)
                        {
                            secondHighNumber = listIndividualJoltage[j];
                        }
                    }

                    //Construction of the joltage with the higher digit found for the range given
                    string stringJoltage = firstHighNumber.ToString() + secondHighNumber.ToString();

                    //Calculation of the total joltage
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

        /// <summary>
        /// Calculation of the joltage needed to power the escalators, second part
        /// </summary>
        public void Joltage2()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                string line;
                long countTotalJoltage = 0;

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine() ?? string.Empty;

                //Loop on the lines in the file
                while (line != null)
                {
                    //Convertion from string to list of chars for easier manipulation
                    List<char> listIndividualJoltage = line.ToList();

                    //Variable used to create the final number
                    string stringJoltage = string.Empty;

                    //index of the last "high number" found. Start at -1 since no high number found
                    int indexHighNumber = -1;

                    //Loop 12 times, 1 time for each digit to find in the joltage
                    for (int i = 0; i < 12; i++)
                    {
                        //the high number to find for the i-th digit of the joltage, starts at 0 since it's the smallest
                        char highNumber = '0';

                        //Each time the search for the high number is done, it starts from the position following the last "high number" found to prevent catching the same "high number" several times
                        int j = indexHighNumber + 1;

                        //It is important to NOT go to the end of the list each time, since it has to be some digit "left" or else you might lack some digit in the voltage
                        //For exemple, in "811111111111119", if you search in all the list, you will have avec final joltage "900000000000" since 9 is higher than anything else and it's the last digit.
                        while (j < listIndividualJoltage.Count - (11 - i))
                        {
                            if (listIndividualJoltage[j] > highNumber)
                            {
                                highNumber = listIndividualJoltage[j];
                                indexHighNumber = j;
                            }
                            j++;
                        }
                        //Construction of the joltage with the higher digit found for the range given
                        stringJoltage += highNumber.ToString();
                    }

                    //Calculation of the total joltage
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

        #region Day 4
        /// <summary>
        /// Calculation of the joltage needed to power the escalators, first part
        /// </summary>
        public void RollsOfPaper1()
        {
            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                string line;

                StreamReader sr = new StreamReader(FilePath);


                List<List<char>> lines = new List<List<char>>();

                line = sr.ReadLine() ?? string.Empty;

                //Loop on the lines in the file to make a map : in each line, there is a list of rolls
                for (int i=0;  line != null; i++)
                {
                    List<char> listRolls = line.ToList();
                    lines.Add(listRolls);
                    line = sr.ReadLine();
                }
                    
                sr.Close();

                //Now that the map is done, the search begins

                int totalRolls = 0;

                //First loop on the lines
                for (int i=0; i < lines.Count ; i++)
                {
                    List<char> listRolls = lines[i];

                    //Second loop on the rolls
                    for (int j=0; j < listRolls.Count; j++)
                    {
                        char roll = listRolls[j];

                        int countCloseRolls = 0;

                        //If a roll is found, count the number of close rolls around
                        if (roll == '@')
                        {
                            //It's a square where the limits are 'i-1','i+1' and 'j-1', 'j+1', but making shure we don't go over the map
                           
                            for (int k = (i != 0) ? i - 1 : i; k < lines.Count && k < i + 2; k++)
                            {
                                for (int l = (j != 0) ? j - 1 : j; l < listRolls.Count && l < j + 2 ; l++)
                                {
                                    if (lines[k][l] == '@' && (k != i || l != j)) countCloseRolls++;
                                }
                            }

                            if(countCloseRolls < 4) totalRolls++;
                        }
                    }
                }

                Solution = totalRolls;
            }
            else
            {
                Console.WriteLine("The path to the file is not correct.");
            }
        }

        /// <summary>
        /// Calculation of the joltage needed to power the escalators, second part
        /// </summary>
        public void RollsOfPaper2()
        {
            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                string line;

                StreamReader sr = new StreamReader(FilePath);


                List<List<char>> lines = new List<List<char>>();

                line = sr.ReadLine() ?? string.Empty;

                //Loop on the lines in the file to make a map : in each line, there is a list of rolls
                for (int i = 0; line != null; i++)
                {
                    List<char> listRolls = line.ToList();
                    lines.Add(listRolls);
                    line = sr.ReadLine();
                }

                sr.Close();

                bool continueRecursive = true;
                long totalRolls = Utils.RecursiveDeletionRolls(lines, out continueRecursive);

                Solution = totalRolls;
            }
            else
            {
                Console.WriteLine("The path to the file is not correct.");
            }
        }
        #endregion

        #region Day5
        /// <summary>
        /// Calculation the fresh ingredients available
        /// </summary>
        public void FreshIngredient1()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                //Declaration of variables
                string line;
                int countFresh = 0;
                List<Tuple<long, long>> listRanges = new List<Tuple<long, long>>();

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine();

                //Loop on the firsts lines in the file to keep the ranges
                while (!string.IsNullOrEmpty(line))
                {
                    //Preperation of variables used for comparaison
                    long idStart = long.Parse(line.Split('-')[0]);
                    long idEnd = long.Parse(line.Split('-')[1]);

                    listRanges.Add(Tuple.Create(idStart, idEnd));
                    line = sr.ReadLine();
                }

                //Now, the ranges are made, time to compare IDs
                line = sr.ReadLine();

                //Loop on all the IDs 
                while (!string.IsNullOrEmpty(line))
                {
                    long id = long.Parse(line);

                    //loop to search for the right range, if it exists
                    foreach(Tuple<long, long> range in listRanges)
                    {
                        if (id >= range.Item1 && id <= range.Item2)
                        {
                            countFresh++;
                            break;
                        }

                    }

                    line = sr.ReadLine();
                }

                sr.Close();

                Solution = countFresh;
            }
            else
            {
                Console.WriteLine("The path to the file is not correct.");
            }
        }

        /// <summary>
        /// Calculation the fresh ingredients available
        /// </summary>
        public void FreshIngredient2()
        {

            if (!string.IsNullOrEmpty(FilePath) && File.Exists(FilePath))
            {
                //Declaration of variables
                string line;
                long countIdsOK = 0;
                List<List <long>> listRanges = new List<List<long>>();

                StreamReader sr = new StreamReader(FilePath);

                line = sr.ReadLine();
                int countLine = 0;
                //Loop on the firsts lines in the file to keep the ranges
                while (!string.IsNullOrEmpty(line))
                {
                    countLine ++;
                    //Preperation of variables used for comparaison
                    long idStart = long.Parse(line.Split('-')[0]);
                    long idEnd = long.Parse(line.Split('-')[1]);
                    bool hasRangeBeenDiscarded = false;
                    //loop to verify the overlapping ranges
                    for(int i = 0; i < listRanges.Count; i++)
                    {
                        //If the first part of the current range is in one of the other ranges
                        if (idStart >= listRanges[i][0] && idStart <= listRanges[i][1])
                        {
                            //If the second part of the current range is in one of the other ranges
                            if (idEnd >= listRanges[i][0] && idEnd <= listRanges[i][1])
                            {
                                //The current range is completely in an other range, it has to be discarded
                                hasRangeBeenDiscarded = true;
                                break;
                            }
                            else
                            {
                                //Only the last part is in an other range, so it's reduced from the start
                                idStart = listRanges[i][1] + 1;

                            }
                        }
                        //If the first part of the current range is NOT in one of the other ranges, the second part might be
                        else if (idEnd >= listRanges[i][0] && idEnd <= listRanges[i][1])
                        {
                            //Only the last part is in an other range, so it's reduced at the end
                            idEnd = listRanges[i][0] - 1;

                        }
                        //If the current range entirely overlap an other range
                        else if(idStart < listRanges[i][0] && idEnd > listRanges[i][1])
                        {
                            //the other range is discarded
                            listRanges.RemoveAt(i);
                        }
                    }
                    if (!hasRangeBeenDiscarded && idStart <= idEnd)
                    {
                        listRanges.Add(new List<long> { idStart, idEnd });
                        listRanges.Sort((x,y) => x[0].CompareTo(y[0]));
                    }

                    line = sr.ReadLine();
                }

                for (int i = 0; i<listRanges.Count; i++)
                {

                    countIdsOK += (listRanges[i][1] - listRanges[i][0]) + 1;
                }
                sr.Close();
                Solution = countIdsOK;
            }
            else
            {
                Console.WriteLine("The path to the file is not correct.");
            }
        }
        #endregion
    }

}

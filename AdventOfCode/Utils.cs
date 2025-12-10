using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal static class Utils
    {
        /// <summary>
        /// Methode used to analyse a ID by splitting it the right number of times
        /// </summary>
        /// <param name="id"> ID to analyse</param>
        /// <param name="numberParts"> Number of part to cut the ID</param>
        /// <returns>Returns a bool at true if a equality</returns>
        public static bool RecursiveDivision(string id, int numberParts) 
        {
            //Declaration of variables
            bool isEqual = false;
            string[] listParts = new string[numberParts];

            //Filling of the list of part by splitting the ID to analyse in the number of part asked
            for (int i = 0; i < numberParts; i++) {
                if(i != numberParts-1) listParts[i] = id.Substring((id.Length / numberParts)*i, id.Length/numberParts); 
                else listParts[i] = id.Substring((id.Length / numberParts)*i);
            }

            //If there is only one distinct number in the list, it means all the part of the list are equal
            if (listParts.Distinct<string>().Count() == 1)
            { 
                isEqual = true;
            }
            //If the numbers are not equal
            else
            {
                //and there is still place left, it starts over with a higher number of part
                if(numberParts < id.Length) isEqual = RecursiveDivision(id, numberParts+1); 
            }
            
            return isEqual;
        }

        /// <summary>
        /// Methode used to analyse a ID by splitting it the right number of times
        /// </summary>
        /// <param name="id"> ID to analyse</param>
        /// <param name="numberParts"> Number of part to cut the ID</param>
        /// <returns>Returns a bool at true if a equality</returns>
        public static long RecursiveDeletionRolls(List<List<char>> lines, out bool topContinue)
        {
            List<Tuple<int, int>> rollsToRemove = new List<Tuple<int, int>>();

            long totalRolls = 0;
            topContinue = true;

            //First loop on the lines
            for (int i = 0; i < lines.Count; i++)
            {
                List<char> listRolls = lines[i];

                //Second loop on the rolls
                for (int j = 0; j < listRolls.Count; j++)
                {
                    char roll = listRolls[j];

                    int countCloseRolls = 0;

                    //If a roll is found, count the number of close rolls around
                    if (roll == '@')
                    {
                        //It's a square where the limits are 'i-1','i+1' and 'j-1', 'j+1', but making shure we don't go over the map

                        for (int k = (i != 0) ? i - 1 : i; k < lines.Count && k < i + 2; k++)
                        {
                            for (int l = (j != 0) ? j - 1 : j; l < listRolls.Count && l < j + 2; l++)
                            {
                                if (lines[k][l] == '@' && (k != i || l != j)) countCloseRolls++;
                            }
                        }

                        if (countCloseRolls < 4)
                        {
                            rollsToRemove.Add(Tuple.Create(i,j));

                        }
                    }
                }
            }
            totalRolls = rollsToRemove.Count;

            //Suppression of the rolls
            for (int i = 0; i < lines.Count; i++)
            {
                List<char> listRolls = lines[i];

                
                for (int j = 0; j < listRolls.Count; j++)
                {
                    char roll = listRolls[j];

                    int countCloseRolls = 0;

                    //We retrieve the counted roll and erase it to make a new map
                    if (roll == '@')
                    {
                        Tuple<int, int> coordinate = Tuple.Create(i, j);

                        if (rollsToRemove.Contains(coordinate)) lines[i][j] = '.';
                    }
                }
            }

            if (rollsToRemove.Count == 0) topContinue = false;

            //Recursive loop until there is no more rolls to delete
            while (topContinue)
            {
                totalRolls += RecursiveDeletionRolls(lines, out topContinue);
            };

            return totalRolls;
        }

    }
}

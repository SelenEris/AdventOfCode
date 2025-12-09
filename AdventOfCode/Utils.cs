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

    }
}

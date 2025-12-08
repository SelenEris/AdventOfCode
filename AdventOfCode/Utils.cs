using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    internal static class Utils
    {
        public static bool RecursiveDivision(string id, int numberParts) 
        {
            bool isEqual = false;
            string[] listParts = new string[numberParts];


            for (int i = 0; i < numberParts; i++) {
                if(i != numberParts-1) listParts[i] = id.Substring((id.Length / numberParts)*i, id.Length/numberParts); 
                else listParts[i] = id.Substring((id.Length / numberParts)*i);
            }

            if (listParts.Distinct<string>().Count() == 1)
            { 
                isEqual = true;
            }
            else
            {
                if(numberParts < id.Length) isEqual = RecursiveDivision(id, numberParts+1); 
            }
            
            return isEqual;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreeLineAssessmentQuestionOne
{
    public static class Extension
    {
        public static bool Includes(this IEnumerable<int> element, int item)
        {
            foreach(int currentItem in element)
            {
                if (currentItem == item)
                    return true;
            }

            return false;
        }
    }
    public class Assessment
    {
        public static int SmallestPositiveInteger(int[] input)
        {
            // [1, 100,000]
            // [-1, -100,000]

            // edge cases
            //check if the input is empty
            if (input.Length == 0)
                return 0;
            // get length of input
            int _len = input.Length;
            int counter = input[0];
            //int smallest = 0;
            Array.Sort(input);
            for(int i=0;i<_len;i++)
            {
                var current = input[i];

                if (current < -1000000 || current > 1000000)
                    throw new Exception("integer out of scope");

                counter = counter + 1;

                bool isIncluded = input.Includes(counter);
                if(!isIncluded && counter > 0)
                {
                    return counter;
                }
            }

            return 0;

        }


        
    }
}


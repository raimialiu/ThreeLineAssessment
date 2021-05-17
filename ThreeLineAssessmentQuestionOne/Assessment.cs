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
            foreach (int currentItem in element)
            {
                if (currentItem == item)
                    return true;
            }

            return false;
        }
    }

    public class Assessment
    {
        public static int[] DistinctElement(int[] input)
        {
            List<int> distincts = new List<int>();

            List<int> allNegavtive = new List<int>();
            if (input.Length > 100000)
                throw new Exception("too much elements");

            foreach (int s in input)
            {
                if (s < -1000000 || s > 1000000)
                    throw new Exception("integer out of scope");
                if (!distincts.Contains(s) && s > 0)
                {
                    distincts.Add(s);
                }

                if (s < 0)
                {
                    allNegavtive.Add(s);
                }
            }

            if (allNegavtive.Count == input.Length)
            {
                return allNegavtive.ToArray();
            }
            return distincts.ToArray();
        }
        public static int SmallestPositiveInteger(int[] input)
        {
            // [1, 100,000]
            // [-1, -100,000]

            if (input == null)
                throw new Exception("null input");
            // edge cases
            //check if the input is empty
            if (input.Length == 0)
                return 0;


            var distincts = DistinctElement(input);
            // get length of input
            int _len = distincts.Length;
            int counter = distincts[0];
            
            //input.Distinct()

            //[1,2,3]
            //int smallest = 0;
            Array.Sort(distincts);
            
            for (int i = 0; i < _len; i++)
            {
                var current = input[i];

               

                if (distincts[0] > 1 || distincts[0] < 0)
                {
                    return 1;
                }


                counter = counter + 1;


                bool isIncluded = input.Includes(counter);

                if (!isIncluded && counter > 0)
                {
                    return counter;
                }
            }


            return 0;
        }
    }
}
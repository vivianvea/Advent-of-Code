﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC.Y2018.Day07
{
    /*
     * https://adventofcode.com/2018/day/7
     */

    public static class Part01
    {
        public static void Exec(AOCContext context)
        {
            string[] input = context.GetInputLines();
            List<KeyValuePair<char, char>> steps = ParseInput(input);
            string result = CalculateResult(steps);

            AOCUtils.PrintResult("The answer is", result);
        }

        /// <summary>
        /// Puts all steps in a list of keyvaluepairs.
        /// </summary>
        private static List<KeyValuePair<char, char>> ParseInput(string[] input)
        {
            List<KeyValuePair<char, char>> steps = new List<KeyValuePair<char, char>>();

            foreach (string line in input)
            {
                char key = line[5];
                char value = line[36];

                steps.Add(new KeyValuePair<char, char>(key, value));
            }

            return steps;
        }

        private static string CalculateResult(List<KeyValuePair<char, char>> steps)
        {
            StringBuilder result = new StringBuilder();
            List<char> stepsList = new List<char>();

            // Finds all steps.
            foreach (KeyValuePair<char, char> pair in steps)
            {
                if (!stepsList.Contains(pair.Key))
                    stepsList.Add(pair.Key);

                if (!stepsList.Contains(pair.Value))
                    stepsList.Add(pair.Value);
            }
            stepsList.Sort();

            // Completes steps that don't depend on other steps, until all steps are completed.
            do
            {
                for (int i = 0; i < stepsList.Count; i++)
                {
                    char key = stepsList[i];
                    foreach (KeyValuePair<char, char> pair in steps)
                    {
                        char value = pair.Value;
                        if (key == value)
                            goto nextKey;
                    }

                    // Execute qualified steps in alphabetical order.
                    result.Append(key);
                    steps.RemoveAll(p => p.Key == key);
                    stepsList.Remove(key);
                    break;

                nextKey:;
                }
            }
            while (stepsList.Any());

            return result.ToString();
        }
    }
}
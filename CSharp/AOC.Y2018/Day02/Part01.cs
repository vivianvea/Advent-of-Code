﻿using System.Collections.Generic;
using System.Linq;

namespace AOC.Y2018.Day02
{
    /*
     * To make sure you didn't miss any, you scan the likely candidate boxes again, counting the number that
     * have an ID containing exactly two of any letter and then separately counting those with exactly three
     * of any letter. You can multiply those two counts together to get a rudimentary checksum and compare it to
     * what your device predicts.
     *
     * What is the checksum for your list of box IDs?
     *
     * https://adventofcode.com/2018/day/2
     */

    public static class Part01
    {
        public static void Exec(AOCContext context)
        {
            string[] input = context.GetInputLines();
            int checksum = CalculateChecksum(input);

            AOCUtils.PrintResult("Your checksum is", checksum);
        }

        /// <summary>
        /// Calculates the checksum of the input.
        /// </summary>
        private static int CalculateChecksum(string[] input)
        {
            int twoLettersTotal = 0;
            int threeLettersTotal = 0;

            foreach (string line in input)
            {
                CheckContent(line, out bool twoLetters, out bool threeLetters);

                if (twoLetters)
                    twoLettersTotal++;

                if (threeLetters)
                    threeLettersTotal++;
            }

            return twoLettersTotal * threeLettersTotal;
        }

        /// <summary>
        /// Checks if a line of input contains any letters that appear only twice and/or thrice.
        /// </summary>
        private static void CheckContent(string line, out bool containsTwoLetters, out bool containsThreeLetters)
        {
            containsTwoLetters = false;
            containsThreeLetters = false;

            List<char> lineList = new List<char>(line.ToCharArray());
            while (lineList.Any())
            {
                char first = lineList.First();
                int count = lineList.Where(c => c == first).Count();
                if (count == 2)
                    containsTwoLetters = true;

                if (count == 3)
                    containsThreeLetters = true;

                lineList.RemoveAll(c => c == first);
            }
        }
    }
}
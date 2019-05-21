﻿using AOC.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_7b
{
    /*
    * https://adventofcode.com/2018/day/7
    */

    internal class Program
    {
        private const int workers = 5;

        private static void Main(string[] args)
        {
            int result = Task.Run(async () =>
            {
                string[] input = await General.GetInputFromPath(@"..\..\..\input\day7.txt");
                //string[] input = General.GetInput();
                List<KeyValuePair<char, char>> steps = ParseInput(input);
                return CalculateResult(steps);
            }).Result;

            Console.WriteLine("Total work time is: {0}", result);
            Console.ReadKey();
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

        private static int CalculateResult(List<KeyValuePair<char, char>> steps)
        {
            int result = 0;
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

            // Sets up "workers".
            char[] workTask = new char[workers];
            int[] workLeft = new int[workers];
            Queue<int> freeWorkers = new Queue<int>();
            for (int i = 0; i < workLeft.Length; i++)
                freeWorkers.Enqueue(i);

            // Completes steps that don't depend on other steps, until all steps are completed.
            //Console.WriteLine("Second | Worker 1 | Worker 2");
            do
            {
                // Asign tasks
                for (int i = 0; i < stepsList.Count; i++)
                {
                    if (!freeWorkers.Any())
                        break;

                    char key = stepsList[i];
                    foreach (KeyValuePair<char, char> pair in steps)
                    {
                        char value = pair.Value;
                        if (key == value)
                            goto nextKey;
                    }

                    // Give task to a worker
                    int workerIndex = freeWorkers.Dequeue();
                    workTask[workerIndex] = key;
                    workLeft[workerIndex] = key - 4;
                    stepsList.Remove(key);

                nextKey:;
                }

                //Console.WriteLine("{0} | {1} | {2}", result, workTask[0], workTask[1]);

                // Complete tasks
                for (int i = 0; i < workLeft.Length; i++)
                {
                    if (workLeft[i] != 0)
                    {
                        workLeft[i]--;
                        if (workLeft[i] == 0)
                        {
                            char key = workTask[i];
                            workTask[i] = '-';
                            steps.RemoveAll(p => p.Key == key);
                            freeWorkers.Enqueue(i);
                        }
                    }
                }

                result++;
            }
            while (workLeft.Any(t => t != 0) || stepsList.Any());

            return result;
        }
    }
}
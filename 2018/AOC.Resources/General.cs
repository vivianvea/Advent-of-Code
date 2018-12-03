﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Resources
{
    public class General
    {
        /// <summary>
        /// Gets input from console.
        /// <para>Escapes on /x.</para>
        /// </summary>
        public static string[] GetInput()
        {
            Console.WriteLine("Please enter your input:");

            List<string> input = new List<string>();
            string line = string.Empty;

            while (true)
            {
                line = Console.ReadLine();
                if (line == "/x")
                    break;

                input.Add(line);
            }

            return input.ToArray();
        }
    }
}
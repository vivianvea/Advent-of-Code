﻿using System;

namespace AOC.Y2019.Day17.P01
{
    public sealed class IntcodeEvaluationException : Exception
    {
        public IntcodeEvaluationException(string message) : base(message)
        {
        }
    }
}
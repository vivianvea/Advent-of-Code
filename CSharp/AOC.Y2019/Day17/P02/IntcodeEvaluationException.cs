﻿using System;

namespace AOC.Y2019.Day17.P02
{
    public sealed class IntcodeEvaluationException : Exception
    {
        public IntcodeEvaluationException(string message) : base(message)
        {
        }
    }
}
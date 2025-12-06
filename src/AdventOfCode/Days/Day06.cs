using System;
using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Days
{
    public struct Problem
    {
        public List<long> Operands;
        public string Operator;
    }

    public class Day06 : AdventOfCode.IDay
    {
        public string Part1(string[] input)
        {
            var lines = new List<string[]>();
            foreach (var line in input)
            {
                var parts = line.Split(new []{" "}, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                lines.Add(parts);
            }
            var total = (long)0;
            for (int i = 0; i < lines[0].Length; i++)
            {
                var op = lines[lines.Count - 1][i];
                var operands = new List<long>();
                for (int j = 0; j < lines.Count - 1; j++)
                {
                    operands.Add(long.Parse(lines[j][i]));
                }
                if (op == "+")
                {
                    foreach (var val in operands)
                    {
                        total += val;
                    }
                }
                else if (op == "*")
                {
                    var subtotal = (long)1;
                    foreach (var val in operands)
                    {
                        subtotal *= val;
                    }
                    total += subtotal;
                }
                else
                {
                    throw new Exception($"Unknown operator: {op}");
                }
            }
            return total.ToString();
        }

        public string Part2(string[] input)
        {
            var problems = new List<Problem>();
            var problem = new Problem
            {
                Operands = new List<long>(),
                Operator = string.Empty
            };
            for (int i = 0; i < input[0].Length; i++)
            {
                var foundOperand = false;
                var currentOperand = 0;
                for (int j = 0; j < input.Length; j++)
                {
                    if (char.IsDigit(input[j][i]))
                    {
                        foundOperand = true;
                        currentOperand = currentOperand * 10 + (input[j][i] - '0');
                    }
                    if (j == input.Length - 1 && input[j][i] == '+' || input[j][i] == '*')
                    {
                        problem.Operator = input[j][i].ToString();
                    }
                }
                if (foundOperand)
                {
                    problem.Operands.Add(currentOperand);
                }
                if (!foundOperand || i == input[0].Length - 1)
                {
                    problems.Add(problem);
                    problem = new Problem
                    {
                        Operands = new List<long>(),
                        Operator = string.Empty
                    };
                }
            }
            var total = (long)0;
            foreach (var currentProblem in problems)
            {
                if (currentProblem.Operator == "+")
                {
                    foreach (var val in currentProblem.Operands)
                    {
                        total += val;
                    }
                }
                else if (currentProblem.Operator == "*")
                {
                    var subtotal = (long)1;
                    foreach (var val in currentProblem.Operands)
                    {
                        subtotal *= val;
                    }
                    total += subtotal;
                }
                else
                {
                    throw new Exception($"Unknown operator: {currentProblem.Operator}");
                }
            }
            return total.ToString();
        }
    }
}

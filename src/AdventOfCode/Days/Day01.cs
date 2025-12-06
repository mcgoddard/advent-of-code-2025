using System;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day01 : AdventOfCode.IDay
    {
        public string Part1(string[] input)
        {
            // Example implementation: sum of integers in input
            var nums = input.Where(s => !string.IsNullOrWhiteSpace(s))
                            .Select(s => int.TryParse(s, out var v) ? v : 0);
            return nums.Sum().ToString();
        }

        public string Part2(string[] input)
        {
            // Example implementation: count non-empty lines
            return input.Count(s => !string.IsNullOrWhiteSpace(s)).ToString();
        }
    }
}

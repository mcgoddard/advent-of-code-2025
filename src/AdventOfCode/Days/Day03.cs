using System;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day03 : AdventOfCode.IDay
    {
        public string Part1(string[] input)
        {
            var maxJoltages = new List<int>();
            foreach (var line in input)
            {
                var maxJolt = 0;
                var maxJoltIndex = -1;
                for (int i = line.Length - 2; i >= 0; i--)
                {
                    if (int.Parse(line[i].ToString()) >= maxJolt)
                    {
                        maxJolt = int.Parse(line[i].ToString());
                        maxJoltIndex = i;
                    }
                }
                var maxSecondJolt = 0;
                var maxSecondJoltIndex = -1;
                for (int i = line.Length - 1; i > maxJoltIndex; i--)
                {
                    if (int.Parse(line[i].ToString()) >= maxSecondJolt)
                    {
                        maxSecondJolt = int.Parse(line[i].ToString());
                        maxSecondJoltIndex = i;
                    }
                }
                var total = int.Parse($"{maxJolt}{maxSecondJolt}");
                maxJoltages.Add(total);
            }
            return maxJoltages.Sum().ToString();
        }

        public string Part2(string[] input)
        {
            var maxJoltages = new List<long>();
            foreach (var line in input)
            {
                var jolts = new List<int>();
                var previousMaxIndex = -1;
                for (int i = 0; i < 12; i++)
                {
                    var (maxDigit, maxIndex) = FindLargestDigitAndIndex(line, previousMaxIndex + 1, line.Length - 12 + i);
                    jolts.Add(maxDigit);
                    previousMaxIndex = maxIndex;
                }
                var total = long.Parse(String.Join("", jolts.Select(x => x.ToString()).ToArray()));
                maxJoltages.Add(total);
            }
            return maxJoltages.Sum().ToString();
        }

        private static (int, int) FindLargestDigitAndIndex(string line, int start, int end)
        {
            int maxDigit = 0;
            int maxIndex = -1;
            for (int i = end; i >= start; i--)
            {
                int digit = int.Parse(line[i].ToString());
                if (digit >= maxDigit)
                {
                    maxDigit = digit;
                    maxIndex = i;
                }
            }
            return (maxDigit, maxIndex);
        }
    }
}

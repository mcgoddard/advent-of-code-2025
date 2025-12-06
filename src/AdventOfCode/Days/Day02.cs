using System;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day02 : AdventOfCode.IDay
    {
        public string Part1(string[] input)
        {
            var ranges = input[0].Split(',')
                              .Select(s => s.Split('-'))
                              .Select(parts => (Min: long.Parse(parts[0]), Max: long.Parse(parts[1])));
            var invalidIds = new List<long>();
            foreach (var range in ranges)
            {
                for (long i = range.Min; i <= range.Max; i++)
                {
                    if (i.ToString().Length % 2 != 0)
                    {
                        continue;
                    }
                    var start = long.Parse(i.ToString().Substring(0, i.ToString().Length / 2));
                    var end = long.Parse(i.ToString().Substring(i.ToString().Length / 2));
                    if (end == start) 
                    {
                        invalidIds.Add(i);
                    }
                }
            }
            return invalidIds.Sum().ToString();
        }

        public string Part2(string[] input)
        {
            var ranges = input[0].Split(',')
                              .Select(s => s.Split('-'))
                              .Select(parts => (Min: long.Parse(parts[0]), Max: long.Parse(parts[1])));
            var invalidIds = new HashSet<long>();
            foreach (var range in ranges)
            {
                for (long i = range.Min; i <= range.Max; i++)
                {
                    for (int j = 1; j <= i.ToString().Length / 2; j++)
                    {
                        if (invalidIds.Contains(i))
                        {
                            break;
                        }
                        if (i.ToString().Substring(j).StartsWith("0"))
                        {
                            // Leading zeros are not allowed
                            continue;
                        }
                        var start = long.Parse(i.ToString().Substring(0, j));
                        var end = long.Parse(i.ToString().Substring(j));
                        if (IsRepeating(start.ToString(), end.ToString()))
                        {
                            invalidIds.Add(i);
                        }
                    }
                }
            }
            return invalidIds.Sum().ToString();
        }

        private static bool IsRepeating(string s, string remaining) 
        {
            if (string.IsNullOrEmpty(remaining))
            {
                return true;
            }

            if (remaining.StartsWith(s))
            {
                return IsRepeating(s, remaining.Substring(s.Length));
            }

            return false;
        }
    }
}

using System;
using System.Linq;

namespace AdventOfCode.Days
{
    public struct Interval 
    {
        public long Start { get; set; }
        public long End { get; set; }
    }

    public class Day05 : AdventOfCode.IDay
    {
        public string Part1(string[] input)
        {
            // Read intervals from input until we hit an empty line
            var readingIntervals = true;
            var intervals = new List<Interval>();
            var ingredients = new List<long>();
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    readingIntervals = false;
                    continue;
                }
                if (readingIntervals)
                {
                    var parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2 ||
                        !long.TryParse(parts[0], out long start) ||
                        !long.TryParse(parts[1], out long end))
                    {
                        throw new ArgumentException($"Invalid interval line: {line}");
                    }
                    intervals.Add(new Interval { Start = start, End = end });
                } else {
                    if (long.TryParse(line, out long ingredient))
                    {
                        ingredients.Add(ingredient);
                    } else {
                        throw new ArgumentException($"Invalid ingredient line: {line}");
                    }
                }
            }
            // Sort and merge intervals
            var mergedIntervals = intervals
                .OrderBy(i => i.Start)
                .Aggregate(new List<Interval>(), (acc, curr) =>
                {
                    if (acc.Count == 0 || acc.Last().End < curr.Start)
                    {
                        acc.Add(curr);
                    }
                    else
                    {
                        acc[acc.Count - 1] = new Interval
                        {
                            Start = acc.Last().Start,
                            End = Math.Max(acc.Last().End, curr.End)
                        };
                    }
                    return acc;
                });
            // Check each ingredient against merged intervals
            var freshIngredients = ingredients.Where(ingredient => mergedIntervals.Any(interval =>
                ingredient >= interval.Start && ingredient <= interval.End));
            return freshIngredients.Count().ToString();
        }

        public string Part2(string[] input)
        {
            // Read intervals from input until we hit an empty line
            var intervals = new List<Interval>();
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    break;
                }
                var parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2 ||
                    !long.TryParse(parts[0], out long start) ||
                    !long.TryParse(parts[1], out long end))
                {
                    throw new ArgumentException($"Invalid interval line: {line}");
                }
                intervals.Add(new Interval { Start = start, End = end });
            }
            // Sort and merge intervals
            var mergedIntervals = intervals
                .OrderBy(i => i.Start)
                .Aggregate(new List<Interval>(), (acc, curr) =>
                {
                    if (acc.Count == 0 || acc.Last().End < curr.Start)
                    {
                        acc.Add(curr);
                    }
                    else
                    {
                        acc[acc.Count - 1] = new Interval
                        {
                            Start = acc.Last().Start,
                            End = Math.Max(acc.Last().End, curr.End)
                        };
                    }
                    return acc;
                });
            // Count merged ranges
            var totalCovered = mergedIntervals.Sum(interval => interval.End - interval.Start + 1);
            return totalCovered.ToString();
        }
    }
}

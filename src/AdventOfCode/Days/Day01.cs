using System;
using System.Linq;

namespace AdventOfCode.Days
{
    public struct Command
    {
        public char Direction { get; set; }
        public int Distance { get; set; }

        public static Command Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line) || line.Length < 2)
            {
                throw new ArgumentException("Invalid command line", nameof(line));
            }

            char direction = line[0];
            if (direction != 'L' && direction != 'R')
            {
                throw new ArgumentException("Direction must be 'L' or 'R'", nameof(line));
            }

            if (!int.TryParse(line.Substring(1), out int distance))
            {
                throw new ArgumentException("Invalid distance in command", nameof(line));
            }

            return new Command { Direction = direction, Distance = distance };
        }
    }

    public class Day01 : AdventOfCode.IDay
    {
        public string Part1(string[] input)
        {
            var commands = input.Where(s => !string.IsNullOrWhiteSpace(s))
                                .Select(s => Command.Parse(s));
            var zero_count = 0;
            var current = 50;
            foreach (var command in commands)
            {
                if (command.Direction == 'L')
                {
                    current -= command.Distance;
                }
                else if (command.Direction == 'R')
                {
                    current += command.Distance;
                }

                current %= 100;
                if (current < 0)
                {
                    current += 100;
                }

                if (current == 0)
                {
                    zero_count++;
                }
            }
            return zero_count.ToString();
        }

        public string Part2(string[] input)
        {
            var commands = input.Where(s => !string.IsNullOrWhiteSpace(s))
                                .Select(s => Command.Parse(s));
            var zero_count = 0;
            var current = 50;
            foreach (var command in commands)
            {
                var previous = current;
                if (command.Direction == 'L')
                {
                    current -= command.Distance;
                }
                else if (command.Direction == 'R')
                {
                    current += command.Distance;
                }

                var zeros = Math.Abs(current / 100);
                current %= 100;
                if (current < 0)
                {
                    if (previous != 0)
                    {
                        zeros++;
                    }
                    current += 100;
                }
                if (current == 0 && command.Direction == 'L')
                {
                    zeros++;
                }
                zero_count += zeros;
            }
            return zero_count.ToString();
        }
    }
}

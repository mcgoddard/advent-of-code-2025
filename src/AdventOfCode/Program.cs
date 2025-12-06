using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AdventOfCode
{
    class Program
    {
        static int Main(string[] args)
        {
            if (!TryParseArgs(args, out int day, out int part, out bool example, out string? inputPath))
            {
                PrintUsage();
                return 1;
            }

            string filename;
            if (!string.IsNullOrEmpty(inputPath))
            {
                filename = inputPath!;
            }
            else
            {
                var cwd = Directory.GetCurrentDirectory();
                var inputsDir = Path.Combine(cwd, "src", "AdventOfCode", "Inputs");
                filename = Path.Combine(inputsDir, $"day{day:D2}_{(example ? "example" : "input")}.txt");
            }

            string[] lines = Array.Empty<string>();
            if (File.Exists(filename))
            {
                lines = File.ReadAllLines(filename);
            }
            else
            {
                Console.WriteLine($"Warning: input file not found: {filename}");
            }

            var typeName = $"AdventOfCode.Days.Day{day:D2}";
            var dayType = Assembly.GetExecutingAssembly().GetType(typeName);
            if (dayType == null)
            {
                Console.WriteLine($"Day {day} not implemented (looked for {typeName}).");
                return 2;
            }

            if (!(Activator.CreateInstance(dayType) is IDay dayInstance))
            {
                Console.WriteLine($"Day {day} does not implement IDay.");
                return 3;
            }

            try
            {
                object result = part == 1 ? dayInstance.Part1(lines) : dayInstance.Part2(lines);
                Console.WriteLine($"Day {day} Part {part} result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running Day {day} Part {part}: {ex}");
                return 4;
            }

            return 0;
        }

        static bool TryParseArgs(string[] args, out int day, out int part, out bool example, out string? inputPath)
        {
            day = 0; part = 1; example = false; inputPath = null;
            for (int i = 0; i < args.Length; i++)
            {
                var a = args[i];
                if (a == "-d" || a == "--day")
                {
                    if (i + 1 >= args.Length) return false;
                    if (!int.TryParse(args[++i], out day)) return false;
                }
                else if (a == "-p" || a == "--part")
                {
                    if (i + 1 >= args.Length) return false;
                    if (!int.TryParse(args[++i], out part)) return false;
                }
                else if (a == "-e" || a == "--example")
                {
                    example = true;
                }
                else if (a == "--input")
                {
                    if (i + 1 >= args.Length) return false;
                    inputPath = args[++i];
                }
                else if (a == "-h" || a == "--help")
                {
                    return false;
                }
                else
                {
                    if (int.TryParse(a, out int v))
                    {
                        day = v;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (day < 1 || day > 25) return false;
            if (part != 1 && part != 2) return false;
            return true;
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: dotnet run --project src/AdventOfCode -- -d <day> -p <part> [-e] [--input <file>]");
            Console.WriteLine("  -d|--day <1-25>     Day number");
            Console.WriteLine("  -p|--part <1|2>     Part number");
            Console.WriteLine("  -e|--example        Use example input (default uses full input)");
            Console.WriteLine("  --input <file>      Use a specific input file");
            Console.WriteLine("Examples:");
            Console.WriteLine("  dotnet run --project src/AdventOfCode -- -d 1 -p 1 -e");
            Console.WriteLine("  dotnet run --project src/AdventOfCode -- -d 2 -p 2 --input Inputs/day02_input.txt");
        }
    }
}

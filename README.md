# advent-of-code-2025

## C# runner

This repository includes a small C# console runner for Advent of Code 2025 puzzles.

- Project: `src/AdventOfCode`

Usage examples (from repository root):

```bash
# Run day 1 part 1 using example input
# Note the `--` separator: args after `--` are passed to the app
dotnet run --project src/AdventOfCode -- -d 1 -p 1 -e

# Run day 1 part 2 using full input file
dotnet run --project src/AdventOfCode -- -d 1 -p 2

# Run with a custom input file
dotnet run --project src/AdventOfCode -- -d 1 -p 1 --input src/AdventOfCode/Inputs/day01_input.txt
```

Structure:

- `src/AdventOfCode/AdventOfCode.csproj` - project file
- `src/AdventOfCode/Program.cs` - CLI entrypoint and loader
- `src/AdventOfCode/IDay.cs` - interface for each day
- `src/AdventOfCode/Days/DayXX.cs` - implement day classes (example `Day01.cs`)
- `src/AdventOfCode/Inputs` - place `day01_example.txt` and `day01_input.txt` here

How it works:

- The runner accepts `-d|--day`, `-p|--part`, `-e|--example` and `--input` flags.
- It loads `AdventOfCode.Days.Day{DD}` via reflection and calls `Part1` or `Part2`.
- Implement new days by adding classes under `src/AdventOfCode/Days` that implement `IDay`.

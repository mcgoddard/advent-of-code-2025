using System;
using System.Linq;

namespace AdventOfCode.Days
{
    public enum CellType
    {
        Empty,
        Roll
    }

    public class Day04 : AdventOfCode.IDay
    {
        public string Part1(string[] input)
        {
            var positions = new CellType[input.Length, input[0].Length];
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    positions[i, j] = input[i][j] == '@' ? CellType.Roll : CellType.Empty;
                }
            }
            var accessibleRollCount = 0;
            for (int i = 0; i < positions.GetLength(0); i++)
            {
                for (int j = 0; j < positions.GetLength(1); j++)
                {
                    if (positions[i, j] == CellType.Roll)
                    {
                        var adjacentRolls = 0;
                        for (int di = -1; di <= 1; di++)
                        {
                            for (int dj = -1; dj <= 1; dj++)
                            {
                                if (di == 0 && dj == 0)
                                {
                                    continue;
                                }
                                int ni = i + di;
                                int nj = j + dj;
                                if (ni >= 0 && ni < positions.GetLength(0) &&
                                    nj >= 0 && nj < positions.GetLength(1) &&
                                    positions[ni, nj] == CellType.Roll)
                                {
                                    adjacentRolls++;
                                }
                            }
                        }
                        if (adjacentRolls < 4)
                        {
                            accessibleRollCount++;
                        }
                    }
                }
            }
            return accessibleRollCount.ToString();
        }

        public string Part2(string[] input)
        {
            var positions = new CellType[input.Length, input[0].Length];
            for (int i = 0; i < input.GetLength(0); i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    positions[i, j] = input[i][j] == '@' ? CellType.Roll : CellType.Empty;
                }
            }
            var removedRollCount = 0;
            while (true)
            {
                var (removedThisRound, newPositions) = RemoveAccessibleRolls(positions);
                if (removedThisRound == 0)
                {
                    break;
                }
                removedRollCount += removedThisRound;
                positions = newPositions;
            }
            return removedRollCount.ToString();
        }

        public static (int, CellType[,]) RemoveAccessibleRolls(CellType[,] input)
        {
            var positions = (CellType[,])input.Clone();
            var toRemove = new bool[positions.GetLength(0), positions.GetLength(1)];
            for (int i = 0; i < positions.GetLength(0); i++)
            {
                for (int j = 0; j < positions.GetLength(1); j++)
                {
                    if (positions[i, j] == CellType.Roll)
                    {
                        var adjacentRolls = 0;
                        for (int di = -1; di <= 1; di++)
                        {
                            for (int dj = -1; dj <= 1; dj++)
                            {
                                if (di == 0 && dj == 0)
                                {
                                    continue;
                                }
                                int ni = i + di;
                                int nj = j + dj;
                                if (ni >= 0 && ni < positions.GetLength(0) &&
                                    nj >= 0 && nj < positions.GetLength(1) &&
                                    positions[ni, nj] == CellType.Roll)
                                {
                                    adjacentRolls++;
                                }
                            }
                        }
                        if (adjacentRolls < 4)
                        {
                            toRemove[i, j] = true;
                        }
                    }
                }
            }
            var removedCount = 0;
            for (int i = 0; i < positions.GetLength(0); i++)
            {
                for (int j = 0; j < positions.GetLength(1); j++)
                {
                    if (toRemove[i, j])
                    {
                        positions[i, j] = CellType.Empty;
                        removedCount++;
                    }
                }
            }
            return (removedCount, positions);
        }
    }
}

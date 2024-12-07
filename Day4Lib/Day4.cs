using Helpers;
using Interfaces;

namespace Day4Lib;

public class Day4 : IAOCDay
{
   public int Day => 4;

   private readonly List<string> _grid = [];

   private static readonly List<Point> offsets =
   [
      new(0, -1),
      new(0, 1),
      new(-1, 0),
      new(1, 0),
      new(-1, -1),
      new(1, -1),
      new(-1, 1),
      new(1, 1)
   ];

   public Day4()
   {
      ParseData();
   }

   private void ParseData()
   {
      using var file = File.OpenRead("Day4.txt");
      using StreamReader sr = new(file);
      while (sr.ReadLine() is { } line)
      {
         _grid.Add(line);
      }
   }

   private char? GetLetter(Point start, Point offset)
   {
      var end = start + offset;

      if (_grid.Count == 0) return null;
      if (end.X >= _grid[0].Length || end.X < 0 || end.Y >= _grid.Count || end.Y < 0) return null;

      return _grid[end.Y][end.X];
   }

   private string? Build3String(Point center, Point offset)
   {
      char?[] letters = [GetLetter(center, offset), GetLetter(center, new Point(0, 0)), GetLetter(center, -offset)];
      return letters.Any(letter => letter is null) ? null : string.Join("", letters);
   }

   public long Part1()
   {
      var numXMAS = 0;
      for (var y = 0; y < _grid.Count; y++)
      {
         for (var x = 0; x < _grid[y].Length; x++)
         {
            Point start = new(x, y);
            numXMAS += offsets.Count(direction => GetLetter(start, direction * 0) == 'X' &&
                                                       GetLetter(start, direction * 1) == 'M' &&
                                                       GetLetter(start, direction * 2) == 'A' &&
                                                       GetLetter(start, direction * 3) == 'S');
         }
      }
      return numXMAS;
   }

   public long Part2()
   {
      var numMAS = 0;
      for (var y = 0; y < _grid.Count; y++)
      {
         for (var x = 0; x < _grid[y].Length; x++)
         {
            Point start = new(x, y);
            if (GetLetter(start, new Point(0, 0)) != 'A') continue;

            var leftDiag = Build3String(start, new Point(-1, -1));
            var rightDiag = Build3String(start, new Point(-1, 1));

            if (leftDiag is "MAS" or "SAM" && rightDiag is "MAS" or "SAM")
            {
               numMAS++;
            }
         }
      }
      return numMAS;
   }
}

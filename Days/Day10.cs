using System.Diagnostics.CodeAnalysis;
using Helpers;
using Interfaces;

namespace Days;

public class Day10 : IAOCDay
{
   public int Day => 10;

   private readonly List<List<int>> _trailMap = [];

   public Day10()
   {
      using var file = File.OpenRead("Day10.txt");
      using StreamReader sr = new(file);

      List<string> lines = [];
      while (sr.ReadLine() is { } line)
      {
         lines.Add(line);
      }
      ParseData(lines);
   }

   public Day10(string input)
   {
      ParseData(input.Split('\n').ToList());
   }

   private void ParseData(List<string> input)
   {
      foreach (var data in input.Select(line => line.ToCharArray().Select(char.ToString).Select(int.Parse).ToList()))
      {
         _trailMap.Add(data);
      }
   }

   private HashSet<Point> FindTrailHeads()
   {
      var result = new HashSet<Point>();
      for (var y = 0; y < _trailMap.Count; y++)
      {
         for (var x = 0; x < _trailMap[y].Count; x++)
         {
            if (_trailMap[y][x] == 0) result.Add(new Point(x, y));
         }
      }

      return result;
   }

   [SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Local")]
   private readonly record struct TrailRecord(Point Start, Point End);

   private List<TrailRecord> TraverseMap()
   {
      var result = new List<TrailRecord>();

      foreach (var trailhead in FindTrailHeads())
      {
         Stack<Point> stack = new();
         stack.Push(trailhead);

         while (stack.Count > 0)
         {
            var loc = stack.Pop();
            List<Point> candidates = [loc + new Point(0, 1), loc - new Point(0, 1), loc + new Point(1, 0), loc - new Point(1, 0)];

            if (_trailMap[loc.Y][loc.X] == 9)
            {
               result.Add(new TrailRecord(trailhead, loc));
            }

            foreach (var point in candidates.Where(dest => dest.Y < _trailMap.Count && dest.Y >= 0 &&
                                                                dest.X < _trailMap[0].Count && dest.X >= 0 &&
                                                                _trailMap[dest.Y][dest.X] == _trailMap[loc.Y][loc.X] + 1))
            {
               stack.Push(point);
            }
         }
      }

      return result;
   }

   public long Part1()
   {
      return TraverseMap().Distinct().Count();
   }

   public long Part2()
   {
      return TraverseMap().Count;
   }
}

using Interfaces;
using Helpers;

namespace Days;

public class Day6 : IAOCDay
{
   public int Day => 6;

   private readonly List<List<char>> _map = [];

   public Day6()
   {
      ParseData();
   }

   private void ParseData()
   {
      using var file = File.OpenRead("Day6.txt");
      using StreamReader sr = new(file);
      while (sr.ReadLine() is { } line)
      {
         _map.Add(line.ToList());
      }
   }

   private static Point FindStartingPos(List<List<char>> map)
   {
      for (var y = 0; y < map.Count; y++)
      {
         for (var x = 0; x < map[y].Count; x++)
         {
            if (map[y][x] == '^')
            {
               return new Point(x, y);
            }
         }
      }
      return default;
   }

   public long Part1()
   {
      var guard = new Guard(FindStartingPos(_map));
      guard.WalkMap(_map);
      return guard.WalkedSpaces.Count;
   }

   public long Part2()
   {
      var loopCount = 0;
      var guard = new Guard(FindStartingPos(_map));
      guard.WalkMap(_map);

      foreach (var space in guard.WalkedSpaces)
      {
         if (space == FindStartingPos(_map)) continue;
         var map = _map.Select(row => row.ToList()).ToList();
         map[space.Y][space.X] = 'O';
         var guard2 = new Guard(FindStartingPos(map));
         guard2.WalkMap(map);
         if (guard2.StuckInLoop)
         {
            loopCount++;
         }
      }

      return loopCount;
   }
}

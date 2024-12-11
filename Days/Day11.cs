using Interfaces;

namespace Days;

public class Day11 : IAOCDay
{
   private readonly Dictionary<long, long> _rocks = [];

   public Day11()
   {
      using var file = File.OpenRead("Day11.txt");
      using StreamReader sr = new(file);
      ParseData(sr.ReadToEnd().Trim());
   }

   private void ParseData(string input)
   {
      foreach (var rock in input.Split(" ").Select(long.Parse))
      {
         _rocks.TryAdd(rock, 0);
         _rocks[rock]++;
      }
   }

   public int Day => 11;

   private static List<long> Blink(long rock)
   {
      if (rock == 0) return [1];

      var digits = (long)Math.Floor(Math.Log10(rock)) + 1;

      if (digits % 2 != 0) return [rock * 2024];

      var halfDigits = digits / 2;
      var first = rock / (long)Math.Pow(10, halfDigits);
      var second = rock % (long)Math.Pow(10, halfDigits);
      return [first, second];
   }

   private static Dictionary<long, long> BlinkRocks(Dictionary<long, long> rocks)
   {
      Dictionary<long, long> result = [];

      foreach (var (rock, count) in rocks)
      {
         var newRocks = Blink(rock);
         foreach (var newRock in newRocks)
         {
            result.TryAdd(newRock, 0);
            result[newRock] += count;
         }
      }

      return result;
   }

   public long Part1()
   {
      var result = _rocks;
      for (var i = 0; i < 25; i++)
      {
         result = BlinkRocks(result);
      }

      return result.Values.Sum();
   }

   public long Part2()
   {
      var result = _rocks;
      for (var i = 0; i < 75; i++)
      {
         result = BlinkRocks(result);
      }

      return result.Values.Sum();
   }
}

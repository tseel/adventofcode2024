using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Interfaces;

namespace Day3Lib;

public partial class Day3 : IAOCDay
{
   public int Day => 3;

   private readonly string _commands;

   public Day3()
   {
      using var file = File.OpenRead("Day3.txt");
      using StreamReader sr = new(file);
      _commands = sr.ReadToEnd();
   }

   public Day3(string input)
   {
      _commands = input;
   }

   public long Part1()
   {
      var matches = MulRegex().Matches(_commands);
      return matches.Select(mul => int.Parse(mul.Groups[1].Value) * int.Parse(mul.Groups[2].Value)).Sum();
   }

   public long Part2()
   {
      var matches = MulRegex().Matches(_commands);
      var doIndices = DoRegex().Matches(_commands).Select(m => m.Index).ToImmutableArray();
      var dontIndices = DontRegex().Matches(_commands).Select(m => m.Index).ToImmutableArray();

      if (dontIndices.IsEmpty) return Part1();

      var lastDo = 0;
      var lastDont = 0;
      var total = 0;

      foreach (Match match in matches)
      {
         var beforeFirstDo = doIndices[0] > match.Index;
         var beforeFirstDont = dontIndices[0] > match.Index;

         while (lastDo < doIndices.Length - 1 && doIndices[lastDo + 1] < match.Index) lastDo++;
         while (lastDont < dontIndices.Length - 1 && dontIndices[lastDont + 1] < match.Index) lastDont++;

         if (beforeFirstDont || (!beforeFirstDo && doIndices[lastDo] > dontIndices[lastDont]))
         {
            total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
         }
      }

      return total;
   }

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"do\(\)")]
    private static partial Regex DoRegex();

    [GeneratedRegex(@"don't\(\)")]
    private static partial Regex DontRegex();
}

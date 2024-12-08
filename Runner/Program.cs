using System.Diagnostics;
using System.Reflection;
using Interfaces;

namespace Runner;

internal static class Runner
{
   private static void Main(string[] args)
   {
      HashSet<DayRun> dayRuns = [];
      foreach (var type in Assembly.Load("Days").GetTypes().Where(t => typeof(IAOCDay).IsAssignableFrom(t)))
      {
         var day = (IAOCDay)Activator.CreateInstance(type)!;
         var sw1 = Stopwatch.StartNew();
         var pt1 = day.Part1();
         sw1.Stop();
         var sw2 = Stopwatch.StartNew();
         var pt2 = day.Part2();
         sw2.Stop();
         Console.WriteLine();

         dayRuns.Add(new DayRun
                     {
                        Day = day.Day,
                        RunTime1 = sw1.ElapsedMilliseconds,
                        RunTime2 = sw2.ElapsedMilliseconds,
                        Answer1 = pt1,
                        Answer2 = pt2
                     });
      }

      foreach (var dayRun in dayRuns.OrderBy(x => x.Day))
      {
         Console.WriteLine($"Day {dayRun.Day} Part 1: {dayRun.Answer1}. Ran in {dayRun.RunTime1} ms.");
         Console.WriteLine($"Day {dayRun.Day} Part 2: {dayRun.Answer2}. Ran in {dayRun.RunTime2} ms.");
         Console.WriteLine();
      }
   }

   private struct DayRun
   {
      public int Day { get; init; }
      public long RunTime1 { get; init; }
      public long RunTime2 { get; init; }
      public long Answer1 { get; init;  }
      public long Answer2 { get; init; }
   }
}
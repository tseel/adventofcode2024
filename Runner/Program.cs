using System.Diagnostics;
using System.Reflection;
using Interfaces;

namespace Runner;

internal static class Runner
{
   private static void Main()
   {
      HashSet<Run> dayRuns = [];
      List<Tuple<int, int>> skip = [new(6, 2), new(9, 2)];
      foreach (var type in Assembly.Load("Days").GetTypes().Where(t => typeof(IAOCDay).IsAssignableFrom(t)))
      {
         var day = (IAOCDay)Activator.CreateInstance(type)!;
         try
         {
            if (!skip.Contains(new Tuple<int, int>(day.Day, 1)))
            {
               var sw1 = Stopwatch.StartNew();
               var pt1 = day.Part1();
               sw1.Stop();
               dayRuns.Add(new Run
                           {
                              Day = day.Day,
                              Part = 1,
                              RunTime = sw1.ElapsedMilliseconds,
                              Answer = pt1,
                           });
            }
         }
         catch (NotImplementedException)
         {
            Console.WriteLine($"Part 1 of day {day.Day} is not implemented.");
         }

         try
         {
            if (!skip.Contains(new Tuple<int, int>(day.Day, 2)))
            {
               var sw2 = Stopwatch.StartNew();
               var pt2 = day.Part2();
               sw2.Stop();
               dayRuns.Add(new Run
                           {
                              Day = day.Day,
                              Part = 2,
                              RunTime = sw2.ElapsedMilliseconds,
                              Answer = pt2,
                           });
            }
         }
         catch (NotImplementedException)
         {
            Console.WriteLine($"Part 2 of day {day.Day} is not implemented.");
         }
      }

      Console.WriteLine();

      foreach (var dayRun in dayRuns.OrderBy(x => x.Day).ThenBy(x => x.Part))
      {
         Console.WriteLine($"Day {dayRun.Day} Part {dayRun.Part}: {dayRun.Answer}. Ran in {dayRun.RunTime} ms.");
      }
   }

   private struct Run
   {
      public int Day { get; init; }
      public int Part { get; init; }
      public long RunTime { get; init; }
      public long Answer { get; init; }
   }
}

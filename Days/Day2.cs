using Interfaces;

namespace Days;

public class Day2 : IAOCDay
{
   public int Day => 2;

   private readonly List<List<int>> _reports = [];

   public Day2()
   {
      ParseData();
   }

   private void ParseData()
   {
      using var file = File.OpenRead("Day2.txt");
      using StreamReader sr = new(file);
      while (sr.ReadLine() is { } line)
      {
         _reports.Add(line.Split().Select(int.Parse).ToList());
      }
   }

   public static bool ReportIsSafe(List<int> report)
   {
      if (report.Count <= 1) return true;

      var increasing = report[0] < report[1];

      return report.Zip(report.Skip(1))
                   .Select(levelPair => levelPair.Second - levelPair.First)
                   .All(diff => ((!increasing && diff < 0) || (increasing && diff > 0)) && Math.Abs(diff) is >= 1 and <= 3);
   }

   public long Part1()
   {
      return _reports.Count(ReportIsSafe);
   }

   public long Part2()
   {
      var unsafeReports = _reports.Where(r => !ReportIsSafe(r)).ToList();
      var safeReports = _reports.Count - unsafeReports.Count;
      foreach (var report in unsafeReports)
      {
         for (var i = 0; i < report.Count; ++i)
         {
            List<int> reportCopy = [..report];
            reportCopy.RemoveAt(i);
            if (!ReportIsSafe(reportCopy)) continue;
            ++safeReports;
            break;
         }
      }

      return safeReports;
   }
}

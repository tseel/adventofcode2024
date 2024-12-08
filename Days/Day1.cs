using Interfaces;

namespace Day1Lib;

public class Day1 : IAOCDay
{

   public int Day => 1;

   private readonly List<int> _locID1 = [];
   private readonly List<int> _locID2 = [];

   public Day1()
   {
      ParseData();
   }

   private void ParseData()
   {
      using var file = File.OpenRead("Day1.txt");
      using StreamReader sr = new(file);
      while (sr.ReadLine() is { } line)
      {
         var splitLine = line.Split("   ");
         _locID1.Add(int.Parse(splitLine[0]));
         _locID2.Add(int.Parse(splitLine[1]));
      }
   }

   public long Part1()
   {
      List<int> locID1 = [.._locID1];
      List<int> locID2 = [.._locID2];
      locID1.Sort();
      locID2.Sort();

      return locID1.Zip(locID2, (a, b) => Math.Abs(a - b)).Sum();
   }

   public long Part2()
   {
      var locFreq = _locID2.CountBy(v => v).ToDictionary();
      return _locID1.Aggregate(0, (sum, loc) => sum + loc * locFreq.GetValueOrDefault(loc, 0));
   }
}

using Interfaces;

namespace Day5Lib;

public class Day5 : IAOCDay
{
   public int Day => 5;

   private readonly PageSorter _sorter = new();
   private readonly List<List<int>> _pages = [];

   private class PageSorter : IComparer<int>
   {
      public Dictionary<int, HashSet<int>> Rules { get; } = new();

      public int Compare(int x, int y)
      {
         if (Rules.TryGetValue(x, out var xRules) && xRules.Contains(y)) return -1;
         if (Rules.TryGetValue(y, out var yRules) && yRules.Contains(x)) return 1;
         return 0;
      }
   }
   public Day5()
   {
      ParseData();
   }

   private void ParseData()
   {
      using var file = File.OpenRead("Day5.txt");
      using StreamReader sr = new(file);

      // Parse rules
      while (sr.ReadLine() is { Length: > 0 } line)
      {
         var rule = line.Split('|').Select(int.Parse).ToList();
         if (!_sorter.Rules.ContainsKey(rule[0])) _sorter.Rules.Add(rule[0], []);
         _sorter.Rules[rule[0]].Add(rule[1]);
      }

      // Parse pages
      while (sr.ReadLine() is { } line)
      {
         _pages.Add(line.Split(',').Select(int.Parse).ToList());
      }
   }

   public long Part1()
   {
      long medianSum = 0;
      foreach (var page in _pages)
      {
         var pageSorted = page[..];
         pageSorted.Sort(_sorter);
         if (pageSorted.SequenceEqual(page))
         {
            medianSum += page[page.Count / 2];
         }
      }
      return medianSum;
   }

   public long Part2()
   {
      long medianSum = 0;
      foreach (var page in _pages)
      {
         var pageSorted = page[..];
         pageSorted.Sort(_sorter);
         if (!pageSorted.SequenceEqual(page))
         {
            medianSum += pageSorted[pageSorted.Count / 2];
         }
      }
      return medianSum;
   }
}

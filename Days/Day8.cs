using Helpers;
using Interfaces;

namespace Days;

public class Day8 : IAOCDay
{
   private struct Node()
   {
      public char? Antenna { get; init; } = null;
      public bool AntiNode { get; set; } = false;
   }

   public int Day => 8;

   private readonly Dictionary<Point, Node> _nodes = [];

   public Day8()
   {
      ParseData();
   }

   private void ParseData()
   {
      using var file = File.OpenRead("Day8.txt");
      using StreamReader sr = new(file);
      var y = 0;
      while (sr.ReadLine() is { } line)
      {
         var x = 0;
         foreach (var node in line)
         {
            _nodes.Add(new Point(x, y),
                       new Node
                       {
                          Antenna = node == '.' ? null : node,
                       });
            ++x;
         }

         ++y;
      }
   }

   public long Part1()
   {
      var nodes = _nodes.ToDictionary();
      foreach (var (node1Loc, node1) in _nodes.Where(x => x.Value.Antenna != null))
      {
         foreach (var (node2Loc, _) in _nodes.Where(x => x.Value.Antenna == node1.Antenna))
         {
            if (node2Loc == node1Loc) continue;
            var diff = node1Loc - node2Loc;
            List<Point> candidates = [node2Loc - diff, node1Loc + diff];
            candidates.ForEach(c =>
                               {
                                  if (!nodes.TryGetValue(c, out var node)) return;
                                  node.AntiNode = true;
                                  nodes[c] = node;
                               });
         }
      }

      return nodes.Count(x => x.Value.AntiNode);
   }

   public long Part2()
   {
      var nodes = _nodes.ToDictionary();
      foreach (var (node1Loc, node1) in _nodes.Where(x => x.Value.Antenna != null))
      {
         foreach (var (node2Loc, _) in _nodes.Where(x => x.Value.Antenna == node1.Antenna))
         {
            if (node2Loc == node1Loc) continue;
            var diff = node1Loc - node2Loc;
            List<Point> candidates = [];

            var curNode = node2Loc;

            while (nodes.ContainsKey(curNode))
            {
               candidates.Add(curNode);
               curNode -= diff;
            }

            curNode = node1Loc;
            while (nodes.ContainsKey(curNode))
            {
               candidates.Add(curNode);
               curNode += diff;
            }

            candidates.ForEach(c =>
                               {
                                  if (!nodes.TryGetValue(c, out var node)) return;
                                  node.AntiNode = true;
                                  nodes[c] = node;
                               });
         }
      }

      return nodes.Count(x => x.Value.AntiNode);
   }
}

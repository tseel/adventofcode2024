using Interfaces;

namespace Days;

public class Day9 : IAOCDay
{
   public int Day => 9;

   public Day9()
   {
      using var file = File.OpenRead("Day9.txt");
      using StreamReader sr = new(file);
      ParseData(sr.ReadToEnd().Trim());
   }

   public Day9(string input)
   {
      ParseData(input);
   }

   private readonly List<int> _disk = [];

   private const int FREE = -1;

   private void ParseData(string input)
   {
      var id = 0;
      var data = true;
      foreach (var count in input.Select(c => int.Parse(c.ToString())))
      {
         for (var i = 0; i < count; ++i)
         {
            _disk.Add(data ? id : FREE);
         }

         if (data)
         {
            ++id;
         }
         data = !data;
      }
   }

   private static int FindStartOfBlock(List<int> diskBlocks, int endOfBlock)
   {
      var target = diskBlocks[endOfBlock];
      var curPos = endOfBlock;
      while (curPos >= 0 && diskBlocks[curPos] == target) --curPos;
      ++curPos;
      return curPos;
   }


   private static int FindEndOfBlock(List<int> diskBlocks, int startOfBlock)
   {
      var target = diskBlocks[startOfBlock];
      var curPos = startOfBlock;
      while (curPos < diskBlocks.Count && diskBlocks[curPos] == target) ++curPos;
      --curPos;
      return curPos;
   }


   private static Dictionary<int, int> CreateFreeMap(List<int> disk, int startIdx = 0, int endIdx = int.MaxValue)
   {
      Dictionary<int, int> freeBlocks = [];

      for (var it = startIdx; it < endIdx && it < disk.Count; ++it)
      {
         if (disk[it] != FREE) continue;
         var size = FindEndOfBlock(disk, it) - it + 1;
         freeBlocks.TryAdd(size, it);
         it += size;
      }

      return freeBlocks;
   }

   public long Part1()
   {
      var disk = _disk[..];
      var firstFree = disk.FindIndex(b => b == FREE);
      var lastData = disk.FindLastIndex(b => b != FREE);

      while (firstFree < lastData)
      {
         (disk[lastData], disk[firstFree]) = (disk[firstFree], disk[lastData]);
         do
         {
            ++firstFree;
         } while (disk[firstFree] != FREE);

         do
         {
            --lastData;
         } while (disk[lastData] == FREE);
      }

      return disk.Select((block, i) =>
                               {
                                  if (block != FREE) return (long)block * i;
                                  return 0;
                               }).Sum();
   }

   public long Part2()
   {
      var disk = _disk[..];

      var freeBlocks = CreateFreeMap(disk, disk.FindIndex(b => b == FREE));


      var movedIds = new HashSet<int>();
      for (var endData = disk.FindLastIndex(b => b != FREE); endData >= 0; --endData)
      {
         var startData = FindStartOfBlock(disk, endData);
         var blockSize = endData - startData + 1;
         if (!movedIds.Add(disk[startData]) || freeBlocks.Count == 0) continue;

         var startFree = -1;


         foreach (var (freeSize, freeLoc) in freeBlocks.OrderBy(x => x.Value))
         {
            if (freeSize < blockSize || freeLoc >= startData) continue;
            startFree = freeLoc;
            break;
         }

         if (startFree == -1) continue;

         for (var i = 0; i < blockSize; ++i)
         {
            (disk[startData + i], disk[startFree + i]) = (disk[startFree + i], disk[startData + i]);
         }

         var firstFree = freeBlocks.Count != 0 ? freeBlocks.Values.Min() : 0;
         freeBlocks = CreateFreeMap(disk, firstFree, startData);
      }

      return disk.Select((block, i) =>
                               {
                                  if (block != FREE) return (long)block * i;
                                  return 0;
                               }).Sum();
   }
}

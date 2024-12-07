using Interfaces;

namespace Day7Lib;

public class Day7 : IAOCDay
{
   public int Day => 7;

   private List<Tuple<long, List<long>>> _operations = [];

   public Day7()
   {
      ParseData();
   }

   private enum EOperation
   {
      ADD,
      MUL,
      CONCAT
   }

   private void ParseData()
   {
      using var file = File.OpenRead("Day7.txt");
      using StreamReader sr = new(file);
      while (sr.ReadLine() is { } line)
      {
         var data = line.Split(": ");
         _operations.Add(new Tuple<long, List<long>>(long.Parse(data[0]), data[1].Split(" ").Select(long.Parse).ToList()));
      }
   }

   private bool TestOperation(List<long> nums, long target, EOperation operation, int part)
   {
      if (nums.Count == 1) return nums[0] == target;
      switch (operation)
      {
         case EOperation.ADD:
         {
            var addNums = nums[1..];
            addNums[0] += nums[0];
            return TestOperation(addNums, target, part);
         }
         case EOperation.MUL:
            var mulNums = nums[1..];
            mulNums[0] *= nums[0];
            return TestOperation(mulNums, target, part);
         case EOperation.CONCAT:
            if (part == 1) return false; // Don't support Concat in part 1

            var conNums = nums[1..];
            conNums[0] = long.Parse(nums[0].ToString() + nums[1]);
            return TestOperation(conNums, target, part);
         default:
            throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
      }
   }

   private bool TestOperation(List<long> nums, long target, int part)
   {
      return TestOperation(nums, target, EOperation.ADD, part) || TestOperation(nums, target, EOperation.MUL, part) || TestOperation(nums, target, EOperation.CONCAT, part);
   }

   public long Part1()
   {
      return _operations.Where(kv => TestOperation(kv.Item2, kv.Item1, 1)).Select(kv => kv.Item1).Sum();
   }


   public long Part2()
   {
      return _operations.Where(kv => TestOperation(kv.Item2, kv.Item1, 2)).Select(kv => kv.Item1).Sum();
   }
}

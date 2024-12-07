using System.Diagnostics;
using Day6Lib;

var day = new Day6();
var sw = Stopwatch.StartNew();
var pt1 = day.Part1();
sw.Stop();
Console.WriteLine($"Day {day.Day} Part 1: {pt1}. Ran in {sw.ElapsedMilliseconds} ms.");
sw.Restart();
var pt2 = day.Part2();
sw.Stop();
Console.WriteLine($"Day {day.Day} Part 2: {pt2}. Ran in {sw.ElapsedMilliseconds} ms.");

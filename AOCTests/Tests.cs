using Day2Lib;

namespace AOCTests;

public class Tests
{
   [SetUp]
   public void Setup() { }

   [Test]
   public void Day2Tests()
   {
        Assert.Multiple(() =>
        {
            Assert.That(Day2.ReportIsSafe([7, 6, 4, 2, 1]), Is.True);
            Assert.That(Day2.ReportIsSafe([1, 3, 6, 7, 9]), Is.True);

            Assert.That(Day2.ReportIsSafe([1, 2, 7, 8, 9]), Is.False);
            Assert.That(Day2.ReportIsSafe([9, 7, 6, 2, 1]), Is.False);
            Assert.That(Day2.ReportIsSafe([1, 3, 2, 4, 5]), Is.False);
            Assert.That(Day2.ReportIsSafe([8, 6, 4, 4, 1]), Is.False);

            Assert.That(Day2.ReportIsSafe([1]), Is.True);
            Assert.That(Day2.ReportIsSafe([]), Is.True);

            Assert.That(Day2.ReportIsSafe([10, 6]), Is.False);
            Assert.That(Day2.ReportIsSafe([10, 10]), Is.False);
            Assert.That(Day2.ReportIsSafe([9, 10, 10]), Is.False);
            Assert.That(Day2.ReportIsSafe([9, 10, 8]), Is.False);
            Assert.That(Day2.ReportIsSafe([9, 10, 11]), Is.True);
            Assert.That(Day2.ReportIsSafe([10, 7]), Is.True);

            Assert.That(Day2.ReportIsSafe([15, 13, 12, 10, 9, 6, 5]), Is.True);
        });
    }
}

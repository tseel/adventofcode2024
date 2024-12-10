using Days;

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

   [Test]
   public void Day3Tests()
   {
      var day3 = new Day3("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))");
      Assert.That(day3.Part2(), Is.EqualTo(161));

      day3 =
         new
            Day3("??$'why(),$#,(mul(388,863)]$;mul(93,214)${from(),>mul(554,29);when(),@#who()),mul(203,377),when()<%;[%;mul(459,428)$where()mul(289,903)$;?]what(),when()%*mul(920,908)~%from()>)?!++@mul(328)");
      var complexAnswer = day3.Part2();

      day3 =
         new
            Day3("]};don't(){^mul(131,421)who()+where()why()why() mul)who()from(361,208)#($>/mul(986,7)~!/+:what(911,564)&~mul(427,317):<how()[-+?from()*do()??$'why(),$#,(mul(388,863)]$;mul(93,214)${from(),>mul(554,29);when(),@#who()),mul(203,377),when()<%;[%;mul(459,428)$where()mul(289,903)$;?]what(),when()%*mul(920,908)~%from()>)?!++@mul(328)");
      Assert.That(day3.Part2(), Is.EqualTo(complexAnswer));
   }

   [Test]
   public void Day9Tests()
   {
      var day9 = new Day9("2333133121414131402");
        Assert.Multiple(() =>
        {
            Assert.That(day9.Part1(), Is.EqualTo(1928));
            Assert.That(day9.Part2(), Is.EqualTo(2858));
        });
    }
}

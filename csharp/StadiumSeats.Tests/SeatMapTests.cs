using StadiumSeats;

namespace StadiumSeats.Tests;

// The tests for your code. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.

[TestClass]
public class SeatMapTests
{
    // What the seat map does today. These pass before you change anything: keep them passing.

    [TestMethod]
    public void APremiumSeatHasItsPriceColorAndPerks()
    {
        SeatDetails details = new SeatMap(200, 250).SeatAt(12, 7).Details;
        Assert.AreEqual("premium", details.Kind, "Keep this working: row 12, seat 7 is a premium seat.");
        Assert.AreEqual(90, details.Price, "Keep this working: a premium seat costs $90.");
        Assert.AreEqual("gold", details.Color, "Keep this working: premium seats are gold on the map.");
        CollectionAssert.AreEqual(new List<string> { "padded seat", "cup holder", "in-seat service" }, details.Perks.ToList(), "Keep this working: a premium seat comes with a padded seat, a cup holder and in-seat service.");
    }

    [TestMethod]
    public void TheFirstTwoSeatsAfterRowTwentyAreAccessible()
    {
        SeatMap map = new SeatMap(200, 250);
        Assert.AreEqual("accessible", map.SeatAt(21, 1).Details.Kind, "Keep this working: row 21, seat 1 is an accessible seat.");
        Assert.AreEqual("green", map.SeatAt(21, 2).Details.Color, "Keep this working: accessible seats are green on the map.");
        Assert.AreEqual("standard", map.SeatAt(21, 3).Details.Kind, "Keep this working: row 21, seat 3 is a standard seat.");
        Assert.AreEqual(40, map.SeatAt(21, 3).Details.Price, "Keep this working: a standard seat costs $40.");
    }

    [TestMethod]
    public void TheStadiumHasFiftyThousandSeatsInThreeKinds()
    {
        SeatMap map = new SeatMap(200, 250);
        Assert.AreEqual(50000, map.Size, "Keep this working: 200 rows of 250 seats is 50,000 seats.");
        Assert.AreEqual(44640, map.Count("standard"), "Keep this working: 44,640 seats are standard.");
        Assert.AreEqual(5000, map.Count("premium"), "Keep this working: 5,000 seats are premium.");
        Assert.AreEqual(360, map.Count("accessible"), "Keep this working: 360 seats are accessible.");
    }

    [TestMethod]
    public void EverySeatKnowsItsRowAndNumber()
    {
        Seat seat = new SeatMap(200, 250).SeatAt(150, 249);
        Assert.AreEqual(150, seat.Row, "Keep this working: the seat at row 150, number 249 knows its row.");
        Assert.AreEqual(249, seat.Number, "Keep this working: the seat at row 150, number 249 knows its number.");
    }

    // The ticket's new requirements. These fail until the work is done.

    [TestMethod]
    public void TheFestivalMapMakesOnlyThreeSeatDetails()
    {
        int before = SeatDetails.Made;
        SeatMap festival = new SeatMap(800, 250);
        int made = SeatDetails.Made - before;
        Assert.AreEqual(200000, festival.Size, "Keep this working: the festival map has 200,000 places.");
        Assert.IsTrue(made <= 3, $"Not yet: 200,000 places need at most 3 SeatDetails, one for each kind of seat, but the map made {made}.");
    }

    [TestMethod]
    public void TheFestivalMapFitsOnAKioskTablet()
    {
        long bytes = new SeatMap(800, 250).EstimatedBytes();
        Assert.IsTrue(bytes < 10_000_000, $"Not yet: the festival map must fit in 10 MB on a kiosk tablet, but it takes about {bytes} bytes.");
    }

    [TestMethod]
    public void SeatsOfTheSameKindShareOneDetailsObject()
    {
        SeatMap map = new SeatMap(200, 250);
        Assert.AreSame(map.SeatAt(1, 1).Details, map.SeatAt(12, 7).Details, "Not yet: two premium seats should share one SeatDetails object.");
        Assert.AreSame(map.SeatAt(21, 3).Details, map.SeatAt(199, 250).Details, "Not yet: two standard seats should share one SeatDetails object.");
    }
}

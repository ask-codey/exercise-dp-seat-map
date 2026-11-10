import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertSame;
import static org.junit.jupiter.api.Assertions.assertTrue;

import java.util.List;
import org.junit.jupiter.api.Test;

// The tests for your code. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.
class SeatMapTest {
    // What the seat map does today. These pass before you change anything: keep them passing.

    @Test
    void aPremiumSeatHasItsPriceColorAndPerks() {
        SeatDetails details = new SeatMap(200, 250).seatAt(12, 7).details;
        assertEquals("premium", details.kind, "Keep this working: row 12, seat 7 is a premium seat.");
        assertEquals(90, details.price, "Keep this working: a premium seat costs $90.");
        assertEquals("gold", details.color, "Keep this working: premium seats are gold on the map.");
        assertEquals(List.of("padded seat", "cup holder", "in-seat service"), details.perks, "Keep this working: a premium seat comes with a padded seat, a cup holder and in-seat service.");
    }

    @Test
    void theFirstTwoSeatsAfterRowTwentyAreAccessible() {
        SeatMap map = new SeatMap(200, 250);
        assertEquals("accessible", map.seatAt(21, 1).details.kind, "Keep this working: row 21, seat 1 is an accessible seat.");
        assertEquals("green", map.seatAt(21, 2).details.color, "Keep this working: accessible seats are green on the map.");
        assertEquals("standard", map.seatAt(21, 3).details.kind, "Keep this working: row 21, seat 3 is a standard seat.");
        assertEquals(40, map.seatAt(21, 3).details.price, "Keep this working: a standard seat costs $40.");
    }

    @Test
    void theStadiumHasFiftyThousandSeatsInThreeKinds() {
        SeatMap map = new SeatMap(200, 250);
        assertEquals(50000, map.size(), "Keep this working: 200 rows of 250 seats is 50,000 seats.");
        assertEquals(44640, map.count("standard"), "Keep this working: 44,640 seats are standard.");
        assertEquals(5000, map.count("premium"), "Keep this working: 5,000 seats are premium.");
        assertEquals(360, map.count("accessible"), "Keep this working: 360 seats are accessible.");
    }

    @Test
    void everySeatKnowsItsRowAndNumber() {
        Seat seat = new SeatMap(200, 250).seatAt(150, 249);
        assertEquals(150, seat.row, "Keep this working: the seat at row 150, number 249 knows its row.");
        assertEquals(249, seat.number, "Keep this working: the seat at row 150, number 249 knows its number.");
    }

    // The ticket's new requirements. These fail until the work is done.

    @Test
    void theFestivalMapMakesOnlyThreeSeatDetails() {
        int before = SeatDetails.made;
        SeatMap festival = new SeatMap(800, 250);
        int made = SeatDetails.made - before;
        assertEquals(200000, festival.size(), "Keep this working: the festival map has 200,000 places.");
        assertTrue(made <= 3, "Not yet: 200,000 places need at most 3 SeatDetails, one for each kind of seat, but the map made " + made + ".");
    }

    @Test
    void theFestivalMapFitsOnAKioskTablet() {
        long bytes = new SeatMap(800, 250).estimatedBytes();
        assertTrue(bytes < 10_000_000, "Not yet: the festival map must fit in 10 MB on a kiosk tablet, but it takes about " + bytes + " bytes.");
    }

    @Test
    void seatsOfTheSameKindShareOneDetailsObject() {
        SeatMap map = new SeatMap(200, 250);
        assertSame(map.seatAt(1, 1).details, map.seatAt(12, 7).details, "Not yet: two premium seats should share one SeatDetails object.");
        assertSame(map.seatAt(21, 3).details, map.seatAt(199, 250).details, "Not yet: two standard seats should share one SeatDetails object.");
    }
}

import java.util.Locale;

// A demo: builds today's map and the festival's. You don't need to change it.
// Run it with: java src/main/java/Demo.java
public class Demo {
    public static void main(String[] args) {
        SeatMap stadium = new SeatMap(200, 250);
        System.out.println("Seats: " + stadium.size() + " (standard " + stadium.count("standard") + ", premium " + stadium.count("premium") + ", accessible " + stadium.count("accessible") + ")");
        Seat picked = stadium.seatAt(12, 7);
        System.out.println("Row 12, seat 7: " + picked.details.kind + ", $" + picked.details.price + ", " + picked.details.color);

        SeatMap festival = new SeatMap(800, 250);
        System.out.println("Festival map: " + festival.size() + " places, about " + String.format(Locale.ROOT, "%.1f", festival.estimatedBytes() / 1_000_000.0) + " MB");
        System.out.println("Seat details made: " + SeatDetails.made);
    }
}

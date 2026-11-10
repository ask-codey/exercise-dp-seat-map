import java.util.ArrayList;
import java.util.List;

class TicketDatabase {
    // The first 20 rows are premium; after that, the first 2 seats of every row are accessible.
    static String kindOf(int row, int number) {
        return row <= 20 ? "premium" : number <= 2 ? "accessible" : "standard";
    }

    // What the ticket database sends back for a kind of seat: a new SeatDetails every time it's asked.
    static SeatDetails loadDetails(String kind) {
        if (kind.equals("premium")) { return new SeatDetails("premium", 90, "gold", new ArrayList<>(List.of("padded seat", "cup holder", "in-seat service"))); }
        if (kind.equals("accessible")) { return new SeatDetails("accessible", 40, "green", new ArrayList<>(List.of("wheelchair space", "companion seat", "cup holder"))); }
        return new SeatDetails("standard", 40, "blue", new ArrayList<>(List.of("cup holder")));
    }
}

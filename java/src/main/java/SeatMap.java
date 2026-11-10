import java.util.ArrayList;
import java.util.Collections;
import java.util.IdentityHashMap;
import java.util.List;
import java.util.Set;

class SeatMap {
    // The kiosk team's memory estimate, in bytes, for each part of the map.
    static final int SEAT_BYTES = 40;
    static final int DETAILS_BYTES = 120;
    static final int PERK_BYTES = 40;

    private final List<Seat> seats = new ArrayList<>();
    private final int seatsPerRow;

    SeatMap(int rows, int seatsPerRow) {
        this.seatsPerRow = seatsPerRow;
        for (int row = 1; row <= rows; row++) {
            for (int number = 1; number <= seatsPerRow; number++) {
                seats.add(new Seat(row, number, TicketDatabase.loadDetails(TicketDatabase.kindOf(row, number))));
            }
        }
    }

    int size() { return seats.size(); }

    Seat seatAt(int row, int number) { return seats.get((row - 1) * seatsPerRow + (number - 1)); }

    long count(String kind) { return seats.stream().filter(seat -> seat.details.kind.equals(kind)).count(); }

    // The kiosk team's estimate of the map's memory: every seat, plus every different SeatDetails object once.
    long estimatedBytes() {
        Set<SeatDetails> details = Collections.newSetFromMap(new IdentityHashMap<>());
        for (Seat seat : seats) { details.add(seat.details); }
        long bytes = (long) seats.size() * SEAT_BYTES;
        for (SeatDetails detail : details) { bytes += DETAILS_BYTES + PERK_BYTES * detail.perks.size(); }
        return bytes;
    }
}

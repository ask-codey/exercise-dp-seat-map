"""Harbor Stadium's seat map."""

SEAT_BYTES = 40      # the kiosk team's memory estimate, in bytes, for each seat,
DETAILS_BYTES = 120  # for each SeatDetails object,
PERK_BYTES = 40      # and for each perk in it


class SeatDetails:
    """What a kind of seat comes with. The kiosk team counts how many are made, to track down the memory problem."""
    made = 0

    def __init__(self, kind, price, color, perks):
        SeatDetails.made += 1
        self.kind = kind
        self.price = price
        self.color = color
        self.perks = perks


class Seat:
    def __init__(self, row, number, details):
        self.row = row
        self.number = number
        self.details = details


def kind_of(row, number):
    """The first 20 rows are premium; after that, the first 2 seats of every row are accessible."""
    if row <= 20:
        return "premium"
    if number <= 2:
        return "accessible"
    return "standard"


def load_details(kind):
    """What the ticket database sends back for a kind of seat: a new SeatDetails every time it's asked."""
    if kind == "premium":
        return SeatDetails("premium", 90, "gold", ["padded seat", "cup holder", "in-seat service"])
    if kind == "accessible":
        return SeatDetails("accessible", 40, "green", ["wheelchair space", "companion seat", "cup holder"])
    return SeatDetails("standard", 40, "blue", ["cup holder"])


class SeatMap:
    def __init__(self, rows, seats_per_row):
        self.seats_per_row = seats_per_row
        self.seats = []
        for row in range(1, rows + 1):
            for number in range(1, seats_per_row + 1):
                self.seats.append(Seat(row, number, load_details(kind_of(row, number))))

    def size(self):
        return len(self.seats)

    def seat_at(self, row, number):
        return self.seats[(row - 1) * self.seats_per_row + (number - 1)]

    def count(self, kind):
        return sum(1 for seat in self.seats if seat.details.kind == kind)

    def estimated_bytes(self):
        """The kiosk team's estimate of the map's memory: every seat, plus every different SeatDetails object once."""
        details = {id(seat.details): seat.details for seat in self.seats}.values()
        return len(self.seats) * SEAT_BYTES + sum(DETAILS_BYTES + PERK_BYTES * len(detail.perks) for detail in details)

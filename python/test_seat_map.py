"""The tests for your code. You don't need to change this file.
Each message says what was expected, so a failing test tells you what to look at."""
from seat_map import SeatDetails, SeatMap


# What the seat map does today. These pass before you change anything: keep them passing.

def test_a_premium_seat_has_its_price_color_and_perks():
    details = SeatMap(200, 250).seat_at(12, 7).details
    assert details.kind == "premium", "Keep this working: row 12, seat 7 is a premium seat."
    assert details.price == 90, "Keep this working: a premium seat costs $90."
    assert details.color == "gold", "Keep this working: premium seats are gold on the map."
    assert list(details.perks) == ["padded seat", "cup holder", "in-seat service"], \
        "Keep this working: a premium seat comes with a padded seat, a cup holder and in-seat service."


def test_the_first_two_seats_after_row_twenty_are_accessible():
    seat_map = SeatMap(200, 250)
    assert seat_map.seat_at(21, 1).details.kind == "accessible", "Keep this working: row 21, seat 1 is an accessible seat."
    assert seat_map.seat_at(21, 2).details.color == "green", "Keep this working: accessible seats are green on the map."
    assert seat_map.seat_at(21, 3).details.kind == "standard", "Keep this working: row 21, seat 3 is a standard seat."
    assert seat_map.seat_at(21, 3).details.price == 40, "Keep this working: a standard seat costs $40."


def test_the_stadium_has_fifty_thousand_seats_in_three_kinds():
    seat_map = SeatMap(200, 250)
    assert seat_map.size() == 50000, "Keep this working: 200 rows of 250 seats is 50,000 seats."
    assert seat_map.count("standard") == 44640, "Keep this working: 44,640 seats are standard."
    assert seat_map.count("premium") == 5000, "Keep this working: 5,000 seats are premium."
    assert seat_map.count("accessible") == 360, "Keep this working: 360 seats are accessible."


def test_every_seat_knows_its_row_and_number():
    seat = SeatMap(200, 250).seat_at(150, 249)
    assert seat.row == 150, "Keep this working: the seat at row 150, number 249 knows its row."
    assert seat.number == 249, "Keep this working: the seat at row 150, number 249 knows its number."


# The ticket's new requirements. These fail until the work is done.

def test_the_festival_map_makes_only_three_seat_details():
    before = SeatDetails.made
    festival = SeatMap(800, 250)
    made = SeatDetails.made - before
    assert festival.size() == 200000, "Keep this working: the festival map has 200,000 places."
    assert made <= 3, f"Not yet: 200,000 places need at most 3 SeatDetails, one for each kind of seat, but the map made {made}."


def test_the_festival_map_fits_on_a_kiosk_tablet():
    size = SeatMap(800, 250).estimated_bytes()
    assert size < 10_000_000, f"Not yet: the festival map must fit in 10 MB on a kiosk tablet, but it takes about {size} bytes."


def test_seats_of_the_same_kind_share_one_details_object():
    seat_map = SeatMap(200, 250)
    assert seat_map.seat_at(1, 1).details is seat_map.seat_at(12, 7).details, \
        "Not yet: two premium seats should share one SeatDetails object."
    assert seat_map.seat_at(21, 3).details is seat_map.seat_at(199, 250).details, \
        "Not yet: two standard seats should share one SeatDetails object."

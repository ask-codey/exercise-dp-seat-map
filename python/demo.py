"""A demo: builds today's map and the festival's. You don't need to change it."""
from seat_map import SeatDetails, SeatMap

stadium = SeatMap(200, 250)
print(f"Seats: {stadium.size()} (standard {stadium.count('standard')}, premium {stadium.count('premium')}, accessible {stadium.count('accessible')})")
picked = stadium.seat_at(12, 7)
print(f"Row 12, seat 7: {picked.details.kind}, ${picked.details.price}, {picked.details.color}")

festival = SeatMap(800, 250)
print(f"Festival map: {festival.size()} places, about {festival.estimated_bytes() / 1_000_000:.1f} MB")
print(f"Seat details made: {SeatDetails.made}")

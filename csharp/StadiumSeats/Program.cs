using System.Globalization;
using StadiumSeats;

// A demo: builds today's map and the festival's. You don't need to change it.
SeatMap stadium = new SeatMap(200, 250);
Console.WriteLine($"Seats: {stadium.Size} (standard {stadium.Count("standard")}, premium {stadium.Count("premium")}, accessible {stadium.Count("accessible")})");
Seat picked = stadium.SeatAt(12, 7);
Console.WriteLine($"Row 12, seat 7: {picked.Details.Kind}, ${picked.Details.Price}, {picked.Details.Color}");

SeatMap festival = new SeatMap(800, 250);
Console.WriteLine($"Festival map: {festival.Size} places, about {(festival.EstimatedBytes() / 1_000_000.0).ToString("0.0", CultureInfo.InvariantCulture)} MB");
Console.WriteLine($"Seat details made: {SeatDetails.Made}");

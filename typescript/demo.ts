// A demo: builds today's map and the festival's. You don't need to change it.
import { SeatDetails, SeatMap } from "./seat-map.ts";

const stadium = new SeatMap(200, 250);
console.log(`Seats: ${stadium.size} (standard ${stadium.count("standard")}, premium ${stadium.count("premium")}, accessible ${stadium.count("accessible")})`);
const picked = stadium.seatAt(12, 7);
console.log(`Row 12, seat 7: ${picked.details.kind}, $${picked.details.price}, ${picked.details.color}`);

const festival = new SeatMap(800, 250);
console.log(`Festival map: ${festival.size} places, about ${(festival.estimatedBytes() / 1_000_000).toFixed(1)} MB`);
console.log(`Seat details made: ${SeatDetails.made}`);

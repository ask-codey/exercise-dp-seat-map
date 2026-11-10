// The tests for your code. You don't need to change this file.
// Each message says what was expected, so a failing test tells you what to look at.
import assert from "node:assert/strict";
import { test } from "node:test";
import { SeatDetails, SeatMap } from "./seat-map.ts";

// What the seat map does today. These pass before you change anything: keep them passing.

test("a premium seat has its price, color and perks", () => {
  const details = new SeatMap(200, 250).seatAt(12, 7).details;
  assert.equal(details.kind, "premium", "Keep this working: row 12, seat 7 is a premium seat.");
  assert.equal(details.price, 90, "Keep this working: a premium seat costs $90.");
  assert.equal(details.color, "gold", "Keep this working: premium seats are gold on the map.");
  assert.deepEqual([...details.perks], ["padded seat", "cup holder", "in-seat service"], "Keep this working: a premium seat comes with a padded seat, a cup holder and in-seat service.");
});

test("the first two seats after row 20 are accessible", () => {
  const map = new SeatMap(200, 250);
  assert.equal(map.seatAt(21, 1).details.kind, "accessible", "Keep this working: row 21, seat 1 is an accessible seat.");
  assert.equal(map.seatAt(21, 2).details.color, "green", "Keep this working: accessible seats are green on the map.");
  assert.equal(map.seatAt(21, 3).details.kind, "standard", "Keep this working: row 21, seat 3 is a standard seat.");
  assert.equal(map.seatAt(21, 3).details.price, 40, "Keep this working: a standard seat costs $40.");
});

test("the stadium has 50,000 seats in three kinds", () => {
  const map = new SeatMap(200, 250);
  assert.equal(map.size, 50000, "Keep this working: 200 rows of 250 seats is 50,000 seats.");
  assert.equal(map.count("standard"), 44640, "Keep this working: 44,640 seats are standard.");
  assert.equal(map.count("premium"), 5000, "Keep this working: 5,000 seats are premium.");
  assert.equal(map.count("accessible"), 360, "Keep this working: 360 seats are accessible.");
});

test("every seat knows its row and number", () => {
  const seat = new SeatMap(200, 250).seatAt(150, 249);
  assert.equal(seat.row, 150, "Keep this working: the seat at row 150, number 249 knows its row.");
  assert.equal(seat.number, 249, "Keep this working: the seat at row 150, number 249 knows its number.");
});

// The ticket's new requirements. These fail until the work is done.

test("the festival map makes only three seat details", () => {
  const before = SeatDetails.made;
  const festival = new SeatMap(800, 250);
  const made = SeatDetails.made - before;
  assert.equal(festival.size, 200000, "Keep this working: the festival map has 200,000 places.");
  assert.ok(made <= 3, `Not yet: 200,000 places need at most 3 SeatDetails, one for each kind of seat, but the map made ${made}.`);
});

test("the festival map fits on a kiosk tablet", () => {
  const bytes = new SeatMap(800, 250).estimatedBytes();
  assert.ok(bytes < 10_000_000, `Not yet: the festival map must fit in 10 MB on a kiosk tablet, but it takes about ${bytes} bytes.`);
});

test("seats of the same kind share one details object", () => {
  const map = new SeatMap(200, 250);
  assert.equal(map.seatAt(1, 1).details, map.seatAt(12, 7).details, "Not yet: two premium seats should share one SeatDetails object.");
  assert.equal(map.seatAt(21, 3).details, map.seatAt(199, 250).details, "Not yet: two standard seats should share one SeatDetails object.");
});

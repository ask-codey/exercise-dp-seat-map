// Harbor Stadium's seat map.

// The kiosk team's memory estimate, in bytes, for each seat, each SeatDetails object and each perk in it.
export const SEAT_BYTES = 40;
export const DETAILS_BYTES = 120;
export const PERK_BYTES = 40;

// What a kind of seat comes with. The kiosk team counts how many are made, to track down the memory problem.
export class SeatDetails {
  static made = 0;
  kind: string;
  price: number;
  color: string;
  perks: string[];

  constructor(kind: string, price: number, color: string, perks: string[]) {
    SeatDetails.made++;
    this.kind = kind;
    this.price = price;
    this.color = color;
    this.perks = perks;
  }
}

export class Seat {
  row: number;
  number: number;
  details: SeatDetails;

  constructor(row: number, number: number, details: SeatDetails) {
    this.row = row;
    this.number = number;
    this.details = details;
  }
}

// The first 20 rows are premium; after that, the first 2 seats of every row are accessible.
export function kindOf(row: number, number: number): string {
  if (row <= 20) { return "premium"; }
  if (number <= 2) { return "accessible"; }
  return "standard";
}

// What the ticket database sends back for a kind of seat: a new SeatDetails every time it's asked.
export function loadDetails(kind: string): SeatDetails {
  if (kind === "premium") { return new SeatDetails("premium", 90, "gold", ["padded seat", "cup holder", "in-seat service"]); }
  if (kind === "accessible") { return new SeatDetails("accessible", 40, "green", ["wheelchair space", "companion seat", "cup holder"]); }
  return new SeatDetails("standard", 40, "blue", ["cup holder"]);
}

export class SeatMap {
  seatsPerRow: number;
  seats: Seat[] = [];

  constructor(rows: number, seatsPerRow: number) {
    this.seatsPerRow = seatsPerRow;
    for (let row = 1; row <= rows; row++) {
      for (let number = 1; number <= seatsPerRow; number++) {
        this.seats.push(new Seat(row, number, loadDetails(kindOf(row, number))));
      }
    }
  }

  get size(): number { return this.seats.length; }

  seatAt(row: number, number: number): Seat { return this.seats[(row - 1) * this.seatsPerRow + (number - 1)]; }

  count(kind: string): number { return this.seats.filter((seat) => seat.details.kind === kind).length; }

  // The kiosk team's estimate of the map's memory: every seat, plus every different SeatDetails object once.
  estimatedBytes(): number {
    const details = new Set(this.seats.map((seat) => seat.details));
    let bytes = this.seats.length * SEAT_BYTES;
    for (const detail of details) { bytes += DETAILS_BYTES + PERK_BYTES * detail.perks.length; }
    return bytes;
  }
}

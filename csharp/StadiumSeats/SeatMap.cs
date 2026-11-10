namespace StadiumSeats;

// What a kind of seat comes with. The kiosk team counts how many are made, to track down the memory problem.
public class SeatDetails
{
    public static int Made { get; private set; }

    public SeatDetails(string kind, int price, string color, List<string> perks)
    {
        Made++;
        Kind = kind;
        Price = price;
        Color = color;
        Perks = perks;
    }

    public string Kind { get; set; }
    public int Price { get; set; }
    public string Color { get; set; }
    public List<string> Perks { get; set; }
}

public class Seat
{
    public Seat(int row, int number, SeatDetails details)
    {
        Row = row;
        Number = number;
        Details = details;
    }

    public int Row { get; }
    public int Number { get; }
    public SeatDetails Details { get; }
}

public static class TicketDatabase
{
    // The first 20 rows are premium; after that, the first 2 seats of every row are accessible.
    public static string KindOf(int row, int number) => row <= 20 ? "premium" : number <= 2 ? "accessible" : "standard";

    // What the ticket database sends back for a kind of seat: a new SeatDetails every time it's asked.
    public static SeatDetails LoadDetails(string kind)
    {
        if (kind == "premium") { return new SeatDetails("premium", 90, "gold", new List<string> { "padded seat", "cup holder", "in-seat service" }); }
        if (kind == "accessible") { return new SeatDetails("accessible", 40, "green", new List<string> { "wheelchair space", "companion seat", "cup holder" }); }
        return new SeatDetails("standard", 40, "blue", new List<string> { "cup holder" });
    }
}

public class SeatMap
{
    // The kiosk team's memory estimate, in bytes, for each part of the map.
    public const int SeatBytes = 40;
    public const int DetailsBytes = 120;
    public const int PerkBytes = 40;

    private readonly List<Seat> seats = new List<Seat>();
    private readonly int seatsPerRow;

    public SeatMap(int rows, int seatsPerRow)
    {
        this.seatsPerRow = seatsPerRow;
        for (int row = 1; row <= rows; row++)
        {
            for (int number = 1; number <= seatsPerRow; number++)
            {
                seats.Add(new Seat(row, number, TicketDatabase.LoadDetails(TicketDatabase.KindOf(row, number))));
            }
        }
    }

    public int Size => seats.Count;

    public Seat SeatAt(int row, int number) => seats[(row - 1) * seatsPerRow + (number - 1)];

    public int Count(string kind) => seats.Count(seat => seat.Details.Kind == kind);

    // The kiosk team's estimate of the map's memory: every seat, plus every different SeatDetails object once.
    public long EstimatedBytes()
    {
        IEnumerable<SeatDetails> details = seats.Select(seat => seat.Details).Distinct(ReferenceEqualityComparer.Instance).Cast<SeatDetails>();
        return (long)seats.Count * SeatBytes + details.Sum(detail => (long)DetailsBytes + PerkBytes * detail.Perks.Count);
    }
}

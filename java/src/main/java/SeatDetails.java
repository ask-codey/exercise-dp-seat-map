import java.util.List;

// What a kind of seat comes with. The kiosk team counts how many are made, to track down the memory problem.
class SeatDetails {
    static int made = 0;

    String kind;
    int price;
    String color;
    List<String> perks;

    SeatDetails(String kind, int price, String color, List<String> perks) {
        made++;
        this.kind = kind;
        this.price = price;
        this.color = color;
        this.perks = perks;
    }
}

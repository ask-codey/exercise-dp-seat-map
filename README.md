# Exercise: The Stadium Seat Map

Exercise code for the lesson **Exercise: The Stadium Seat Map** in the course *Design Patterns, Demystified* on [ask-codey.com](https://www.ask-codey.com).

## The ticket

> **From:** the kiosk team, Harbor Stadium
>
> 1. **The festival map has to fit on the kiosk tablets.** Next summer's festival has 200,000 places (800 rows of 250), and the tablets at the gates have room for about 10 MB of seat map. We added a memory estimate, `estimatedBytes()`, and today's code puts the festival map at about 40 MB. It has to come in under 10 MB.
> 2. **We think we know why.** We count every `SeatDetails` the map makes (`SeatDetails.made`), and the festival map makes 200,000 of them, one for every place, though there are only three kinds of seat: standard, premium and accessible. The festival map should make at most three.
> 3. **Seats of the same kind should share one details object**, so that, for example, every premium seat's details are the very same object.
>
> Everything the map does today has to keep working: every seat's kind, price, color and perks, the counts of each kind, and each seat's row and number.

`estimatedBytes()` and the `SeatDetails` counter are already there; the kiosk team added them to measure the problem. You don't need to change either of them.

## Pick your language

The same exercise is here in five languages. Work in the folder for the language you set up; you only need one.

| Folder | The seat map's code | The tests |
|---|---|---|
| `csharp/` | `StadiumSeats/SeatMap.cs` | `StadiumSeats.Tests/SeatMapTests.cs` |
| `java/` | `src/main/java/` (`SeatMap.java`, `SeatDetails.java`, `TicketDatabase.java` and `Seat.java`) | `src/test/java/SeatMapTest.java` |
| `python/` | `seat_map.py` | `test_seat_map.py` |
| `javascript/` | `seat-map.js` | `seat-map.test.js` |
| `typescript/` | `seat-map.ts` | `seat-map.test.ts` |

The names follow each language's style: `SeatAt` and `EstimatedBytes()` in C#, `seat_at` and `estimated_bytes()` in Python, and `seatAt` and `estimatedBytes()` in Java, JavaScript and TypeScript.

Every folder also has a small demo program that builds today's 50,000-seat map and the festival's 200,000-place map. You don't need to change the tests or the demo.

## Run it and check your work

Open your language's folder in VS Code (**File > Open Folder**), then open its terminal (**Terminal > New Terminal**) and run these commands there.

**C#**

```bash
dotnet run --project StadiumSeats
dotnet test
```

**Java** (the Maven Wrapper downloads Maven the first time, so the first run takes a minute)

```bash
java src/main/java/Demo.java
./mvnw test
```

On Windows in PowerShell, write `.\mvnw test` instead of `./mvnw test`.

**Python** (once: make a virtual environment and install pytest into it)

```bash
python -m venv .venv
```

Then turn it on: `.venv\Scripts\Activate.ps1` in PowerShell on Windows, or `source .venv/bin/activate` on macOS (where the command is `python3`, not `python`). Then:

```bash
pip install -r requirements.txt
python demo.py
python -m pytest
```

**JavaScript**

```bash
node demo.js
npm test
```

**TypeScript** (once: `npm install`, which installs TypeScript)

```bash
node demo.ts
npm test
```

At first, 4 tests pass and 3 fail. The 4 that pass are what the map does today: keep them green the whole time, and run the tests after every small change. The 3 that fail are the ticket. A failing test's message starts with **Not yet:** (or **Keep this working:** if you broke something that worked) and says what it expected.

Today the demo prints:

```text
Seats: 50000 (standard 44640, premium 5000, accessible 360)
Row 12, seat 7: premium, $90, gold
Festival map: 200000 places, about 40.5 MB
Seat details made: 250000
```

## When you're done

All 7 tests are green, and the demo prints:

```text
Seats: 50000 (standard 44640, premium 5000, accessible 360)
Row 12, seat 7: premium, $90, gold
Festival map: 200000 places, about 8.0 MB
Seat details made: 3
```

<details>
<summary>Stuck?</summary>

- **What's the same, and what's each seat's own?** A seat's kind, price, color and perks are the same for every seat of its kind; only its row and number are its own. Store the shared part once per kind, and let every seat of that kind point to the same object (the **Flyweight** pattern). (*Flyweight: Share What's the Same*.)
- **Where the shared objects come from:** `TicketDatabase.LoadDetails` (`load_details`, `loadDetails`) makes a new `SeatDetails` every time it's called. Put one place in front of it that asks the database once for each kind, keeps what it gets, and hands out the same object every time after: a dictionary from kind to details does it.
- **Once objects are shared, nobody should change them.** If one premium seat's price could be changed, every premium seat would change with it. Make the details read-only.

</details>

## Hand it in

When it works, push your solution to a public repo of your own on GitHub, then paste that repo's address into **Submit your exercise** on the lesson page at [ask-codey.com](https://www.ask-codey.com). That's what completes the lesson. The lesson page shows the Git commands, step by step.

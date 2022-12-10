var file = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

PartOne();
PartTwo();

void PartOne()
{
    var cyclesWhenMeasurementNeeded = new List<int>() { 20, 60, 100, 140, 180, 220 };
    var measurements = new List<int>();
    var cycle = 0;
    var x = 1;
    foreach (var line in File.ReadLines(file))
    {
        cycle++;
        if (!line.StartsWith("addx"))
        {
            continue;
        }

        if (cyclesWhenMeasurementNeeded.Contains(cycle))
        {
            measurements.Add(x);
        }

        cycle++;
        if (cyclesWhenMeasurementNeeded.Contains(cycle))
        {
            measurements.Add(x);
        }

        var value = int.Parse(line.Split(" ")[1]);
        x += value;
    }

    var signalStrength = 0;
    for (int i = 0; i < measurements.Count; i++)
    {
        signalStrength += measurements[i] * cyclesWhenMeasurementNeeded[i];
    }

    Console.WriteLine(signalStrength);
}

void PartTwo()
{
    var cycle = 1;
    const int screenWide = 40;
    var x = 1;
    var row = new List<char>(Enumerable.Repeat('.', screenWide));
    const int addCommandCycleCount = 2;
    foreach (var line in File.ReadLines(file))
    {
        if (!line.StartsWith("addx"))
        {
            DrawPixel(x, cycle, row);
            if (cycle == screenWide)
            {
                DrawLine(screenWide, ref row);
                cycle = 0;
            }

            cycle++;
            continue;
        }

        for (int i = 0; i < addCommandCycleCount; i++)
        {
            DrawPixel(x, cycle, row);
            if (cycle == screenWide)
            {
                DrawLine(screenWide, ref row);
                cycle = 0;
            }

            cycle++;
        }

        var value = int.Parse(line.Split(" ")[1]);
        x += value;
    }
}

void DrawPixel(int x, int cycle, IList<char> row)
{
    if (x - 1 <= cycle - 1 && cycle - 1 <= x + 1)
    {
        row[cycle - 1] = '#';
    }
    else
    {
        row[cycle - 1] = '.';
    }
}

void DrawLine(int screenWide1, ref List<char> chars)
{
    Console.WriteLine(new string(chars.ToArray()));
    chars = new List<char>(Enumerable.Repeat('.', screenWide1));
}
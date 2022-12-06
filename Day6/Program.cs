var file = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

var dataStreamBuffer = File.ReadLines(file).First();
PartOne();
PartTwo();

void PartOne()
{
    const int markerLength = 4;
    var firstMarker = FindFirstMarker(markerLength);
    Console.WriteLine(firstMarker);
}

void PartTwo()
{
    const int markerLength = 14;
    var firstMarker = FindFirstMarker(markerLength);
    Console.WriteLine(firstMarker);
}

int FindFirstMarker(int markerLength)
{
    var final = dataStreamBuffer.Take(markerLength).ToList();
    for (int i = markerLength; i < dataStreamBuffer.Length; i++)
    {
        var group = final.GroupBy(e => e);
        if (group.All(e => e.Count() == 1))
        {
            return i;
        }

        final.RemoveAt(0);
        final.Add(dataStreamBuffer[i]);
    }

    return 0;
}